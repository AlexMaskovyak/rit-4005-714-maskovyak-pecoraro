using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using ATS.Database;

namespace ATS.Database
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
              new Switcher(
                new Enable(isEnabled => {
                  Toggle.IsEnabled = Search.IsEnabled = Enter.IsEnabled = Remove.IsEnabled = isEnabled; }),
                new IAccess[]{
                  new Access(() => Current.Text, s => { Current.Text = s; }),
                  new Access(() => Size.Text, s => { Size.Text = s; }),
                  new Access(() => Names.Text, s => { Names.Text = s; }),
                  new Access(() => Phones.Text, s => { Phones.Text = s; }),
                  new Access(() => Rooms.Text, s => { Rooms.Text = s; })},
                new SetClick[]{
                  new SetClick((EventHandler e) => { Toggle.Click += new RoutedEventHandler(e); }),
                  new SetClick((EventHandler e) => { Search.Click += new RoutedEventHandler(e); }),
                  new SetClick((EventHandler e) => { Enter.Click += new RoutedEventHandler(e); }),
                  new SetClick((EventHandler e) => { Remove.Click += new RoutedEventHandler(e); }),
                  new SetClick((EventHandler e) => { Size.MouseLeftButtonDown += new MouseButtonEventHandler(e); }) },
                new WorkQueue(),
                    "local", new LocalDB<string>(), "remote", new RemoteDB());
        }
    }
}
