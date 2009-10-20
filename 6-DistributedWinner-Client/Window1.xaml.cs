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

namespace _6_DistributedWinner_Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        protected temperatures.Service1SoapClient model = new temperatures.Service1SoapClient(); // proxy

        public Window1()
        {
            InitializeComponent();
            
            TextBox t = new TextBox();
            System.Console.WriteLine(model.HelloWorld());
            
        }
    }
}
