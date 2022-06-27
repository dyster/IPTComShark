using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IPTComShark.FileManager
{
    public partial class DatePicker : UserControl
    {
        private DateTime _from;
        private DateTime _to;

        public DatePicker()
        {
            InitializeComponent();
        }

        public DateTime From;

        public DateTime To;

        public void Update(List<DateTime> dates)
        {
            if (dates.Count == 0)
                return;


            dates.Sort();

            _from = dates.First();
            _to = dates.Last();

            labelDateFrom.Text = _from.ToString();
            labelDateTo.Text = _to.ToString();

            dateTimePickerFrom.MinDate = _from;
            dateTimePickerTo.MinDate = _from;
            dateTimePickerFromTime.MinDate = _from;
            dateTimePickerToTime.MinDate = _from;
            dateTimePickerFrom.MaxDate = _to;
            dateTimePickerTo.MaxDate = _to;
            dateTimePickerFromTime.MaxDate = _to;
            dateTimePickerToTime.MaxDate = _to;
            dateTimePickerFrom.Value = _from;
            dateTimePickerTo.Value = _to;
            dateTimePickerFromTime.Value = _from;
            dateTimePickerToTime.Value = _to;
        }

        private void DateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            From = dateTimePickerFrom.Value.Date.Add(dateTimePickerFromTime.Value.TimeOfDay);
            To = dateTimePickerTo.Value.Date.Add(dateTimePickerToTime.Value.TimeOfDay);
        }
    }
}