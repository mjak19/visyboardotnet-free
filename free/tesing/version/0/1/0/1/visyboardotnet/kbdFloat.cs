using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFViKy;
namespace WFViKy
{
    public partial class kbdFloat : FRM
    {
        public kbdFloat()
        {
            /*LastObj = this;*/

            TheButton = new BTN();
            TheButton.Core.Parent = this;
            //this.Core.CreateParams(0x08000000);

        }

        public BTN TheButton;
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            kbdMain.LastObj.Core.TopMost = true;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
        }


    }
}
