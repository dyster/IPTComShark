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
        public override void DrawText(Graphics g, Rectangle r, string txt)
        {
            Color backColor = Color.Transparent;
            if (IsDrawBackground && IsItemSelected && !ListView.FullRowSelect)
                backColor = GetSelectedBackgroundColor();

            TextFormatFlags flags = TextFormatFlags.NoPrefix |
                                    TextFormatFlags.EndEllipsis |
                                    TextFormatFlags.PreserveGraphicsTranslateTransform |
                                    CellVerticalAlignmentAsTextFormatFlag;


            Dictionary<string, object> dic = null;
            switch (RowObject)
            {
                case SS27Packet ss27:
                    if (Column.AspectName == "SubMessage")
                        dic = ss27.SubMessage?.ToDictionary(row => row.Name, row => row.Value);
                    else if (Column.AspectName == "Header")
                        dic = ss27.Header?.ToDictionary(row => row.Name, row => row.Value);
                    break;
                case ParsedDataObject ido:
                    dic = ido.DictionaryData;
                    break;
            }


            if (dic != null)
            {
                Font font1 = Font;
                var font2 = new Font(Font, FontStyle.Bold);

                // make the original rectangle zero width to set the first string up
                r = new Rectangle(r.X, r.Y, 0, r.Height);

                foreach (KeyValuePair<string, object> o in dic)
                {
                    string text = o.Key + ": ";

                    int width = TextRenderer.MeasureText(text, font1).Width;


                    r = new Rectangle(r.Right, r.Y, width, r.Height);
                    TextRenderer.DrawText(g, text, font1, r, GetForegroundColor(), backColor, flags);


                    text = o.Value.ToString();

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