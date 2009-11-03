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
using System.Windows.Media.Effects;


namespace _8_DatabaseWebService
{
    /// <summary>
    /// Interaction logic for TupleFieldsUserControl.xaml
    /// </summary>
    public partial class TupleFieldsUserControl : System.Windows.Controls.UserControl, IEnumerable<string>
    {
        // Fields
        /// <summary> holds quick access to the TextBoxes of this control. </summary>
        protected ICollection<TextBox> _textBoxes;

       
        /// <summary> convenience constructor. </summary>
        public TupleFieldsUserControl() : this(3) {}

        /// <summary> convenience constructor. </summary>
        /// <param name="numberOfFields"> number of blank fields to create. </param>
        public TupleFieldsUserControl(int numberOfFields)
            : this(new string[numberOfFields])
        {
        }

        /// <summary> main constructor. </summary>
        /// <param name="fieldNames"></param>
        public TupleFieldsUserControl(ICollection<string> fieldNames) {
            _textBoxes = new List<TextBox>();

            InitializeComponent();
            AddFields(fieldNames);
        }

        
        /// <summary> adds a single blank field. </summary>
        public void AddField() {
            AddFields(1);
        }

        /// <summary> adds a number of blank fields. </summary>
        /// <param name="numberOfFields"> number of fields to add. </param>
        public void AddFields(int numberOfFields) {
            AddFields(new string[numberOfFields]);
        }

        /// <summary> adds a number of fields with the specific headers. </summary>
        /// <param name="fieldNames"> collection of field names. </param>
        public void AddFields(IEnumerable<string> fieldNames) {
            foreach (string name in fieldNames) {
                // create groupbox with header, could be blank
                GroupBox groupBox = new GroupBox();
                groupBox.Header = name;
                Grid grid = new Grid();
                groupBox.Content = grid;
                TextBox textBox = new TextBox();
                grid.Children.Add(textBox);
                FieldsGrid.Children.Add(groupBox);
            }
        }

        /// <summary>
        /// Obtain genericized enumerator for the PlayingCards in this Deck.
        /// </summary>
        /// <returns>IEnumerator of PlayingCards.</returns>
        public virtual IEnumerator<string> GetEnumerator() {
            foreach (TextBox textBox in _textBoxes) {
                yield return textBox.Text;
            }
        }

        /// <summary>
        /// Obtain an enumerator for the PlayingCards in this Deck.
        /// </summary>
        /// <returns>IEnumerator of PlayingCards.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

    }
}
