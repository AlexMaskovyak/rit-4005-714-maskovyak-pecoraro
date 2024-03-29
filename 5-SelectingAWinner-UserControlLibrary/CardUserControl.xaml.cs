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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Effects;

namespace _5_SelectingAWinner_UserControlLibrary
{
    /// <summary> handler for a card flip event. </summary>
    /// <param name="c"> reference to control which had an event occur. </param>
    public delegate void CardFlippedEventHandler( CardUserControl c );

    /// <summary> controls for user interaction with a flippable card.</summary>
    public partial class CardUserControl : UserControl {

// events

        /// <summary> fired when a card has been selected and flipped to show its face. </summary>
        public event CardFlippedEventHandler OnFlip;

        /// <summary> fired when a card has been selected and flipped to show its back. </summary>
        public event CardFlippedEventHandler OnHide;
        
// fields

        /// <summary> specifies whether the card has been flipped to reveal its value. </summary>
        protected bool _revealed = false;

        /// <summary> highlighted or not </summary>
        protected bool _highlighted = false;

// properties

        /// <summary> image to display on the front of the card. </summary>
        public virtual BitmapImage Front {
            set { imgFront.Source = value; }
        }

        /// <summary> image to display on the back of the card. </summary>
        public virtual BitmapImage Back {
            set { imgBack.Source = value; }
        }

        /// <summary> specifies whether the card has been flipped to reveal its face. </summary>
        /// <remarks> setting will flip the card accordingly. </remarks>
        public virtual bool Revealed {
            get { return _revealed;  }
            set {
                if (_revealed != value) {
                    _revealed = value;
                    imgBack.Visibility = (imgBack.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden);
                    imgFront.Visibility = (imgFront.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden);
                }
            }
        }

        /// <summary> add a simple highlight effect to the control </summary>
        public virtual bool Highlighted {
            get { return _highlighted; }
            set {
                _highlighted = value;
                if (!value) {
                    this.Effect = null;
                    return;
                }

                // Red Color
                Color color = new Color();
                color.ScA = 1;
                color.ScB = 0;
                color.ScG = 0;
                color.ScR = 2;

                // Drop Shadow
                DropShadowEffect drop = new DropShadowEffect();
                drop.Color = color;
                drop.Opacity = 0.5;
                drop.ShadowDepth = 10;
                drop.Direction = 320;

                this.Effect = drop;
            }
        }

// constructors

        /// <summary> default constructor. </summary>
        public CardUserControl() { }

        /// <summary> default constructor. </summary>
        /// <param name="backImage"> uri for the back of the card. </param>
        public CardUserControl(BitmapImage backImage) {
            InitializeComponent();
            imgBack.Source = backImage;
            this.MouseUp += new MouseButtonEventHandler(OnClick);
        }


// handlers

        /// <summary> handler user clicks on the control. </summary>
        /// <param name="sender"> default sender. </param>
        /// <param name="e"> default event arguments. </param>
        public virtual void OnClick(System.Object sender, MouseButtonEventArgs e) {
            if (!Revealed) {
                if (OnFlip != null) OnFlip(this);
            } else {
                if (OnHide != null) OnHide(this);
            }
        }

    }
}
