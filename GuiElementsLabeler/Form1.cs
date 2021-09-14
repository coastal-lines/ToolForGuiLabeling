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
        private List<Point> gridCells = new List<Point>();
        private Rectangle r1;
        private Rectangle r2;
        private Graphics g;
        private int w;
        private int h;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Load(@"C:\Temp\Photos\Tests.bmp");
            pictureBox1.Width = Form1.ActiveForm.Width - 100;
            pictureBox1.Height = Form1.ActiveForm.Height - 100;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            w = pictureBox1.Width / 8;
            h = pictureBox1.Height / 8;

            g = pictureBox1.CreateGraphics();
            Font f = new Font("Arial", 16);
            Pen pen = new Pen(Color.LawnGreen);
            pen.Width = 2.0f;

            int count = 0;
            for (int i = 1; i < pictureBox1.Height; i++)
            {
                if (i % h == 0)
                {
                    g.DrawLine(pen, 0, i, pictureBox1.Width, i);

                    for (int j = 1; j < pictureBox1.Width; j++)
                    {
                        if (j % w == 0)
                        {
                            g.DrawLine(pen, j, 0, j, pictureBox1.Height);
                            count += 1;
                            Point point = new Point(j - (w / 2), i - (h / 2));
                            gridCells.Add(new Point(j - w, i - h));
                            g.DrawString(count.ToString(), f, Brushes.Green, point);
                        }
                    }
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            r1 = new Rectangle(e.X, e.Y, 0, 0);
            //Console.WriteLine(r1);
            this.Invalidate();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            r2 = new Rectangle(e.X, e.Y, 0, 0);
            //Console.WriteLine(r2);

            if (g != null && (r2.X != 0 & r2.Y !=0))
            {
                Pen pen = new Pen(Color.Red, 2);
                var userRectange = new Rectangle(r1.X, r1.Y, r2.X - r1.X, r2.Y - r1.Y);
                g.DrawRectangle(pen, userRectange);
            }

            List<int> t = new List<int>();

            for (int i = r1.X; i >= r1.X & i <= r2.X; i++)
            {
                for (int j = 0; j < gridCells.Count; j++)
                {
                    if ((r1.X >= gridCells[j].X && r1.X <= gridCells[j].X + w) &
                        (r1.Y >= gridCells[j].Y && r1.Y <= gridCells[j].Y + h))
                        //(r1.Y >= gridCells[j].Y & r1.Y <= gridCells[j].Y + h) &
                        //(r2.Y >= gridCells[j].Y & r2.Y <= gridCells[j].Y + h)
                    {
                        //Console.WriteLine(j);
                        if (!t.Contains(j))
                        {
                            t.Add(j);
                        }
                        continue;
                    }
                }
            }
            
            for (int i = r1.X; i >= r1.X & i <= r2.X; i++)
            {
                for (int j = 0; j < gridCells.Count; j++)
                {
                    if ((r2.X >= gridCells[j].X && r2.X <= gridCells[j].X + w) &
                        (r2.Y >= gridCells[j].Y && r2.Y <= gridCells[j].Y + h))
                        //(r1.Y >= gridCells[j].Y & r1.Y <= gridCells[j].Y + h) &
                        //(r2.Y >= gridCells[j].Y & r2.Y <= gridCells[j].Y + h)
                    {
                        if (!t.Contains(j))
                        {
                            t.Add(j);
                        }
                        continue;
                    }
                }
            }
            
            /*
            
            for (int i = 0; i < gridCells.Count; i++)
            {
                if ((r1.X >= gridCells[i].X & r1.X <= gridCells[i].X + w) & 
                    (r2.X >= gridCells[i].X & r2.X <= gridCells[i].X + w) &
                    (r1.Y >= gridCells[i].Y & r1.Y <= gridCells[i].Y + h) &
                    (r2.Y >= gridCells[i].Y & r2.Y <= gridCells[i].Y + h))
                {
                    t.Add(i);
                }
            }
            */
            foreach (var item in t)
            {
                Console.WriteLine(item);
            }

            this.Invalidate();
        }
    }

    public class GuiElement
    {
        private string Name { get; set; }
        private string Type { get; set; }
        private string Width { get; set; }
        private string Height { get; set; }
        private string Parent { get; set; }
        private string ColorActive { get; set; }
        private string ColorNonActive { get; set; }
        private string Text { get; set; }
        private List<string> Columns { get; set; }
        private bool ScrollVertical { get; set; }
        private bool ScrollHorizontal { get; set; }
        private List<int> Grid { get; set; }
        private string ExtraDataExpandDirection { get; set; }
        private string ExtraDataSize { get; set; }
        private string ExtraDataText { get; set; }
    }
}