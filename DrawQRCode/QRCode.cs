using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawQRCode
{
    class QRCode
    {
        private bool[,] data;
        private bool[,] locked;
        private PictureBox disp;
        public int dim;
        private Bitmap bm;

        public QRCode(PictureBox disp, int dim, bool[,] data = null)
        {
            if (dim > 177 || dim < 21 || dim % 4 != 1)
                throw new ArgumentOutOfRangeException();
            if (data != null)
                this.data = data;
            else
                this.data = new bool[dim, dim];
            locked = new bool[dim, dim];
            this.disp = disp;
            this.dim = dim;
            InitPixels();
            Draw();
        }

        private void PlaceBlock(int x, int y, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != 1 && i != size - 2 && j != 1 && j != size - 2 || i == 0 || i == size - 1 || j == 0 ||
                        j == size - 1)
                    {
                        data[x + i, y + j] = true;
                    }
                }
            }
        }

        private void LockBlock(int x, int y, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    locked[x + i, y + j] = true;
                }
            }
        }

        private void PlaceDot()
        {
            for (int i = 8; i <= dim - 9; i += 2)
            {
                data[6, i] = true;
                data[i, 6] = true;
            }
        }

        private void InitPixels()
        {
            PlaceBlock(0, 0, 7);
            LockBlock(0, 0, 8, 8);
            PlaceBlock(0, dim - 7, 7);
            LockBlock(0, dim - 8, 8, 8);
            PlaceBlock(dim - 7, 0, 7);
            LockBlock(dim - 8, 0, 8, 8);
            if (dim > 21)
            {
                PlaceBlock(dim - 9, dim - 9, 5);
                LockBlock(dim - 9, dim - 9, 5, 5);
            }
            PlaceDot();
            LockBlock(8, 6, dim - 16, 1);
            LockBlock(6, 8, 1, dim - 16);
            data[8, dim - 8] = true;
            LockBlock(8, dim - 8, 1, 1);
        }

        private void DrawOne(int x, int y)
        {
            float bs = (float)1.0 * (bm.Height - 1) / dim;
            Color color;
            Graphics g = Graphics.FromImage(bm);
            if (data[x, y])
            {
                color = Color.Black;
                if (locked[x, y])
                {
                    color = Color.FromArgb(32, 32, 32);
                }
            }
            else
            {
                color = Color.White;
                if (locked[x, y])
                {
                    color = Color.FromArgb(224, 224, 224);
                }
            }
            g.FillRectangle(new SolidBrush(color), new RectangleF(bs * x, bs * y, bs, bs));
            g.DrawLine(new Pen(Color.Gray), bs * x, 0, bs * x, bm.Height);
            g.DrawLine(new Pen(Color.Gray), 0, bs * y, bm.Height, bs * y);
            g.DrawLine(new Pen(Color.Gray), bs * (x + 1), 0, bs * (x + 1), bm.Height);
            g.DrawLine(new Pen(Color.Gray), 0, bs * (y + 1), bm.Height, bs * (y + 1));
        }

        public void Draw()
        {
            bm = new Bitmap(disp.Width, disp.Height);
            Graphics g = Graphics.FromImage(bm);
            float bs = (float)1.0 * (bm.Height - 1) / dim;
            g.Clear(Color.White);
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    DrawOne(i, j);
                }
            }
            disp.Image = bm;
        }

        public bool Set(int x, int y, bool value)
        {
            if (x < 0 || x >= dim || y < 0 || y >= dim)
                return false;
            if (locked[x, y])
                return false;
            if (value != data[x, y])
            {
                data[x, y] = value;
                DrawOne(x, y);
                disp.Image = bm;
            }
            return true;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (data[j, i])
                        s += 'X';
                    else
                        s += '_';
                }
                s += Environment.NewLine;
            }
            return s;
        }
    }
}
