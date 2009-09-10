using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using COM = System.Console;
using List = System.Collections.Generic.List<int>;
// bad: using List = System.Collections.Generic.List;
using System.Windows;

namespace _0_FirstStepsTutorial
{
    class HelloWorld
    {
        static void Main(string[] args)
        {
            COM.WriteLine("Hello, World!");
            List l = new List();
        }
    }
}
