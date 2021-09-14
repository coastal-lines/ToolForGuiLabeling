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
        //private List<Point> gridCells = new List<Point>();
        private List<Cell> gridCellsTemp = new List<Cell>();
        //private Rectangle r1;
        //private Rectangle r2;
        private Point p1;
        private Point p2;
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
                            //gridCells.Add(new Point(j - w, i - h));

                            var x1 = j - w;
                            var y1 = i - h;
                            var x2 = j;
                            var y2 = i;

                            gridCellsTemp.Add(new Cell(){X1 = x1, X2 = x2, Y1 = y1, Y2 = y2});
                            g.DrawString(count.ToString(), f, Brushes.Green, point);
                        }
                    }
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            //r1 = new Rectangle(e.X, e.Y, 0, 0);
            p1 = new Point(e.X, e.Y);
            //Console.WriteLine(r1);
            this.Invalidate();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            //r2 = new Rectangle(e.X, e.Y, 0, 0);
            p2 = new Point(e.X, e.Y);
            //Console.WriteLine(r2);

            if (g != null && (p2.X != 0 & p2.Y !=0))
            {
                Pen pen = new Pen(Color.Red, 2);
                var userRectange = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
                g.DrawRectangle(pen, userRectange);
            }

            List<int> t = new List<int>();
            /*
            for (int i = p1.X; i >= p1.X & i <= p2.X; i++)
            {
                for (int j = 0; j < gridCellsTemp.Count; j++)
                {
                    if ((p1.X >= gridCellsTemp[j].X1 && p1.X <= gridCellsTemp[j].X2) &
                        (p1.Y >= gridCellsTemp[j].Y1 && p1.Y <= gridCellsTemp[j].Y2))
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
            
            for (int i = p1.X; i >= p1.X & i <= p2.X; i++)
            {
                for (int j = 0; j < gridCellsTemp.Count; j++)
                {
                    if ((p2.X >= gridCellsTemp[j].X1 && p2.X <= gridCellsTemp[j].X2) &
                        (p2.Y >= gridCellsTemp[j].Y1 && p2.Y <= gridCellsTemp[j].Y2))
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
            */
            
            /*
            for (int i = 0; i < gridCellsTemp.Count; i++)
            {
                var segmentW = gridCellsTemp[i].X2 - gridCellsTemp[i].X1;
                var segmentH = gridCellsTemp[i].Y2 - gridCellsTemp[i].Y1;

                if (p1.X > gridCellsTemp[i].X1 && p1.Y > gridCellsTemp[i].Y1)
                {
                    if (!t.Contains(i))
                    {
                        t.Add(i);
                    }
                }
            }
            */
            List<int> t2 = new List<int>();
            List<int> t3 = new List<int>();
            var userW = GenerateSegment(p1.X, p1.X + w);
            var userH = GenerateSegment(p1.Y, p1.Y + h);
            for (int i = 0; i < gridCellsTemp.Count; i++)
            {
                var lineW = GenerateSegment(gridCellsTemp[i].X1, gridCellsTemp[i].X2);
                var lineH = GenerateSegment(gridCellsTemp[i].Y1, gridCellsTemp[i].Y2);

                for (int j = 0; j < lineW.Count; j++)
                {
                    for (int k = 0; k < userW.Count; k++)
                    {
                        if (lineW[j] == userW[k])
                        {
                            if (!t2.Contains(i))
                            {
                                t2.Add(i);
                            }

                            break;
                        }
                    }
                }

                for (int j = 0; j < lineH.Count; j++)
                {
                    for (int k = 0; k < userH.Count; k++)
                    {
                        if (lineH[j] == userH[k])
                        {
                            if (!t3.Contains(i))
                            {
                                t3.Add(i);
                            }

                            break;
                        }
                    }
                }
            }



            foreach (var item in t2)
            {
                Console.WriteLine(item);
            }

            this.Invalidate();
        }

        public List<int> GenerateSegment(int v1, int v2)
        {
            var l = new List<int>();

            for (int i = v1; i < v2; i++)
            {
                l.Add(i);
            }

            return l;
        }
    }

    public class Cell
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
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