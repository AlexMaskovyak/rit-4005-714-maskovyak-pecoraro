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

using _4_SudokuView_UserControlLibrary;

namespace _4_SudokuView_Tester
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            SudokuCellUserControl control = new SudokuCellUserControl();
            control.Visibility = System.Windows.Visibility.Visible;
            this.Content = control;
            
            InitializeComponent();

            this.Show();

        }
    }
}
