using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IPTComShark.Properties;

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
                var ignores = Settings.Default.IgnoreVariables
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (cpac.DisplayFields.Count > 0)
                {
                    foreach (var displayField in cpac.DisplayFields)
                    {
                        if (!ignores.Contains(displayField.Name))
                            tuples.Add(new Tuple<string, string>(displayField.Name, displayField.Val.ToString()));
                    }
                }
                
            }
            else
            {
                return;
            }

            Font font1 = Font;
            var font2 = new Font(Font, FontStyle.Bold);
            
            var maxX = r.X + Column.Width;

            // make the original rectangle zero width to set the first string up
            r = new Rectangle(r.X, r.Y, 0, r.Height);

            var foregroundColor = GetForegroundColor();
            //var brush = new SolidBrush(foregroundColor);

            foreach (Tuple<string, string> o in tuples)
            {
                string text = o.Item1 + ": ";

                int width = TextRenderer.MeasureText(text, font1).Width;

                if (r.Left > maxX || r.Left > this.ListView.Bounds.Right)
                {
                    // no point drawing outside of bounds
                    break;
                }
                r = new Rectangle(r.Right, r.Y, width, r.Height);
                
                TextRenderer.DrawText(g, text, font1, r, foregroundColor, backColor, flags);
                
                text = o.Item2;

                width = TextRenderer.MeasureText(text, font2).Width;

                r = new Rectangle(r.Right, r.Y, width, r.Height);
                TextRenderer.DrawText(g, text, font2, r, foregroundColor, backColor, flags);
            }

            
        }
    }
}