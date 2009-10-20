namespace ATS {
  namespace Winner {
    /// <summary> what a game view must do. </summary>
    public interface IView {
      /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
      int Choose ();

      /// <summary> find out about a chosen card. </summary>
      void Tell (int index, int suit, int value);

      /// <summary> find out about a round's outcome. </summary>
      void Winner (bool yes);

      /// <summary> return once view is ready for a new round. </summary>
      void Ready ();
    }
  }
}