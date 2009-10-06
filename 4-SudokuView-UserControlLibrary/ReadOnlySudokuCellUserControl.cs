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

using _4_SudokuView;

namespace _4_SudokuView_UserControlLibrary
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public class ReadOnlySudokuCellUserControl : SudokuCellUserControl
    {
        public ReadOnlySudokuCellUserControl(SudokuViewWindow window, SolidColorBrush brush) : base(window, brush) { }

        /// <summary>Read-only cells should not respond to a click event with any sort of play commnd.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Click(System.Object sender, EventArgs e)
        {
        }

        /// <summary>Readonly cells cannot update anything.</summary>
        public override void Update() { }
    }
}
