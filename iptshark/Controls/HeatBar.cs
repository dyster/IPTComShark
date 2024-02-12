using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrainShark.Controls
{
    public partial class HeatBar : UserControl
    {
        private uint _min = default;
        private uint _max = default;
        private Dictionary<uint, uint> _list = new Dictionary<uint, uint>();
        private DateTime _epoch = new DateTime(2000, 1, 1);
        public HeatBar()
        {
            InitializeComponent();

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 200, 300));
            myBrush.Dispose();
            formGraphics.Dispose();


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.
            base.OnPaint(e);

            // Declare and instantiate a new pen that will be disposed of at the end of the method.
            using var myPen = new Pen(Color.Aqua);

            // Create a rectangle that represents the size of the control, minus 1 pixel.
            var area = new Rectangle(new Point(0, 0), new Size(this.Size.Width - 1, this.Size.Height - 1));

            // Draw an aqua rectangle in the rectangle represented by the control.
            e.Graphics.DrawRectangle(myPen, area);
        }

        private void ReBar()
        {
            int width = this.Size.Width;

            uint totalSpan = _max - _min;
            double pixelSpan = totalSpan / width;


        }

        private uint DateToEpoch(DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime - _epoch;
            return Convert.ToUInt32(timeSpan.TotalSeconds);
        }

        public void Push(DateTime dateTime)
        {
            uint time = DateToEpoch(dateTime);

            if (_list.TryGetValue(time, out uint val))
            {
                _list[time] = val + 1;
                return;
            }
            else
            {
                _list.Add(time, 1);

            }



            if (_max == default)
            {
                _min = time;
                _max = time;
                return;
            }

            if (time < _min)
            {
                _min = time;
                ReBar();
            }
            else if (time > _max)
            {
                _max = time;
                ReBar();
            }
            else
            {
                // nothing?
            }
        }

        private void HeatBar_Load(object sender, EventArgs e)
        {

        }
    }

    public class Bar
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int Heat { get; set; }
    }
}
