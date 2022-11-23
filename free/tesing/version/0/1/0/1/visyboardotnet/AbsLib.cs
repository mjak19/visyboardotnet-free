using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFViKy
{
    /// <summary>
    /// libraries portable for any project
    /// </summary>
    internal class Lib
    {
        public class Logic
        {

            public static bool equalOr<T1, T2>(T1[] oprands1, T2[] Operands2)
            {
                foreach (T1 oprand1 in oprands1)
                {
                    foreach (T2 oprand2 in Operands2)
                    {
                        if (oprand1.Equals(oprand2))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            public static bool equalAnd<T1, T2>(T1[] oprands1, T2[] Operands2, Func<T1, T2, bool> func)
            {
                foreach (T1 oprand1 in oprands1)
                {
                    foreach (T2 oprand2 in Operands2)
                    {
                        if (! oprand1.Equals(oprand2))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

    }
}
