using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Life
{
    public partial class GUIforLife : Form 
    {
        private Life game;
        private Bitmap bitmap = new Bitmap(10,10);
        private Thread t = null;
        private bool isDrawing = false;
        public GUIforLife()
        {
 
            InitializeComponent();
            game = new Life(Grid.Width, Grid.Height);
            bitmap = new Bitmap(Grid.Width, Grid.Height);
        }

        public void Start()
        {
            while (true)
            {
                //game.Step();
                What();
                Thread.Sleep(1000);

            }

        }

        public void What()
        {
            // блокируем изображение и получаем информацию о массиве пикселей
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            // получаем адрес первого пикселя в массиве пикселей
            IntPtr ptr = bitmapData.Scan0;

            // вычисляем количество байтов на один пиксель
            int bytesPerPixel = 4;

            // проходим по всем клеткам и перекрашиваем соответствующие пиксели в нужный цвет
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * bitmapData.Stride + x * bytesPerPixel;

                    if (game[x, y] == true)
                    {
                        // клетка живая - перекрашиваем в белый цвет
                        Marshal.WriteByte(ptr, index + 3, 255); // A
                        Marshal.WriteByte(ptr, index + 2, 255); // R
                        Marshal.WriteByte(ptr, index + 1, 255); // G
                        Marshal.WriteByte(ptr, index, 255);     // B
                    }
                    else
                    {
                        // клетка мертвая - перекрашиваем в черный цвет
                        Marshal.WriteByte(ptr, index + 3, 255); // A
                        Marshal.WriteByte(ptr, index + 2, 0);   // R
                        Marshal.WriteByte(ptr, index + 1, 0);   // G
                        Marshal.WriteByte(ptr, index, 0);       // B
                    }
                }
            }

            // разблокируем изображение
            bitmap.UnlockBits(bitmapData);
            Grid.Image = bitmap;
            Grid.Invalidate();
        }
        public void SetPixel(MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;

            // пересчитываем координаты в координаты пикселей на Bitmap
            int pixelX = (int)(mouseX);
            int pixelY = (int)(mouseY);

            // перекрашиваем соответствующий пиксель в белый цвет
            bitmap.SetPixel(pixelX, pixelY, Color.Red);
            game[pixelX,pixelY] = true;

            // перерисовываем холст
            Grid.Image = bitmap;
            Grid.Invalidate();
        }
        private void Grid_MouseUp(object sender, MouseEventArgs e) => isDrawing = false;

        private void Grid_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            SetPixel(e);
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(isDrawing)
            {
                SetPixel(e);
            }
        }

        private void button1_Click(object sender, EventArgs e) => Start();
    }
}
