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
    /// <summary>Controls for user interaction with a flippable card.</summary>
    public partial class CardUserControl : UserControl
    {

        protected Image _back;
        protected Image _front;

        /// <summary> image to display on the front of the card. </summary>
        public Image Front {
            get { return _front;  }
            set { _front = value;  }
        }

        /// <summary> image to display on the back of the card. </summary>
        public Image Back {
            get { return _back; }
            set { _back = value; }
        }


        public CardUserControl()
        {
            InitializeComponent();
        }

// Properties

    }
}
