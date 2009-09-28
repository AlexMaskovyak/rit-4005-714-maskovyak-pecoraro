using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_SudokuModel
{
    using System;

    public class DelegateDemo
    {
        delegate String DelegateAbc(String txt);

        public String TestAbc(String txt)
        {
            Console.WriteLine(txt);
            return "Hello from TestAbc";
        }
        public static String TestStatic(String txt)
        {
            Console.WriteLine(txt);
            return "Hello from TestStatic";
        }
        static void Main()
        {
            DelegateDemo t1 = new DelegateDemo();
            DelegateAbc d1 = new DelegateAbc(t1.TestAbc);
            Console.WriteLine(d1("First call"));
            d1 += new DelegateAbc(DelegateDemo.TestStatic);
            Console.WriteLine(d1("Second call"));

        }
    }

}
