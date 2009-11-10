using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ATS.Database;

namespace page
{

    /// <summary> Default.aspx page handlers </summary>
    public partial class _Default : System.Web.UI.Page {

// Constants

        /// <summary> maintain current state in the session </summary>
        const string CurrentKey = "current";

        /// <summary> access to the Switcher in the session </summary>
        const string SwitcherKey = "switcher";

        /// <summary> session local database </summary>
        const string SessionLocalKey = "sessionLocal";

        /// <summary> application remote database </summary>
        const string ApplicationRemoteKey = "applicationRemote";

        /// <summary> application local database </summary>
        const string ApplicationLocalKey = "applicationLocal";

// Page Events

        /// <summary> set the state of the switcher for this session </summary>
        protected void Page_Load(object sender, EventArgs e) {
            lock (Session) {
                int current = (int)Session[CurrentKey];
                ((Switcher)Session[SwitcherKey]).Current = current;
            }
        }

        /// <summary> store the state of the switcher for this session </summary>
        protected void Page_Unload(object sender, EventArgs e) {
            lock (Session) {
                Session[CurrentKey] = ((Switcher)Session[SwitcherKey]).Current;
            }
        }

        /// <summary> setup the initial session and application databases </summary>
        protected void Page_Init(object sender, EventArgs e) {
            Application.Lock();
            try {

                if (Application[ApplicationRemoteKey] == null)
                    Application[ApplicationRemoteKey] = new RemoteDB();

                if (Application[ApplicationLocalKey] == null)
                    Application[ApplicationLocalKey] = new LocalDB<string>();

                lock (Session) {
                    if (Session[SessionLocalKey] == null)
                        Session[SessionLocalKey] = new LocalDB<string>();

                    if (Session[CurrentKey] == null)
                        Session[CurrentKey] = 0;

                    Session[SwitcherKey] = new Switcher(
                        new Enable(isEnabled => {
                            Toggle.Enabled = Search.Enabled = Enter.Enabled = Remove.Enabled = isEnabled;
                        }),
                        new IAccess[]{
                            new Access(() => Current.Text, s => { Current.Text = s; }),
                            new Access(() => Size.Text, s => { Size.Text = s; }),
                            new Access(() => Names.Text, s => { Names.Text = s; }),
                            new Access(() => Phones.Text, s => { Phones.Text = s; }),
                            new Access(() => Rooms.Text, s => { Rooms.Text = s; })},
                        new SetClick[]{
                            new SetClick((EventHandler eh) => { Toggle.Click += eh; }),
                            new SetClick((EventHandler eh) => { Search.Click += eh; }),
                            new SetClick((EventHandler eh) => { Enter.Click += eh; }),
                            new SetClick((EventHandler eh) => { Remove.Click += eh; }),
                            new SetClick((EventHandler eh) => { Size.Load += eh; }) },
                        new WorkQueue(),
                            "session local DB", Session[SessionLocalKey],
                            "application local DB", Application[ApplicationLocalKey],
                            "application remote DB", Application[ApplicationRemoteKey]);
                }
                
            } finally {
                Application.UnLock();
            }
        }
    }
}
