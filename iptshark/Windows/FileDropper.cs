using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TrainShark.Windows
{
    public partial class FileDropper : Form
    {
        public List<string> DroppedFiles = new List<string>();

        public FileDropper()
        {
            InitializeComponent();
        }

        public delegate void PrintDelegate(string text);

        public void Print(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new PrintDelegate(Print), text);
            }
            else
            {
                listBox1.Items.Add(text);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
            }
        }

        public List<string> FilterPaths(string[] paths)
        {
            var files = new List<string>();
            foreach (var s in paths)
            {
                if (File.Exists(s))
                {
                    files.Add(s);
                    Print("Added file " + s);
                }
                else if (Directory.Exists(s))
                {
                    try
                    {
                        var strings = Directory.GetFiles(s, "*", SearchOption.AllDirectories);
                        files.AddRange(strings);
                        Print("Added " + strings.Length + " files from " + s);
                    }
                    catch (Exception e)
                    {
                        Print(e.Message);
                    }
                }
            }

            return files;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = (string[])e.Data.GetData(DataFormats.FileDrop);

                var filterPaths = FilterPaths(data);

                DroppedFiles.AddRange(filterPaths);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}