using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using sonesson_tools.DataParsers;

namespace IPTComShark
{
    public class MultiColourTextRenderer : BaseRenderer
    {
        private Color backColor = Color.Transparent;

        public override void DrawText(Graphics g, Rectangle r, string txt)
        {
            if (IsDrawBackground && IsItemSelected && !ListView.FullRowSelect)
                backColor = GetSelectedBackgroundColor();

            TextFormatFlags flags = TextFormatFlags.NoPrefix |
                                    TextFormatFlags.EndEllipsis |
                                    TextFormatFlags.PreserveGraphicsTranslateTransform |
                                    CellVerticalAlignmentAsTextFormatFlag;


            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (RowObject is CapturePacket)
            {
                var cpac = (CapturePacket) RowObject;

                if (cpac.ParsedData == null)
                    return;

                // TODO use the generic function in CapturePacket instead

                var now = cpac.ParsedData.GetStringDictionary()
                    .Where(pair => pair.Key != "MMI_M_PACKET" && pair.Key != "MMI_L_PACKET")
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
                if (cpac.Previous?.ParsedData != null)
                {
                    var before = cpac.Previous.ParsedData.GetStringDictionary();

                    foreach (KeyValuePair<string, string> pair in now)
                    {
                        if (before.ContainsKey(pair.Key))
                        {
                            if (pair.Value != before[pair.Key])
                                dic.Add(pair.Key, pair.Value);
                        }
                    }
                }
                else
                {
                    dic = now;
                }
            }
            else
            {
                return;
            }


            if (dic != null)
            {
                Font font1 = Font;
                var font2 = new Font(Font, FontStyle.Bold);

                // make the original rectangle zero width to set the first string up
                r = new Rectangle(r.X, r.Y, 0, r.Height);

                foreach (KeyValuePair<string, string> o in dic)
                {
                    string text = o.Key + ": ";

                    int width = TextRenderer.MeasureText(text, font1).Width;


                    r = new Rectangle(r.Right, r.Y, width, r.Height);
                    TextRenderer.DrawText(g, text, font1, r, GetForegroundColor(), backColor, flags);


                    text = o.Value;

                    width = TextRenderer.MeasureText(text, font2).Width;

                    r = new Rectangle(r.Right, r.Y, width, r.Height);
                    TextRenderer.DrawText(g, text, font2, r, GetForegroundColor(), backColor, flags);
                }

                //TextRenderer.DrawText(g, dic.DictionaryData.Count.ToString(), this.Font, r, this.GetForegroundColor(), backColor, flags);
            }
            else
            {
                base.DrawText(g, r, txt);
            }


            //TextRenderer.DrawText(g, txt, this.Font, r, this.GetForegroundColor(), backColor);
        }
    }
}