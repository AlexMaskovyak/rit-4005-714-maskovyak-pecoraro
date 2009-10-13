using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5_SelectingAWinner_UserControlLibrary
{
    /// <summary> handler for a card flip event. </summary>
    /// <param name="c"> reference to control which had an event occur. </param>
    public delegate void CardFlippedEventHandler( CardUserControl c );

    /// <summary> controls for user interaction with a flippable card.</summary>
    public partial class CardUserControl : UserControl
    {

// events
        /// <summary> fired when a card has been selected and flipped. </summary>
        public event CardFlippedEventHandler OnFlip;
        
// fields

        /// <summary> image to display on the front of the card. </summary>
        protected Image _front;
        /// <summary> image to display on the back of the card. </summary>
        protected Image _back;

        /// <summary> specifies whether the card has been flipped to reveal its value. </summary>
        protected bool _revealed;

// properties

        /// <summary> image to display on the front of the card. </summary>
        public virtual Image Front {
            get { return _front;  }
            set { _front = value; }
        }

        /// <summary> image to display on the back of the card. </summary>
        public virtual Image Back {
            get { return _back; }
            set { _back = value; }
        }

        /// <summary> specifies whether the card has been flipped to reveal its face. </summary>
        public virtual bool Revealed {
            get { return _revealed;  }
            set { _revealed = value;  }
        }

// constructors

        /// <summary> default constructor. </summary>
        /// <param name="frontImageUri"> uri for the front of the card. </param>
        /// <param name="backImageUri"> uri for the back of the card. </param>
        public CardUserControl(Uri frontImageUri, Uri backImageUri) {
            InitializeComponent();

            _back = new Image();
            _back.Source = new BitmapImage(backImageUri);
            _back.Effect = null;
            _front = new Image();
            _front.Source = new BitmapImage(frontImageUri);
            _front.Effect = null;

            this.MouseUp += new MouseButtonEventHandler(OnClick);
        }


// handlers

        /// <summary> handler user clicks on the control. </summary>
        /// <param name="sender"> default sender. </param>
        /// <param name="e"> default event arguments. </param>
        public virtual void OnClick(System.Object sender, MouseButtonEventArgs e) {
            if(!Revealed) {
                Image temp = Front;
                Front = Back;
                Back = temp;
                OnFlip(this);
            }
        }

    }
}
