namespace WFViKy
{

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="CT">Core Type ... Type Of core</typeparam>
    public class CTRL<CT> where CT : Control, new()
    {
        public static CTRL<CT> LastObj;
        public CT Core;
        public CTRL() : this(new CT())
        {

        }
        public struct ColorsDef
        {

            public static readonly Color BackUp = Color.FromArgb(77, 77, 77);
            public static readonly Color ForeUp = Color.FromArgb(243, 243, 243);

            public static readonly Color BackMouseOn = Color.FromArgb(14, 151, 123);
            public static readonly Color ForeMouseOn = Color.Black;

            public static readonly Color BackDown = Color.Green;
            public static readonly Color ForeDown = Color.Black;



            public static readonly Color BackKeepHoldingDown= Color.Orange;
            public static readonly Color ForeKeepHoldingDown = Color.DarkCyan;

            public static readonly Color CapsLedOnBabk = Color.Brown;
            public static readonly Color CapsLedOnFore = Color.Orange;



        }
        public CTRL(CT c)
        {
            Core = c;

            c.Tag = this;

            LastObj = this;
            c.ForeColor = ColorsDef.ForeUp;
            c.BackColor = ColorsDef.BackUp;

            Core.Dock = DockStyle.Fill;

        }

        public static implicit operator CTRL<CT>(CT c)
        {
            if (c.Tag == null)
                return new CTRL<CT>(c);//not good enough has to be modified .. read from list for those who already created or .. cant every time create a new one. stupid
            else
                return (CTRL<CT>)c.Tag;
        }
        public static implicit operator CT(CTRL<CT> c)
        {
            return c.Core;
        }

    }
    public class BTN : CTRL<Button>
    {
        public class statuse
        {
        }

        public kbdAbstract.Row.Button btnConcept;
        public BTN()
        {
            AutoTextResize = true;
            var b = this.Core;

            b.TextAlign = ContentAlignment.MiddleCenter;
            b.UseMnemonic = false;


            if (AutoTextResize)
                b.Resize += Btn_Resize;
            b.MouseEnter += B_MouseEnter;
            b.MouseLeave += B_MouseLeave;


        }


        public void B_MouseEnter(object? sender, EventArgs e)
        {
            Button b = ((Button)sender);
            if (b.BackColor != ColorsDef.BackKeepHoldingDown && b.BackColor != ColorsDef.BackDown && b.BackColor != ColorsDef.CapsLedOnBabk)
            {

                b.BackColor = ColorsDef.BackMouseOn;

                b.ForeColor = ColorsDef.ForeMouseOn;
            }
        }
        public void B_MouseLeave(object? sender, EventArgs e)
        {
            Button b = ((Button)sender);
            if (b.BackColor != ColorsDef.BackKeepHoldingDown && b.BackColor != ColorsDef.BackDown && b.BackColor != ColorsDef.CapsLedOnBabk )
            {

                b.BackColor = ColorsDef.BackUp;

                b.ForeColor = ColorsDef.ForeUp;
            }
        }

        public bool AutoTextResize { get; set; }

        public void Btn_Resize(object? sender, EventArgs e)
        {
            if (AutoTextResize)
            {

                var btn = ((Button)(sender));
                btn.SuspendLayout();
                string[] linesOfText = btn.Text.Split("\n");
                int MaxCols = 0;
                foreach (var line in linesOfText)
                {
                    if (line.Length > MaxCols)
                        MaxCols = line.Length;
                }
                try
                {
                    float HSize = btn.Width / (MaxCols * 1.5f),
                    VSize = btn.Height / (linesOfText.Length * 3.5f);
                    btn.Font = new Font(btn.Font.FontFamily, Math.Min(HSize, VSize));
                }
                catch { }
                btn.ResumeLayout();


            }
        }

    }
    public class TBLP : CTRL<TableLayoutPanel>
    {
        public TBLP()
        {

        }

    }
    public class FRM : CTRL<MSBASE.From>
    {
        public FRM()
        {

        }
    }
    public class PICBX : CTRL<PictureBox>
    {
        public PICBX()
        {
            Core.SizeMode = PictureBoxSizeMode.StretchImage;
            Core.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
    public class MnuStrip : CTRL<MenuStrip>
    {
        public MnuStrip()
        {

        }
    }
    public class MSBASE
    {
        public class From : Form
        {

            public From()
            {
                this.DoubleBuffered = true;
            }
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams param = base.CreateParams;
                    param.ExStyle |= 0x08000000;
                    return param;
                }
            }
        }
    }


}