using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiElementsLabeler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Load(@"C:\Temp\Photos\Tests.bmp");
            //Console.WriteLine(9 % 4);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = Form1.ActiveForm.Width;
            pictureBox1.Height = Form1.ActiveForm.Height - 30;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var w = pictureBox1.Width / 8;
            var h = pictureBox1.Height / 8;

            Graphics g = pictureBox1.CreateGraphics();

            Pen p = new Pen(Color.LawnGreen);
            p.Width = 2.0f;

            Font f = new Font("Arial", 14);

            //horizontal lines
            for (int i = 0; i < pictureBox1.Height; i++)
            {
                if (i % h == 0)
                {
                    g.DrawLine(p, 0, i, pictureBox1.Width, i);
                }
            }
            
            //vertical lines
            for (int i = 0; i < pictureBox1.Width; i++)
            {
                if (i % w == 0)
                {
                    g.DrawLine(p, i, 0, i, pictureBox1.Height);
                }
            }
        }
    }
}
