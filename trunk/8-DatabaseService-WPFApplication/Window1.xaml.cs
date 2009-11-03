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

using _7_Database;

namespace _8_DatabaseWebService
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public Window1() : this( "Name", "Phone", "Room" ) {
        }

        public Window1(params string[] fields)
        {
            InitializeComponent();
            InitFields(fields);
        }

        protected void InitFields(params string[] fields) {
            foreach (string field in fields) {
                
            }
        }
    }
}
