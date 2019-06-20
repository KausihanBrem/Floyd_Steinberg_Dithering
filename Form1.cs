using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloydSteinbergDithering
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int rgb(float a)
        {
            if (a < 255 && a > 0) { return (int) a;}
            else if (a < 0) { return 0; }
            else { return 255; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //read image
            Bitmap bmp = new Bitmap("F:\\Scientific Instrumentation\\Master Thesis\\Work Student Projects\\Error Diffusion\\A380GS.jpg");

            //load original image in picturebox1
            pictureBox1.Image = Image.FromFile("F:\\Scientific Instrumentation\\Master Thesis\\Work Student Projects\\Error Diffusion\\A380GS.jpg");

            //get image dimension
            int width = bmp.Width;
            int height = bmp.Height;

            //color of pixel
            Color p, q;

            //Floyd Steinberg Error Diffusion
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    //get pixel value
                    p = bmp.GetPixel(x, y);

                    //extract pixel component ARGB
                    int a = p.A;
                    float r = p.R;
                    float g = p.G;
                    float b = p.B;

                    //Error Calculation
                    int R, G, B;
                    if (r < 127)
                    { R = 0; }
                    else
                    { R = 255; }

                    if (g < 127)
                    { G = 0; }
                    else
                    { G = 255; }

                    if (b < 127)
                    { B = 0; }
                    else
                    { B = 255; }

                    bmp.SetPixel(x, y, Color.FromArgb(a, R, G, B));

                    float errR = r - R;
                    float errG = g - G;
                    float errB = b - B;

                    //Applying Floyd Steinberg Matrix
                    q = bmp.GetPixel(x + 1, y);
                    float rr = q.R;
                    float gg = q.G;
                    float bb = q.B;
                    rr = rr + (errR * 7 / 16);
                    gg = gg + (errG * 7 / 16);
                    bb = bb + (errB * 7 / 16);
                    int rrr = rgb(rr);
                    int ggg = rgb(gg);
                    int bbb = rgb(bb);
                    bmp.SetPixel(x + 1, y, Color.FromArgb(a, rrr, ggg, bbb));


                    q = bmp.GetPixel(x - 1, y + 1);
                    rr = q.R;
                    gg = q.G;
                    bb = q.B;
                    rr = rr + errR * 3 / 16;
                    gg = gg + errG * 3 / 16;
                    bb = bb + errB * 3 / 16;
                    rrr = rgb(rr);
                    ggg = rgb(gg);
                    bbb = rgb(bb);
                    bmp.SetPixel(x - 1, y + 1, Color.FromArgb(a, rrr, ggg, bbb));


                    q = bmp.GetPixel(x, y + 1);
                    rr = q.R;
                    gg = q.G;
                    bb = q.B;
                    rr = rr + errR * 5 / 16;
                    gg = gg + errG * 5 / 16;
                    bb = bb + errB * 5 / 16;
                    rrr = rgb(rr);
                    ggg = rgb(gg);
                    bbb = rgb(bb);
                    bmp.SetPixel(x, y + 1, Color.FromArgb(a, rrr, ggg, bbb));


                    q = bmp.GetPixel(x + 1, y + 1);
                    rr = q.R;
                    gg = q.G;
                    bb = q.B;
                    rr = rr + errR * 1 / 16;
                    gg = gg + errG * 1 / 16;
                    bb = bb + errB * 1 / 16;
                    rrr = rgb(rr);
                    ggg = rgb(gg);
                    bbb = rgb(bb);
                    bmp.SetPixel(x + 1, y + 1, Color.FromArgb(a, rrr, ggg, bbb));
                

                }
            }

            //load Floyd Steinberg image in picturebox2
            pictureBox2.Image = bmp;

            //write the Floyd Steinberg image
            bmp.Save("F:\\Scientific Instrumentation\\Master Thesis\\Work Student Projects\\Error Diffusion\\A380FSER.jpg");
        }
    }
}
