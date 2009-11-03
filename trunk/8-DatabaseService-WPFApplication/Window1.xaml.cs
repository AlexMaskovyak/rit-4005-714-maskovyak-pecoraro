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

using _7_Database;

namespace _8_DatabaseWebService
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {

// Constants

        const string LocalDBMode = "Local";
        const string RemoteDBMode = "Remote";

// Fields

        /// <summary> local database facade </summary>
        protected IModel<string> _local;

        /// <summary> remote database facade </summary>
        protected IModel<string> _remote;

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

            // Initialize with Local
            switchToLocal();
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

        /// <summary> display the local database </summary>
        protected virtual void switchToLocal() {
            if (_isLocal)
                return;

            if (_local == null)
                _local = createLocalDatabase();

            _isLocal = true;
            DBMode.Text = LocalDBMode; // needs GUI thread
            switchToDatabase(_local);
        }

        /// <summary> display the remote database </summary>
        protected virtual void switchToRemote() {
            if (!_isLocal)
                return;

            if (_remote == null)
                _remote = createRemoteDatabase();

            _isLocal = false;
            DBMode.Text = RemoteDBMode; // needs GUI thread
            switchToDatabase(_remote);
        }

        /// <summary> generic method for displaying a database </summary>
        /// <param name="database"> the database to display </param>
        protected virtual void switchToDatabase(IModel<string> database) {
            // TODO: Joe is implementing now
        }


// Button Listeners

        /// <summary> clicked Toggle Button </summary>
        private void Toggle_Click(object sender, RoutedEventArgs e) {
            if (_isLocal) {
                switchToRemote();
            } else {
                switchToLocal();
            }
        }



    }
}
