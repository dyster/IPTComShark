using BrightIdeasSoftware;
using System.Drawing;
using System.Linq;
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
                        
            if (RowObject is not CapturePacket cpac)
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

            foreach(var field in cpac.DisplayFields.Where(df => df.Display))
            {
                string text = field.Name + ": ";

                int width = TextRenderer.MeasureText(text, font1).Width;

                if (r.Left > maxX || r.Left > this.ListView.Bounds.Right)
                {
                    // no point drawing outside of bounds
                    break;
                }

                r = new Rectangle(r.Right, r.Y, width, r.Height);

                TextRenderer.DrawText(g, text, font1, r, foregroundColor, backColor, flags);

                text = field.Val.ToString();

                width = TextRenderer.MeasureText(text, font2).Width;

                r = new Rectangle(r.Right, r.Y, width, r.Height);
                TextRenderer.DrawText(g, text, font2, r, foregroundColor, backColor, flags);
            }            
        }
    }
}