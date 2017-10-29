using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawQRCode
{
    public partial class FormMain : Form
    {
        private QRCode qrcode;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            QRCodeDisplay.Top = QRCodeDisplay.Left = 0;
            QRCodeDisplay.Height = QRCodeDisplay.Width = ClientSize.Height;
            qrcode = new QRCode(QRCodeDisplay, 25);
        }

        private void QRCodeDisplay_Click(object sender, EventArgs e)
        {

        }

        private void QRCodeDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            QRCodeDisplay_MouseMove(sender, e);
        }

        private void QRCodeDisplay_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void QRCodeDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            bool value = false;
            bool pressed = false;
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
    }
}
