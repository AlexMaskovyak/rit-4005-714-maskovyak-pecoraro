using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _4_SudokuView_UserControlLibrary
{
    public interface ISudokuViewCell
    {
        void Click(System.Object sender, EventArgs e);
        void Update();
    }
}
