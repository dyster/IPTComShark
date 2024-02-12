using System.Windows.Forms;

namespace TrainShark.Windows
{
    public partial class TextWindow : Form
    {
        public TextWindow(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
        }
    }
}