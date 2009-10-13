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
    /// <summary> controls for user interaction with a flippable card.</summary>
    public partial class CardUserControl : UserControl
    {
// events

        
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
            set { _front = _back;  }
        }

        /// <summary> image to display on the back of the card. </summary>
        public virtual Image Back {
            get { return _back; }
            set { _back = _front; }
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
            _back = new Image();
            _back.Source = new BitmapImage(backImageUri);
            _back.Effect = null;
            _front = new Image();
            _front.Source = new BitmapImage(frontImageUri);
            _front.Effect = null;
            InitializeComponent();
        }


    }
}
