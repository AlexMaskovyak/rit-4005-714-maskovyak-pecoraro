using System;
using System.Collections.Generic;
using ATS.Poker;

namespace ATS {
  namespace Winner {

    /// <summary> subclass to change order. </summary>
    public class MyCard: Card {

      /// <summary> create from string with value and suit. </summary>
      public MyCard (string face) : base(face) { }

      /// <summary> order by suit clubs/spades/hearts/diamonds and then by value. </summary>
      public override int CompareTo (Card other) {
        //            C D H S
        int[] map = { 3, 0, 1, 2 };

        if (Suit != other.Suit) return map[Suit] - map[other.Suit];
        return base.CompareTo(other);
      }
    }

    /// <summary> subclass to use <c>MyCard</c>. </summary>
    public class Deck: ATS.PokerPuzzle.Deck {

      /// <summary> add factory method. </summary>
      protected override void Init () {
        for (int suit = 0; suit < Card.Suits.Length; ++suit)
          for (int value = 0; value < Card.Values.Length; ++value)
            deck[suit * Card.Values.Length + value] =
              NewCard(new string(new char[]{
                Card.Suits[suit], Card.Values[value]
              }));
      }

      /// <summary> factory method. </summary>
      protected virtual Card NewCard (string face) {
        return new MyCard(face);
      }
    }

    /// <summary> run rounds of a card game. </summary>
    public static class Referee {

      /// <summary> run rounds. </summary>
      /// <param name="seed"> first random seed; increment by 1 for each round. </param>
      /// <param name="players"> list of players. </param>
      /// <param name="m"> number of cards to deal. </param>
      public static void Rounds (int seed, IView[] players, int m) {
        for (int starter = 0; true; ++seed) {
          // deal
          IEnumerable<Card> sequence = new Deck().Shuffle(seed);
          Card[] cards = new Card[m];
          int c = 0;
          foreach (var card in sequence) {
            cards[c] = card;
            if (++c >= m) break;
          }

          // avoid duplicate choices
          bool[] chosen = new bool[cards.Length];

          // remember maximum and winning player
          Card max = null;
          int winner = -1;

          // run a round
          for (var p = 0; p < players.Length; ++p) {
            // make an unused choice
            int choice;
            do
              choice = players[p].Choose();
            while (choice < 0 || choice >= chosen.Length || chosen[choice]);
            chosen[choice] = true;

            // show and tell
            foreach (IView view in players)
              view.Tell(choice, cards[choice].Suit, cards[choice].Value);

            // remember max
            if (max == null || cards[choice].CompareTo(max) > 0) {
              max = cards[choice];
              winner = p;
            }
          }

          // outcome
          for (var p = 0; p < players.Length; ++p)
            players[p].Winner(p == winner);

          // ready for new game?
          foreach (var player in players)
            player.Ready();

          // player following winner starts
          starter = (winner + 1) % players.Length;
        }
      }
    }

#if !SILVERLIGHT
    /// <summary> test program and view. </summary>
    public class View: IView {

      /// <summary> run one test. </summary>
      /// <param name="arg"> seed, number of cards, choice of player 1..n. </param>
      public static void Main (string[] arg) {
        // get seed
        int seed = int.Parse(arg[0]);
        // get m
        int m = int.Parse(arg[1]);

        // get i[0..n-1] into players
        IView[] views = new IView[arg.Length - 2];
        for (int v = 0; v < views.Length; ++v)
          views[v] = new View((char)('A' + v), int.Parse(arg[v + 2]));

        // run
        Referee.Rounds(seed, views, m);
      }

      /// <summary> label view for debugging. </summary>
      protected readonly char name;

      /// <summary> view's choice. </summary>
      protected readonly int choice;

      /// <summary> create a player with name and choice. </summary>
      public View (char name, int choice) {
        this.name = name; this.choice = choice;
      }

      /// <summary> return and show choice. </summary>
      public virtual int Choose () {
        Console.WriteLine(name + ": chooses " + choice);
        return choice;
      }

      /// <summary> first player shows everybody's choice. </summary>
      public void Tell (int index, int suit, int value) {
        if (name == 'A')
          Console.WriteLine("someone chose card " + index + ": suit " + suit + ", value " + value);
      }

      /// <summary> announce name of winner only. </summary>
      public void Winner (bool yes) {
        if (yes) Console.WriteLine(name + ": I won");
      }

      /// <summary> terminate test. </summary>
      public void Ready () {
        Environment.Exit(0);
      }
    }
#endif
  }
}