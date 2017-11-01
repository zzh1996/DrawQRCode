using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawQRCode
{
    public partial class FormMain : Form
    {
        private QRCode qrcode;
        private bool mousedown;

        public FormMain()
        {
            InitializeComponent();
            mousedown = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            qrcode = new QRCode(QRCodeDisplay, 25);
            FormMain_SizeChanged(sender, e);
        }

        private void QRCodeDisplay_Click(object sender, EventArgs e)
        {

        }

        private void QRCodeDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
            QRCodeDisplay_MouseMove(sender, e);
        }

        private void QRCodeDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void QRCodeDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            bool value = false;
            bool pressed = false;
            if (!mousedown) return;
            if (e.Button == MouseButtons.Left)
            {
                value = true;
                pressed = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                value = false;
                pressed = true;
            }
            if (pressed)
            {
                float bs = (float)1.0 * (QRCodeDisplay.Height - 1) / qrcode.dim;
                qrcode.Set((int)(e.X / bs), (int)(e.Y / bs), value);
            }
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                QRCodeDisplay.Left = 0;
                QRCodeDisplay.Top = menuStrip1.Height;
                QRCodeDisplay.Height = QRCodeDisplay.Width =
                    ClientSize.Height - statusStrip1.Height - menuStrip1.Height;
                PicDisplay.Left = QRCodeDisplay.Width;
                PicDisplay.Top = QRCodeDisplay.Top;
                PicDisplay.Width = ClientSize.Width - QRCodeDisplay.Width;
                PicDisplay.Height = QRCodeDisplay.Height;
                qrcode.Draw();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            inputBox.ClientSize = size;
            inputBox.Text = "Size";
            inputBox.StartPosition = FormStartPosition.CenterParent;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = "21";
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();

            if (result == DialogResult.OK)
            {
                int dim = int.Parse(textBox.Text);
                try
                {
                    qrcode = new QRCode(QRCodeDisplay, dim);
                }
                catch
                {
                    MessageBox.Show("Not valid size");
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream stream = saveFileDialog1.OpenFile();
                    StreamWriter sw = new StreamWriter(stream);
                    sw.Write(qrcode.ToString());
                    sw.Close();
                    stream.Close();
                }
                catch
                {
                    MessageBox.Show("Error opening file");
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Beta version, by zzh1996");
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream stream = openFileDialog1.OpenFile();
                    StreamReader sr = new StreamReader(stream);
                    string s = sr.ReadToEnd();
                    string[] lines = s.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    int dim = lines.Length;
                    bool[,] data = new bool[dim, dim];
                    for (int i = 0; i < dim; i++)
                    {
                        for (int j = 0; j < dim; j++)
                        {
                            data[j, i] = lines[i][j] == 'X';
                        }
                    }
                    qrcode = new QRCode(QRCodeDisplay, dim, data);
                    sr.Close();
                    stream.Close();
                }
                catch
                {
                    MessageBox.Show("Error opening file");
                }
            }
        }

        private void loadPicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PicDisplay.Image = Image.FromFile(openFileDialog2.FileName);
                }
                catch
                {
                    MessageBox.Show("Error opening file");
                }
            }
        }
    }
}
