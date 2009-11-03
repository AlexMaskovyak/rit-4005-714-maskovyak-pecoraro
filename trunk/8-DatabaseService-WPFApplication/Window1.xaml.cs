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
using System.ComponentModel;

using _7_Database;

namespace _8_DatabaseWebService
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {

// Constants

        /// <summary> mode to display for the Local Database </summary>
        const string LocalDBMode = "Local";

        /// <summary> mode to display for the Remote Database </summary>
        const string RemoteDBMode = "Remote";

// Fields

        /// <summary> local database facade </summary>
        protected IModel<string> _local;

        /// <summary> remote database facade </summary>
        protected IModel<string> _remote;

        /// <summary> current database to use </summary>
        protected IModel<string> _active;

        /// <summary> state of the UI </summary>
        protected bool _isLocal;

// Constructors

        /// <summary> default constructor </summary>
        public Window1() : this( "Name", "Phone", "Room" ) {}

        /// <summary> constructor with names for tuple fields </summary>
        /// <param name="fields"> the tuple field names </param>
        public Window1(params string[] fields) {
            InitializeComponent();
            FieldsControl.AddFields(fields);

            // NOTE: These are created lazily, as needed, in switchTo*()
            _local = null;
            _remote = null;
            _active = null;

            // Initialize with Local, sets active as well
            SwitchToLocal();
        }

// Factory Methods

        /// <summary> factory method to create the "local" database </summary>
        /// <returns> a new database </returns>
        protected virtual IModel<string> createLocalDatabase() {
            return new LocalDB();
        }

        /// <summary> factory method to create the "remote" database </summary>
        /// <returns> a new database </returns>
        protected virtual IModel<string> createRemoteDatabase() {
            return new RemoteDB();
        }

// UI Methods
// NOTE: Each of these should be called from the GUI thread

        /// <summary> display the local database </summary>
        protected virtual void SwitchToLocal() {
            if (_isLocal)
                return;

            if (_local == null)
                _local = createLocalDatabase();

            _isLocal = true;
            DBMode.Text = LocalDBMode;
            SwitchToDatabase(_local);
        }

        /// <summary> display the remote database </summary>
        protected virtual void SwitchToRemote() {
            if (!_isLocal)
                return;

            if (_remote == null)
                _remote = createRemoteDatabase();

            _isLocal = false;
            DBMode.Text = RemoteDBMode;
            SwitchToDatabase(_remote);
        }

        /// <summary> generic method for displaying a database </summary>
        /// <param name="database"> the database to display information for </param>
        protected virtual void SwitchToDatabase(IModel<string> database) {

            // Set the current
            _active = database;

            // UI Updates - Clear fields, disable buttons
            ToggleButtons(false);
            FieldsControl.Clear();

            // Get the Size and Reupdate the UI once we've gotten that information
            // NOTE: this is in a background worker because the DB may be remote.
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e) {
                int size = database.Size;
                Size.Dispatcher.BeginInvoke(new Action(delegate() {
                    Size.Text = size.ToString();
                    ToggleButtons(true);
                }));
            };
            worker.RunWorkerAsync();

        }

        /// <summary> Toggle the IsEnabled state of all UI buttons </summary>
        /// <param name="enabled"> true to enable, false to disable </param>
        protected virtual void ToggleButtons(bool enabled) {
            Toggle.IsEnabled = enabled;
            Search.IsEnabled = enabled;
            Enter.IsEnabled  = enabled;
            Remove.IsEnabled = enabled;
        }

// Helpers

        /// <summary> get the first lines from each textbox </summary>
        /// <returns> an array of the first lines </returns>
        protected virtual string[] GetFirstLines() {
            List<string> keys = new List<string>();
            foreach (string s in FieldsControl) {
                int index = s.IndexOf("\n");
                if (index < 0) {
                    keys.Add(s);
                } else {
                    keys.Add(s.Substring(0, index));
                }
            }
            return keys.ToArray<string>();
        }

// Button Listeners

        /// <summary> clicked Toggle Button - Switch Databases </summary>
        protected virtual void Toggle_Click(object sender, RoutedEventArgs e) {
            if (_isLocal) {
                SwitchToRemote();
            } else {
                SwitchToLocal();
            }
        }

        /// <summary> clicked Enter Button - Add Tuple </summary>
        protected virtual void Enter_Click(object sender, RoutedEventArgs e) {

            // Get Values to Send
            string[] tuple = GetFirstLines();
            ToggleButtons(false);

            // DEBUG - Remove after testing.
            Console.WriteLine(tuple.Length);
            foreach (string s in tuple) Console.WriteLine("[{0}]", s);

            // Add and Update Size
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate(object s, DoWorkEventArgs d) {
                _active.Enter(tuple);
                int size = _active.Size;
                Size.Dispatcher.BeginInvoke(new Action(delegate() {
                    Size.Text = size.ToString();
                    ToggleButtons(true);
                }));
            };
            worker.RunWorkerAsync();

        }

        /// <summary> clicked Remove Button - Remove Tuple </summary>
        private void Remove_Click(object sender, RoutedEventArgs e) {

            // Get Values to Send
            string[] tuple = GetFirstLines();
            ToggleButtons(false);

            // DEBUG - Remove after testing.
            Console.WriteLine(tuple.Length);
            foreach (string s in tuple) Console.WriteLine("[{0}]", s);

            // Add and Update Size
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate(object s, DoWorkEventArgs d) {
                _active.Remove(tuple);
                int size = _active.Size;
                Size.Dispatcher.BeginInvoke(new Action(delegate() {
                    Size.Text = size.ToString();
                    ToggleButtons(true);
                }));
            };
            worker.RunWorkerAsync();

        }

    }
}
