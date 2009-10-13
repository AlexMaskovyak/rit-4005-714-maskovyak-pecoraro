﻿using System;
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
using System.Windows.Shapes;

using _5_SelectingAWinner_ConsoleApplication;

namespace _5_SelectingAWinner_WPFApplication
{
    /// <summary>
    /// Interaction logic for CardGameViewWindow.xaml
    /// </summary>
    public partial class CardGameViewWindow : Window, IView
    {

// fields
        /// <summary> cell holding player's ready state. </summary>
        protected Cell<bool> _readyCell;

        /// <summary> cell holding a player's selection. </summary>
        protected Cell<int> _chooseCell;


// constructors

        public CardGameViewWindow() {
            InitializeComponent();
        }

// IView implementation

        /// <summary> return <c>0..m-1</c>, index of chosen (and unexposed) card. </summary>
        public int Choose()
        {
            throw new NotImplementedException();
        }

        /// <summary> find out about a chosen card. </summary>
        public void Tell(int index, int suit, int value)
        {
            throw new NotImplementedException();
        }

        /// <summary> find out about a round's outcome. </summary>
        public void Winner(bool yes)
        {
            throw new NotImplementedException();
        }

        /// <summary> return once view is ready for a new round. </summary>
        public void Ready()
        {
            throw new NotImplementedException();
        }
    }
}
