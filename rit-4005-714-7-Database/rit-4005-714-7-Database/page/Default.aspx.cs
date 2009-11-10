using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ATS.Database;

namespace page
{
    public partial class _Default : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            int current = (int)Session["current"];
            ((Switcher)Session["switcher"]).Current = current;
        }

        protected void Page_Unload(object sender, EventArgs e) {
            Session["current"] = ((Switcher)Session["switcher"]).Current;
        }

        protected void Page_Init(object sender, EventArgs e) {
            Application.Lock();
            try {
                if (Session["db"] == null) Session["db"] = new LocalDB<string>();
                if (Session["current"] == null) Session["current"] = 0;
                if (Application["dbLocal"] == null) Application["dbLocal"] = new LocalDB<string>();
                if (Application["dbRemote"] == null) Application["dbRemote"] = new RemoteDB();

                Session["switcher"] = new Switcher(
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
                        "session local DB", Session["db"], 
                        "application local DB", Application["dbLocal"], 
                        "application remote DB", Application["dbRemote"] );
                
            } finally {
                Application.UnLock();
            }
        }
    }
}
