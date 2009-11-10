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
            new Switcher(
                new Enable(isEnabled => {
                    Toggle.Enabled = Search.Enabled = Enter.Enabled = Remove.Enabled = isEnabled; }),
                new IAccess[]{
                    new Access(() => Current.Text, s => { Current.Text = s; }),
                    new Access(() => Size.Text, s => { Size.Text = s; }),
                    new Access(() => Names.Text, s => { Names.Text = s; }),
                    new Access(() => Phones.Text, s => { Phones.Text = s; }),
                    new Access(() => Rooms.Text, s => { Rooms.Text = s; })},
                new SetClick[]{
                    new SetClick((EventHandler eh) => { Toggle.Click += new EventHandler(eh); }),
                    new SetClick((EventHandler eh) => { Search.Click += new EventHandler(eh); }),
                    new SetClick((EventHandler eh) => { Enter.Click += new EventHandler(eh); }),
                    new SetClick((EventHandler eh) => { Remove.Click += new EventHandler(eh); }),
                    new SetClick((EventHandler eh) => { Size.Load += new EventHandler(eh); }) },
                new WorkQueue(),
                    "local", new LocalDB<string>(), "remote", new RemoteDB());
        }
    }
}
