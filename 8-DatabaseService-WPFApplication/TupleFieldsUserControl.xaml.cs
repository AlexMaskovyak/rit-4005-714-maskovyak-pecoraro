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
    public partial class TupleFieldsUserControl : System.Windows.Controls.UserControl, IEnumerable<string> {
        
// Fields

        /// <summary> holds quick access to the TextBoxes of this control. </summary>
        protected ICollection<TextBox> _textBoxes;

// Constructors
       
        /// <summary> convenience constructor. </summary>
        public TupleFieldsUserControl() : this(0) {}

        /// <summary> convenience constructor. </summary>
        /// <param name="numberOfFields"> number of blank fields to create. </param>
        public TupleFieldsUserControl(int numberOfFields)
            : this(new string[numberOfFields]) {}

        /// <summary> main constructor. </summary>
        /// <param name="fieldNames"></param>
        public TupleFieldsUserControl(ICollection<string> fieldNames) {
            _textBoxes = new List<TextBox>();

            InitializeComponent();
            AddFields(fieldNames);
        }

// Public Methods
        
        /// <summary> adds a single blank field. </summary>
        public virtual void AddField() {
            AddFields(1);
        }

        /// <summary> adds a number of blank fields. </summary>
        /// <param name="numberOfFields"> number of fields to add. </param>
        public virtual void AddFields(int numberOfFields) {
            AddFields(new string[numberOfFields]);
        }

        /// <summary> adds a number of fields with the specific headers. </summary>
        /// <param name="fieldNames"> collection of field names. </param>
        public virtual void AddFields(IEnumerable<string> fieldNames) {
            foreach (string name in fieldNames) {

                // placement
                int top = _textBoxes.Count * 100;

                // create groupbox with header, could be blanks
                GroupBox groupBox = new GroupBox();
                groupBox.Header = name;
                groupBox.Visibility = Visibility.Visible;
                groupBox.VerticalAlignment = VerticalAlignment.Top;
                groupBox.Margin = new Thickness(0, top, 0, 0);
                groupBox.Height = 100;
                
                // create the textfield and save it
                TextBox textBox = new TextBox();
                textBox.Visibility = Visibility.Visible;
                textBox.Height = 50;
                textBox.Margin = new Thickness(0);
                groupBox.Content = textBox;
                _textBoxes.Add(textBox);
                
                // update the UI
                FieldsGrid.Children.Add(groupBox);
                this.Height += 100;
            }

            this.UpdateLayout();
        }

        /// <summary> clear the textboxes </summary>
        public void Clear() {
            foreach (TextBox t in _textBoxes) {
                t.Clear();
            }
        }


// Enumeration

        /// <summary> obtain genericized enumerator for the strings held in the fields of this control. </summary>
        /// <returns> enumerator of strings. </returns>
        public virtual IEnumerator<string> GetEnumerator() {
            foreach (TextBox textBox in _textBoxes) {
                yield return textBox.Text;
            }
        }

        /// <summary> obtain an enumerator for the strings contained in the fields of this control. </summary>
        /// <returns> enumerator of strings. </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

    }
}
