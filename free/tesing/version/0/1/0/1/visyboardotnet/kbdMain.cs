using WindowsInput.Native;

namespace WFViKy
{

    public class kbdMain : FRM
    {
        private FRM TheSettingsForm;
        private TBLP TheTable;
        private PICBX ThePicBx;
        private BTN TheEscapeButton;
        private BTN TheShiftButton;
        private BTN TheCapsButton;
        private BTN TheControlButton;
        private MnuStrip TheMenu;
        WindowsInput.InputSimulator TheVkInSimulator = new WindowsInput.InputSimulator();


        public double Opacity
        {
            get
            {
                return base.Core.Opacity;
            }
            set
            {
                if (value > 0)
                    base.Core.Opacity = value;
            }
        }

        public kbdMain()/*: base()*/
        {


            var This = (Form)(this);
            This.SuspendLayout();
            This.Size = new Size(1400, 400);
            This.Text = "visiboard";
            This.TopMost = true;
            var kbd = new kbdAbstract(kbdAbstract.SampleKeyboardKeysConfStrs[2], "\n", " ", "''", "::");
            PictureBox picBox = ThePicBx = new PICBX();
            TableLayoutPanel tbl = TheTable = new TBLP();
            tbl.Parent = picBox;
            picBox.Parent = This;


           /* TheMenu = new MnuStrip();
            TheMenu.Core.Dock = DockStyle.Top;
            TheMenu.Core.Parent = This;
            ToolStripMenuItem item;

            item = (ToolStripMenuItem)TheMenu.Core.Items.Add("tools");
            item.DropDownItems.Add("settings").Click += KbdMain_Click; ;

            item = (ToolStripMenuItem)TheMenu.Core.Items.Add("Help");
            item.DropDownItems.Add("About");
            item.DropDownItems.Add("content");
            item.DropDownItems.Add("check update");*/

            TheGuiParse(kbd);

            This.ResizeBegin += TheGuiForm1_ResizeBegin;
            This.ResizeEnd += TheGuiForm1_ResizeEnd;
            This.MouseWheel += TheGuiMouseWheel;
            This.ResumeLayout(false);
            this.Core.FormClosed += TheGuiCore_FormClosed;
        }
        PropertyGrid ThePropertyGrid;
       /* private void KbdMain_Click(object? sender, EventArgs e)
        {
            if(TheSettingsForm==null || TheSettingsForm.Core.IsDisposed)
            {
                TheSettingsForm = new FRM();
                TheSettingsForm.Core.Text = "Settings";
                TheSettingsForm.Core.Size = new Size(400, 400);
                if (ThePropertyGrid == null)
                {
                    ThePropertyGrid = new PropertyGrid();
                }
                ThePropertyGrid.Dock = DockStyle.Fill;
                ThePropertyGrid.Parent = TheSettingsForm.Core;


                *//*TheSettingsForm.Core.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                TheSettingsForm.Core.StartPosition = FormStartPosition.CenterScreen;
                TheSettingsForm.Core.SuspendLayout();
                TheSettingsForm.Core.Controls.Add(new kbdSettings());
                TheSettingsForm.Core.ResumeLayout(false);
                TheSettingsForm.Core.FormClosed += TheGuiCore_FormClosed;*//*
            }
            ThePropertyGrid.SelectedObject = TheShiftButton.Core;

            TheSettingsForm.Core.TopMost = true;
            TheSettingsForm.Core.ShowDialog();
        }*/

        public void TheGuiParse(kbdAbstract kbd)
        {

            var container = TheTable.Core;
            int rowNumber = 0;

            container.SuspendLayout();

            foreach (var currentRow in kbd.rows)
            {
                int ColumnNumber = 0;
                container.RowCount++;
                container.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));

                foreach (var krb in currentRow.buttons)
                {
                    var BTNC = new BTN();
                    var btn = BTNC.Core;
                    (btn).SuspendLayout();


                    BTNC.btnConcept = krb;
                    BTNC.btnConcept.GuiBTN = BTNC;


                    btn.MouseDown += theGuiBtn_MouseDown; ;
                    btn.MouseUp += TheGuiBtn_MouseUp; ;
                    btn.Text = krb.strBlocks[0];
                    //Debug(krb.strBlocks[0]+ krb.NativeVK.ToString());
                    if (krb.NativeVK == VirtualKeyCode.CAPITAL)
                    {
                        //Debug("CAPS");
                        TheCapsButton = BTNC;
                    }
                    if (krb.NativeVK == VirtualKeyCode.SHIFT)
                    {
                        //Debug("SHIFT");
                        TheShiftButton = BTNC;
                    }
                    if (krb.NativeVK == VirtualKeyCode.ESCAPE)
                    {
                        //Debug("ESC");
                        TheEscapeButton = BTNC;
                    }
                    if (krb.NativeVK == VirtualKeyCode.CONTROL || krb.NativeVK == VirtualKeyCode.LCONTROL || krb.NativeVK == VirtualKeyCode.RCONTROL)
                    {
                        TheControlButton = BTNC;
                    }
                    if (TheSimKeyIsDown(BTNC.btnConcept.NativeVK))
                    {
                        BTNC.Core.BackColor = ColorsDef.BackDown;
                        BTNC.Core.ForeColor = ColorsDef.ForeDown;
                    }

                    btn.UseMnemonic = false;
                    container.Controls.Add(btn);
                    container.SetRow(btn, rowNumber);
                    container.SetColumnSpan(btn, (int)Math.Ceiling(krb.xSpan));
                    container.SetColumn(btn, ColumnNumber);
                    ColumnNumber += (int)Math.Ceiling(krb.xSpan);
                    if (ColumnNumber > container.ColumnCount)
                        container.ColumnCount = ColumnNumber;
                    btn.ResumeLayout();
                }
                rowNumber++;
            }
            for (int i = 0; i < container.ColumnCount; i++)
            {
                container.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            }

            if (TheVkInSimulator.InputDeviceState.IsTogglingKeyInEffect(TheCapsButton.btnConcept.NativeVK))
            {
                TheCapsButton.Core.ForeColor = ColorsDef.CapsLedOnFore;
                TheCapsButton.Core.BackColor = ColorsDef.CapsLedOnBabk;
            }
            container.ResumeLayout();
        }
        private void theGuiBtn_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
            }
            else if (e.Button == MouseButtons.Right)
            {
            }
            else if (e.Button == MouseButtons.Middle)
            {
            }

        }
        private void TheGuiBtn_MouseUp(object? sender, MouseEventArgs e)
        {
            var senderButton = ((Button)sender);
            var senderBTN = (BTN)senderButton;
            var vkey = senderBTN.btnConcept.NativeVK;
            var cat = TheSimKeyCat(senderBTN.btnConcept.NativeVK);
            var vkDown = TheSimKeyIsDown(vkey);
            var vkUp = TheSimKeyIsUp(vkey);
            var mouseBtn = e.Button;


            if (e.Button == MouseButtons.Left)
            {
                if (vkDown)
                {
                    TheSimKeyDoUp(senderButton);
                }
                if (cat == 0)
                {
                    if (vkUp)
                    {

                        TheSimKeyDoPress(senderBTN);

                    }
                }
                if (cat == 1)
                {

                    if (vkUp)
                    {



                        TheSimKeyDoDown(senderButton, false);
                        senderButton.Update();
                        Thread.Sleep(50);
                    }
                }
                if (cat == 2)
                {
                    if (senderBTN == TheCapsButton)
                    {
                        if (vkUp)
                        {
                            TheSimKeyDoPress(senderBTN);

                        }
                    }
                }

            }
            else if (mouseBtn == MouseButtons.Right)
            {
                //Statics.Debug("right");
                TheGuiBtn_MouseUp(TheShiftButton.Core, new MouseEventArgs(MouseButtons.Left, e.Clicks, e.X, e.Y, e.Delta));
                TheGuiBtn_MouseUp(sender, new MouseEventArgs(MouseButtons.Left, e.Clicks, e.X, e.Y, e.Delta));

            }
            else if (mouseBtn == MouseButtons.Middle)
            {
                if (cat == 1)
                {
                    if (senderBTN.Core.BackColor != ColorsDef.BackKeepHoldingDown)
                    {
                        TheSimKeyDoDown(senderButton, true);
                    }
                    else
                    {
                        TheSimKeyDoUp(senderButton);
                    }
                    //senderButton.Update();
                }
                else
                {
                    TheGuiBtn_MouseUp(TheControlButton.Core, new MouseEventArgs(MouseButtons.Left, e.Clicks, e.X, e.Y, e.Delta));
                    TheGuiBtn_MouseUp(sender, new MouseEventArgs(MouseButtons.Left, e.Clicks, e.X, e.Y, e.Delta));
                }
            }


            TheGuiSetCaseAll(TheSimKeyIsDown(TheShiftButton.btnConcept.NativeVK));



        }



        int TheSimKeyCat(VirtualKeyCode vkey)
        {
            if (vkey == VirtualKeyCode.SHIFT || vkey == VirtualKeyCode.MENU || vkey == VirtualKeyCode.LMENU || vkey == VirtualKeyCode.RMENU || vkey == VirtualKeyCode.LCONTROL || vkey == VirtualKeyCode.RCONTROL || vkey == VirtualKeyCode.CONTROL || vkey == VirtualKeyCode.LWIN || vkey == VirtualKeyCode.RWIN)
                return 1;
            else if (vkey == VirtualKeyCode.CAPITAL)
                return 2;

            return 0;
        }
        int TheSimKeyCat(Button bt)
        {
            return TheSimKeyCat((BTN)bt);
        }
        int TheSimKeyCat(BTN btn)
        {
            return TheSimKeyCat(btn.btnConcept.NativeVK);
        }

        void TheGuiSetCase(Button bt, bool LetterCase)
        {
            if (TheSimKeyCat(bt) == 0)
            {
                var strBlocks = ((BTN)bt).btnConcept.strBlocks;
                if (strBlocks.Count > 1)
                {
                    if (LetterCase)
                        bt.Text = strBlocks[1];
                    else bt.Text = strBlocks[0];
                }
                else if (bt.Text.Length == 1)
                {
                    if (char.IsLetter(bt.Text[0]))
                    {
                        if (LetterCase ^ TheVkInSimulator.InputDeviceState.IsTogglingKeyInEffect(TheCapsButton.btnConcept.NativeVK))
                        {
                            bt.Text = bt.Text.ToUpper();
                        }
                        else
                        {
                            bt.Text = bt.Text.ToLower();
                        }
                    }
                }
                else
                {
                    bt.Text = string.Join("\n", bt.Text.Split("\n").Reverse());
                }
            }
        }

        private void TheGuiMouseWheel(object? sender, MouseEventArgs e)
        {
            this.Opacity += (double)e.Delta / 500;
        }

        private void TheGuiForm1_ResizeEnd(object? sender, EventArgs e)
        {
            TheTable.Core.Visible = true;
        }

        private void TheGuiForm1_ResizeBegin(object? sender, EventArgs e)
        {
            TableLayoutPanel tbl = TheTable;
            Bitmap bmp = new Bitmap(tbl.Width, tbl.Height);
            tbl.DrawToBitmap(bmp, tbl.DisplayRectangle);
            tbl.CreateGraphics();

            ThePicBx.Core.BackgroundImage = bmp;

            TheTable.Core.Visible = false;
        }

        private void TheGuiCore_FormClosed(object? sender, FormClosedEventArgs e)
        {
            TheSimKeyDoUpAllCat1And2();
        }



        private void TheSimKeyDoUp(Button senderButton)
        {
            var vkey = ((BTN)senderButton).btnConcept.NativeVK;
            TheVkInSimulator.Keyboard.KeyUp(vkey);

            //TheGuiUpdatePaint(senderButton, false);
            senderButton.BackColor = ColorsDef.BackUp;
            senderButton.ForeColor = ColorsDef.ForeUp;

            /*if(senderButton==TheShiftButton.Core)
            {
                TheGuiSetCaseAll(TheSimKeyIsDown(TheShiftButton.btnConcept.NativeVK));
            }*/

            if (TheSimKeyCat(((BTN)senderButton).btnConcept.NativeVK) == 0)
            {
                TheSimKeyDoUpAllCat1And2(false);
            }
        }
        private void TheGuiSetCaseAll(bool LetterCase)
        {
            foreach (Button EachBt in TheTable.Core.Controls)
            {
                var strblocks = ((BTN)EachBt).btnConcept.strBlocks;
                if (strblocks.Count > 1 || (strblocks[0].Length == 1 && char.IsLetterOrDigit(strblocks[0][0])))
                {
                    TheGuiSetCase(EachBt, LetterCase);
                }
            }
        }
        private void TheSimKeyDoUpAllCat1And2(bool includingKeepHolding = false)
        {
            foreach (Button EachButton in TheTable.Core.Controls)
            {
                var EachBtn = (BTN)EachButton;
                var EachVk = EachBtn.btnConcept.NativeVK;
                if (TheSimKeyCat(EachVk) > 0 && TheSimKeyIsDown(EachVk))
                {
                    if (includingKeepHolding || !(EachBtn.Core.BackColor == ColorsDef.BackKeepHoldingDown))
                        TheSimKeyDoUp(EachButton);
                }
            }

        }

        void TheSimKeyDoPress(BTN SenderBtn)
        {
            var senderButton = SenderBtn.Core;
            var krb = SenderBtn.btnConcept;
            var vkey = krb.NativeVK;



            Color currentBack = senderButton.BackColor, currentFore = senderButton.ForeColor;
            senderButton.BackColor = ColorsDef.BackDown;//because mouse is already on button and fore and back ground already meant to be replaced
            senderButton.ForeColor = ColorsDef.ForeDown;
            senderButton.Update();
            Thread.Sleep(50);
            if (vkey == (VirtualKeyCode)Keys.None)
                this.Core.Text = "not handled" + (senderButton.Text);
            else
                TheVkInSimulator.Keyboard.KeyPress(vkey);
            senderButton.BackColor = currentBack;
            senderButton.ForeColor = currentFore;
            senderButton.Update();

            if (TheSimKeyCat(vkey) == 0)
            {
                TheSimKeyDoUpAllCat1And2();
            }
            if (SenderBtn == TheCapsButton)
            {
                if (TheVkInSimulator.InputDeviceState.IsTogglingKeyInEffect(vkey))
                {
                    senderButton.BackColor = ColorsDef.CapsLedOnBabk;
                    senderButton.ForeColor = ColorsDef.CapsLedOnFore;
                }
                else
                {
                    senderButton.BackColor = ColorsDef.BackUp;
                    senderButton.ForeColor = ColorsDef.ForeUp;
                }
            }
        }

        bool TheSimKeyIsDown(VirtualKeyCode vk)
        {
            return TheVkInSimulator.InputDeviceState.IsKeyDown(vk);
        }

        bool TheSimKeyIsUp(VirtualKeyCode vk)
        {
            return TheVkInSimulator.InputDeviceState.IsKeyUp(vk);
        }



        private void TheSimKeyDoDown(Button senderButton, bool keepHolding)
        {
            var vkey = ((BTN)senderButton).btnConcept.NativeVK;
            if (vkey == (VirtualKeyCode)Keys.None)
            {
                this.Core.Text = "not handled" + (senderButton.Text);
                return;
            }

            if (TheSimKeyCat(vkey) == 0)
            {
                TheSimKeyDontBecauseItIsIllegal("its illegal to hold a cat 0 key down but instead you can just simply press it that press guarantees that key isn't hold down that might cause an unwanted repetition, also you can force down under your own responsibility however its not recommended", senderButton);
                return;
            }
            if (TheSimKeyCat(vkey) > 0)
            {
                if (keepHolding)
                {
                    senderButton.BackColor = ColorsDef.BackKeepHoldingDown;
                    senderButton.ForeColor = ColorsDef.ForeKeepHoldingDown;
                }
                else
                {
                    senderButton.BackColor = ColorsDef.BackDown;
                    senderButton.ForeColor = ColorsDef.ForeDown;
                }
                TheVkInSimulator.Keyboard.KeyDown(vkey);
                /*if (senderButton == TheShiftButton.Core)
                    TheGuiSetCaseAll(TheSimKeyIsDown(TheShiftButton.btnConcept.NativeVK));
                if (vkey == VirtualKeyCode.CAPITAL)
                {
                    foreach (Button EachBt in TheTable.Core.Controls)
                    {
                        var strblocks = ((BTN)EachBt).btnConcept.strBlocks;
                        if (strblocks.Count == 1 && (strblocks[0].Length == 1 && char.IsLetter(strblocks[0][0])))
                        {
                            TheGuiSetCase(EachBt, TheSimKeyIsDown(vkey));
                        }
                    }
                }*/
            }
        }

        private void TheSimKeyDontBecauseItIsIllegal(string v, Button? senderButton)
        {
            throw new NotImplementedException(v);
        }

    }
}