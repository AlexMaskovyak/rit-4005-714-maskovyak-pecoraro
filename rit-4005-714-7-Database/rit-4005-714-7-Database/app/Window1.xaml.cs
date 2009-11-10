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

namespace ATS.Database {
  /// <summary> controller. </summary>
  public partial class Window1: Window {
    /// <summary> connects to <c>Switcher</c>. </summary>
    public Window1 () {
      InitializeComponent();
      new Switcher(
        new Enable(isEnabled => {
          Toggle.IsEnabled = Search.IsEnabled = Enter.IsEnabled = Remove.IsEnabled = isEnabled; }),
        new IAccess[]{
          new Access(() => Current.Header.ToString(), s => { Current.Header = s; }),
          new Access(() => Size.Text, s => { Size.Text = s; }),
          new Access(() => Names.Text, s => { Names.Text = s; }),
          new Access(() => Phones.Text, s => { Phones.Text = s; }),
          new Access(() => Rooms.Text, s => { Rooms.Text = s; })},
        new SetClick[]{
          new SetClick((EventHandler e) => { Toggle.Click += new RoutedEventHandler(e); }),
          new SetClick((EventHandler e) => { Search.Click += new RoutedEventHandler(e); }),
          new SetClick((EventHandler e) => { Enter.Click += new RoutedEventHandler(e); }),
          new SetClick((EventHandler e) => { Remove.Click += new RoutedEventHandler(e); }),
          new SetClick((EventHandler e) => { Size.MouseDown += new MouseButtonEventHandler(e); }) },
        new WorkQueue(),
        "local", new LocalDB<string>(), "remote", new RemoteDB());
    }
  }
}
