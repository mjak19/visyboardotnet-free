using WFViKy;
using System;
using WindowsInput.Native;
using WindowsInput;
namespace WFViKy
{
    public class kbdAbstract
    {

        public kbdAbstract(string KeyboardKeysConfStr, string rowDelimiter, string buttonDelimiter, string multiCharDelimiter, string xSpanDelimiter)
        {
            renderFrom(KeyboardKeysConfStr, rowDelimiter, buttonDelimiter, multiCharDelimiter, xSpanDelimiter);
        }

        public class Row
        {
            public void render(string conf, string buttonDelimiter, bool alsoCase, string multiCharSymbol, string xspanDelimiter, ref bool Multichar)
            {
                foreach (var buttonConfStr in conf.Split(buttonDelimiter))
                {
                    var krb = new kbdAbstract.Row.Button();
                    krb.ParseFrom(buttonConfStr, alsoCase, multiCharSymbol, xspanDelimiter, ref Multichar);
                    this.buttons.Add(krb);
                }
            }
            public class Button
            {

                public void ParseFrom(string conf, bool alsoCase, string multiCharSymbol, string xspanDelimiter, ref bool Multichar)
                {

                    var parts = conf.Split(multiCharSymbol);
                    var partNumber = 0;
                    foreach (var part in parts)
                    {
                        if (partNumber > 0) //''tng''''stng'' ''hh''
                            Multichar = !Multichar;
                        var alterable_part = part;
                        if (alterable_part.Length > 0)
                        {
                            if (alterable_part.Contains(xspanDelimiter))
                            {
                                this.xSpan = double.Parse(alterable_part.Split(xspanDelimiter)[1]);
                                alterable_part = alterable_part.Replace(alterable_part.Split(xspanDelimiter)[1], "");
                                alterable_part = alterable_part.Replace(xspanDelimiter, "");
                            }
                            if (!Multichar)
                            {

                                foreach (char ch in alterable_part)
                                {

                                    strBlocks.Add(ch.ToString().ToLower());

                                }
                            }
                            else
                            {
                                strBlocks.Add(alterable_part);
                            }
                        }


                        partNumber++;
                    }
                    try
                    {
                        var mpk = MapKey(strBlocks[DefaulBlockIdx]);
                        //Debug("map-key mpk = "+mpk.ToString());
                        this.NativeVK = KeyMap(mpk);
                    }
                    catch (Exception ex)
                    {
                        Statics.report(ex);
                    }
                }
                public List<string> strBlocks = new List<string>();
                public int DefaulBlockIdx = 0;
                public BTN GuiBTN;
                public VirtualKeyCode NativeVK;

                public static Keys MapKey(WindowsInput.Native.VirtualKeyCode vk)
                {
                    //already(previously) known
                    if (VKToKey.ContainsKey(vk))
                        return VKToKey[vk];

                    //similarity reasons name:
                    //could have been exactly same
                    if (Enum.TryParse<Keys>(vk.ToString(), true, out Keys k))
                    {
                        VKToKey.Add(vk, k);
                        return k;
                    }


                    /*//if there is no duplicate.duplicates makes it harder.
                    //referring to an already made database. gradually
                    //dynamic reasoning*/

                    //code
                    return (Keys)(int)vk;
                }
                public static WindowsInput.Native.VirtualKeyCode KeyMap(string str)
                {
                    return (KeyMap(MapKey(str)));

                }
                public static WindowsInput.Native.VirtualKeyCode KeyMap(Keys key)
                {
                    /*if (vk == Keys.D1)
                         Debug(vk.ToString());*/
                    if (KeyToVK.ContainsKey(key))
                    {
                        /*if (vk == Keys.D1)
                            Debug(vk.ToString());*/
                        return KeyToVK[key];
                    }
                    //similarity reasons name:
                    //could have been exactly same
                    if (Enum.TryParse<WindowsInput.Native.VirtualKeyCode>(key.ToString().Replace("D", "VK_"), true, out WindowsInput.Native.VirtualKeyCode k))
                    {
                        KeyToVK.Add(key, k);
                        return k;
                    }
                    if (Enum.TryParse<WindowsInput.Native.VirtualKeyCode>(key.ToString(), true, out k))
                    {
                        KeyToVK.Add(key, k);
                        return k;
                    }


                    /*//if there is no duplicate.duplicates makes it harder.
                    //referring to an already made database. gradually
                    //dynamic reasoning*/

                    //code
                    return (WindowsInput.Native.VirtualKeyCode)(int)key;
                }
                public static Keys MapKey(string kstr)
                {
                    Keys k;
                    try
                    {
                        if (strToKey.ContainsKey(kstr))
                        {
                            //Debug("found");
                            return strToKey[kstr];
                        }
                    }
                    catch (Exception ex)
                    {
                        Statics.Debug("catched:\n"+ex.ToString());
                    }
                    if (Enum.TryParse<Keys>("D" + kstr, true, out k))
                    {
                        /*if (vk == "1")
                            Debug(k.ToString());*/
                        strToKey.Add(kstr, k);
                        return k;
                    }
                    if (Enum.TryParse<Keys>(kstr, true, out k))
                    {

                        strToKey.Add(kstr, k);
                        return k;
                    }
                    return Keys.None;

                }
                public static string MapKey(Keys vk)
                {
                    //already(previously) known
                    if ((keyToCode.ContainsKey(vk)))
                        return keyToCode[vk];
                    //similarity reasons name:
                    //could have been exactly same
                    if (Enum.TryParse<Keys>(vk.ToString(), true, out Keys k))
                    {
                        keyToCode.Add(vk, k.ToString());
                        return k.ToString();
                    }
                    return Keys.None.ToString();
                }



                private static Dictionary<string, Keys> strToKey = new Dictionary<string, Keys>
                {
                    { "⇧", Keys.Shift },
                    { "-", Keys.OemMinus },
                    { "=", Keys.Oemplus },
                    { "/", Keys.OemQuestion },
                    { "⏎", Keys.Enter },
                    { "❖", Keys.LWin },
                    { "space", Keys.Space },
                    { "⌫", Keys.Back },
                    { "⌦", Keys.Delete },
                    { "␛", Keys.Escape },
                    { "⭾", Keys.Tab },
                    { "⇪", Keys.CapsLock },
                    { "⎙", Keys.PrintScreen },
                    { "CTRL", Keys.Control },
                    { "↑", Keys.Up },
                    { "←", Keys.Left },
                    { "→", Keys.Right },
                    { "↓", Keys.Down },
                    { "☰", Keys.Apps },
                    { "`", Keys.Oemtilde },
                    { "[", Keys.OemOpenBrackets },
                    { "]", Keys.OemCloseBrackets},
                    { ";", Keys.OemSemicolon },
                    { "'", Keys.OemQuotes },
                    { "\\", Keys.OemPipe },
                    { ",", Keys.Oemcomma },
                    { ".", Keys.OemPeriod }
                };

                private static Dictionary<Keys, VirtualKeyCode> KeyToVK = new Dictionary<Keys, WindowsInput.Native.VirtualKeyCode>
                {
                    { Keys.Alt, VirtualKeyCode.MENU },
                    { Keys.LMenu, VirtualKeyCode.LMENU },
                    { Keys.Menu, VirtualKeyCode.MENU },
                    { Keys.RMenu, VirtualKeyCode.RMENU },
                    { Keys.D1, VirtualKeyCode.VK_1 },
                    { Keys.CapsLock, VirtualKeyCode.CAPITAL },
                    { Keys.Shift, VirtualKeyCode.SHIFT },
                    { Keys.Oemplus, VirtualKeyCode.OEM_PLUS },

                };

                private static Dictionary<Keys, string> keyToCode = new Dictionary<Keys, string> { { Keys.LWin, "❖" }, { Keys.Return, "ENTER" }, { Keys.Back, "BACKSPACE" }, { Keys.Escape, "ESC" }, { Keys.Tab, "TAB" }, { Keys.Space, " " }, { Keys.Shift, "Shift" } };

                private static Dictionary<WindowsInput.Native.VirtualKeyCode, Keys> VKToKey = new Dictionary<WindowsInput.Native.VirtualKeyCode, Keys> ();

                // public static readonly int defaultSpan = 4;
                //public static readonly double defaultXSpan = 4;
                public double xSpan = 1;
                //public static readonly int defaultYSpan = 1;
                //public int ySpan = defaultYSpan;
            }
            public List<Button> buttons = new List<Button>();
        }

        public List<Row> rows = new List<Row>();

        public static readonly string[] SampleKeyboardKeysConfStrs = {
@"''ESC f1 f2 f3 f4 f5 f6 f7 f8 f9 f10 f11 f12''
`~ 1!"
        ,
@"''ESC f1 f2 f3 f4 f5 f6 f7 f8 f9 f10 f11 f12''
`~ 1! 2@ 3# 4$ 5% 6^ 7& 8* 9( 0) -_ =+ ''backsp''''del''"
    ,
@"␛ ''f1 f2 f3 f4 f5 f6 f7 f8 f9 f10 f11 f12'' ⌫
`~ 1! 2@ 3# 4$ 5% 6^ 7& 8* 9( 0) -_ =+ ⌦
⭾::2 Q W E R T Y U I O P [{ ]}
⇪::2 A S D F G H J K L ;: '" + "\" ⏎" + @"
⇧::2 Z X C V B N M ,< .> ↑ /? \|
''CTRL ❖ ALT space::6'' ☰ ← ↓ → ⎙"

};
        public object keyb()
        {
            dynamic keys = new string[] { "␛", "hh" };
            return keys;
        }
        private Dictionary<VirtualKeyCode, kbdAbstract.Row.Button> VCodeToKrb = new Dictionary<VirtualKeyCode, Row.Button>();
       /* public kbdAbstract.Row.Button? MapKey(VirtualKeyCode vk)
        {
            if (VCodeToKrb.ContainsKey(vk))
            {
                return VCodeToKrb[vk];
            }
            else
                foreach (kbdAbstract.Row kbdrow in rows)
                {
                    foreach (kbdAbstract.Row.Button Kbdrbtn in kbdrow.buttons)
                    {
                        if (Kbdrbtn.NativeVK == vk)
                        {
                            VCodeToKrb.Add(vk, Kbdrbtn);
                            return Kbdrbtn;
                        }
                    }
                }
            return null;
        }*/

        private void renderFrom(string conf, string rowDelimiter, string buttonDelimiter, string multiCharSymbol, string xspanDelimiter, bool Multichar = false)
        {
            int rowNumber = 0;
            foreach (string currentRowStr in conf.Split(rowDelimiter))
            {
                var row = new kbdAbstract.Row();
                row.render(currentRowStr, buttonDelimiter, (rowNumber > 1 && rowNumber < 5), multiCharSymbol, xspanDelimiter, ref Multichar);
                this.rows.Add(row);
                rowNumber++;
            }
        }
    }
}