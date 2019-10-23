using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IPTComShark.Controls
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


            //Dictionary<string, string> dic = new Dictionary<string, string>();
            var tuples = new List<Tuple<string, string>>();
            if (RowObject is CapturePacket cpac)
            {
                if (cpac.DisplayFields.Count > 0)
                {
                    foreach (var displayField in cpac.DisplayFields)
                    {
                        tuples.Add(new Tuple<string, string>(displayField.Item1, displayField.Item2.ToString()));
                    }
                }
                else
                {
                    if (cpac.ParsedData == null)
                        return;

                    var delta = cpac.GetDelta();

                    foreach (var field in delta)
                    {
                        if (field.Name != "MMI_M_PACKET" && field.Name != "MMI_L_PACKET")
                            tuples.Add(new Tuple<string, string>(field.Name, field.Value.ToString()));
                    }
                }

                
            }
            else
            {
                return;
            }

            Font font1 = Font;
            var font2 = new Font(Font, FontStyle.Bold);

            // make the original rectangle zero width to set the first string up
            r = new Rectangle(r.X, r.Y, 0, r.Height);

            foreach (Tuple<string, string> o in tuples)
            {
                string text = o.Item1 + ": ";

                int width = TextRenderer.MeasureText(text, font1).Width;


                r = new Rectangle(r.Right, r.Y, width, r.Height);
                TextRenderer.DrawText(g, text, font1, r, GetForegroundColor(), backColor, flags);


                text = o.Item2;

                width = TextRenderer.MeasureText(text, font2).Width;

                r = new Rectangle(r.Right, r.Y, width, r.Height);
                TextRenderer.DrawText(g, text, font2, r, GetForegroundColor(), backColor, flags);
            }

            //TextRenderer.DrawText(g, dic.DictionaryData.Count.ToString(), this.Font, r, this.GetForegroundColor(), backColor, flags);
            //base.DrawText(g, r, txt);         
            //TextRenderer.DrawText(g, txt, this.Font, r, this.GetForegroundColor(), backColor);
        }
    }
}