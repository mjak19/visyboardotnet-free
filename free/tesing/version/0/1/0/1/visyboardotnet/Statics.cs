using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFViKy
{
    internal class Statics
    {
        private static readonly bool toDebug = false;
        public static void Debug(string message="debug")
        {
            if  (toDebug)
                MessageBox.Show(message);
        }
        public static void report(Exception ex)
        {
            Debug(ex.ToString());
        }

    }
}
