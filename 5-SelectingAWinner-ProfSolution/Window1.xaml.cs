using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ATS;
using ATS.Poker;

namespace ATS.Winner {

  /// <summary> view for card game. </summary>
  public partial class Window1: Window, IView {

    #region code to access card bitmaps

    /// <summary> relative URI for folder with images. </summary>
    /// <remarks> Files are numbered <c>1.png</c> and up. </remarks>
    protected readonly string ImageDir = "images/";

    /// <summary> maps <c>Cards</c>'s suit numbers to image file names. </summary>
    /// <remarks> <c>Deck</c> maps as <c>CDHS</c>, image files are ordered as <c>CSHD</c>. </remarks>
    protected readonly int[] MapSuit = new int[] { 0, 3, 2, 1 };

    /// <summary> maps <c>Cards</c>'s value numbers to image file names. </summary>
    /// <remarks> <c>Deck</c> maps as <c>23456789TJQKA</c>, image files are ordered as <c>AKQJT98765432</c>. </remarks>
    protected readonly int[] MapValue = new int[] { 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

    /// <summary> cached bitmaps for cards. </summary>
    /// <remarks> <c>static</c> to share between windows. </remarks>
    protected static BitmapSource[] bitmaps = new BitmapSource[52];

    /// <summary> cached bitmaps for red and blue back. </summary>
    /// <remarks> <c>static</c> to share between windows. </remarks>
    private static BitmapSource red, blue;

    /// <summary> returns and caches image for a card. </summary>
    public BitmapSource Bitmap (int suit, int value) {
      int n = MapValue[value] * 4 + MapSuit[suit];
      if (bitmaps[n] == null)
        bitmaps[n] = new BitmapImage(new Uri(ImageDir + "/" + (n + 1) + ".png"));
      return bitmaps[n];
    }

    /// <summary> red back. </summary>
    public BitmapSource Red {
      get {
        if (red == null)
          red = new BitmapImage(new Uri(ImageDir + "/red.png"));
        return red;
      }
    }

    /// <summary> blue back. </summary>
    public BitmapSource Blue {
      get {
        if (blue == null)
          blue = new BitmapImage(new Uri(ImageDir + "/blue.png"));
        return blue;
      }
    }

    #endregion
    #region fields

    /// <summary> references to the images in the GUI, defines size. </summary>
    protected readonly Image[] images;

    /// <summary> report choice from event thread to referee. </summary>
    protected readonly Cell<int> choice = new Cell<int>();

    #endregion
    #region constructors

    /// <summary> convenience constructor for game with 7 cards.  </summary>
    public Window1 () : this(7, "http://www.cs.rit.edu/~ats/cs-2009-1/2/Release/images") { }

    /// <summary> populates GUI. </summary>
    /// <param name="nCards"> number of cards in a game, >= 1. </param>
    /// <param name="ImageDir"> folder for images. </param>
    public Window1 (int nCards, string ImageDir) {
      InitializeComponent();

      this.ImageDir = ImageDir;
      images = new Image[nCards];

      // create images, etc. 
      for (int n = 0; n < images.Length; ++n) {
        Image image = images[n] = new Image();
        image.Width = 72.0;
        image.Margin = new Thickness(4.0);
        image.MouseUp += new System.Windows.Input.MouseButtonEventHandler(image_MouseUp);
        Items.Children.Add(image);
      }
      ResetImages();
    }

    /// <summary> turn back of images up. </summary>
    protected virtual void ResetImages () {
      for (int n = 0; n < images.Length; ++n) {
        images[n].Source = n % 2 == 0 ? Red : Blue;
        images[n].Tag = n; // index
      }
    }

    #endregion
    #region methods

    /// <summary> referee thread asks for choice. </summary>
    /// <remarks> enable images, wait for choice, disable images. </remarks>
    public virtual int Choose () {
      Allow(true);
      int result = choice.Value;
      Allow(false);
      return result;
    }

    /// <summary> used by referee thread to dis/en/able the GUI. </summary>
    protected virtual void Allow (bool yes) {
      if (!Status.CheckAccess())
        Status.Dispatcher.Invoke(new Action(delegate {
          Allow(yes);
        }));
      else {
        Status.Text = yes ? "Pick a card" : "";
        foreach (Image image in images)
          image.IsEnabled = yes;
      }
    }

    /// <summary> referee reports other choice. </summary>
    /// <remarks> display card, remove tag. </remarks>
    public virtual void Tell (int index, int suit, int value) {
      if (!images[0].CheckAccess())
        images[0].Dispatcher.Invoke(new Action(delegate {
          Tell(index, suit, value);
        }));
      else {
        images[index].Source = Bitmap(suit, value);
        images[index].Tag = null;
      }
    }

    /// <summary> show what happened. </summary>
    public virtual void Winner (bool yes) {
      if (!Status.CheckAccess())
        Status.Dispatcher.Invoke(new Action(delegate {
          Winner(yes);
        }));
      else {
        Status.Text = yes ? "You win!" : "You loose!";
        Reset.IsEnabled = true;
      }
    }

    /// <summary> wait until Reset is clicked. </summary>
    public virtual void Ready () {
      int dummy = choice.Value;
    }

    /// <summary> image is clicked. </summary>
    /// <remarks> if available, report choice. </remarks>
    protected virtual void image_MouseUp (object sender, System.Windows.Input.MouseButtonEventArgs e) {
      Image image = (Image)sender;
      if (image.Tag != null)
        choice.Value = (int)image.Tag;
    }

    /// <summary> reset is clicked. </summary>
    /// <remarks> clean up images, report zero choice. </remarks>
    protected virtual void Reset_Click (object sender, RoutedEventArgs e) {
      ResetImages();
      Reset.IsEnabled = false;
      Status.Text = "";
      choice.Value = -1;
    }

    #endregion
    #region main program with arguments

    /// <summary> run the application with a seed, a variable number of cards, players,
    ///   and a URI for the image directory. </summary>
    [System.STAThreadAttribute()]
    public static void Main (string[] args) {
      // get seed
      int seed = int.Parse(args[0]);

      // get m
      int m = int.Parse(args[1]);

      // get n
      int n = int.Parse(args[2]);

      // get URI
      string uri = args[3];

      // create application and views
      App app = new App();

      Window1[] views = new Window1[n];
      for (int v = 0; v < n; ++v)
        (views[v] = new Window1(m, uri)).Show();

      // run referee in background
      BackgroundWorker worker = new BackgroundWorker();

      worker.DoWork += new DoWorkEventHandler(delegate {
        Referee.Rounds(seed, views, m);
      });
      worker.RunWorkerAsync();

      // run event loop
      app.Run();
    }

    #endregion
  }
}
