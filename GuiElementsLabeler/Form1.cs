using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GuiElementsLabeler
{
    public partial class Form1 : Form
    {
        private List<Cell> mainGridCells= new List<Cell>();
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
            pictureBox1.Width = Form1.ActiveForm.Width - 300;
            pictureBox1.Height = Form1.ActiveForm.Height - 100;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            w = pictureBox1.Width / 8;
            h = pictureBox1.Height / 8;

            g = pictureBox1.CreateGraphics();
            Font f = new Font("Arial", 16);
            Pen pen = new Pen(Color.LawnGreen);
            pen.Width = 1.0f;

            int count = 0;
            for (int i = 1; i < pictureBox1.Height; i++)
            {
                if (i % h == 0)
                {
                    //g.DrawLine(pen, 0, i, pictureBox1.Width, i);

                    for (int j = 1; j < pictureBox1.Width; j++)
                    {
                        if (j % w == 0)
                        {
                            //g.DrawLine(pen, j, 0, j, pictureBox1.Height);
                            count += 1;
                            Point point = new Point(j - (w / 2), i - (h / 2));

                            var x1 = j - w;
                            var y1 = i - h;
                            var x2 = j;
                            var y2 = i;

                            mainGridCells.Add(new Cell(){X1 = x1, X2 = x2, Y1 = y1, Y2 = y2});
                            //g.DrawString(count.ToString(), f, Brushes.LawnGreen, point);
                        }
                    }
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            p1 = new Point(e.X, e.Y);
            this.Invalidate();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            p2 = new Point(e.X, e.Y);

            if (g != null && (p2.X != 0 & p2.Y !=0))
            {
                Pen pen = new Pen(Color.Red, 2);
                var userRectange = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
                g.DrawRectangle(pen, userRectange);
            }

            if (checkBox1.Checked == true)
            {
                DrawGrid();
            }

            this.Invalidate();
        }

        public void DrawGrid()
        {
            var customWidth = p2.X - p1.X;
            var customHeight = p2.Y - p1.Y;


            int customCellWidth = customWidth / 8;
            int customCellHeight = customHeight / 8;

            var gr = pictureBox1.CreateGraphics();
            Font f = new Font("Arial", 8);
            Pen pen = new Pen(Color.Aqua);
            pen.Width = 1.0f;

            int count = 0;
            List<Cell> gridCells = new List<Cell>();
            var remainderDivisionX = customWidth % 8;
            var remainderDivisionY = customHeight % 8;
            for (int i = p1.X; i < p2.X; i = i + customCellWidth)
            {
                gr.DrawLine(pen, i, p1.Y, i, p2.Y);

                for (int j = p1.Y; j < p2.Y; j = j + customCellHeight)
                {
                    if (i < p2.X - remainderDivisionX && j < p2.Y - remainderDivisionY)
                    {
                        gr.DrawLine(pen, p1.X, j, p2.X, j);
                        Point point = new Point(i, j);
                        gr.DrawString(count.ToString(), f, Brushes.Aqua, point);
                        count++;

                        var x1 = i;
                        var y1 = j;
                        var x2 = i + customCellWidth;
                        var y2 = j + customCellHeight;
                        gridCells.Add(new Cell() { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2 });
                    }
                }
            }



            /*
            int count = 0;
            for (int i = p1.X; i < p2.X; i ++)
            {



                if (i % customCellWidth == 0)
                {
                    gr.DrawLine(pen, p1.X + i, p1.Y, p1.X + i, p2.Y);

                    for (int j = p1.Y; j < p2.Y; j++)
                    {
                        if ((j % customCellHeight == 0) && (j >= p1.Y + customCellHeight))
                        {
                            gr.DrawLine(pen, p1.X, j, p2.X, j);
                            count += 1;
                        }
                    }
                }
            }
            */
        }


        public void DrawGrid2()
        {
            var customGridWidth = p2.X - p1.X;
            var customGridHeight = p2.Y - p1.Y;

            float f2 = (float)Math.Round((float)5);

            float customCellWidth = (float)Math.Round(((float)p2.X - (float)p1.X) / 8.0f, 1);
            float customCellHeight = (float)Math.Round(((float)p2.Y - (float)p1.Y) / 8.0f, 1);

            g = pictureBox1.CreateGraphics();
            Font f = new Font("Arial", 8);
            Pen pen = new Pen(Color.Aqua);
            pen.Width = 1.0f;

            int count = 0;
            for (float i = (float)p1.X; i < p2.X; i+=0.1f)
            {
                if (Math.Round(i,1) % customCellWidth == 0)
                {
                    g.DrawLine(pen, i, p1.Y, i, p2.Y);
                    /*
                    for (int j = p1.X; j < customGridWidth; j++)
                    {
                        if (j % customCellWidth == 0)
                        {
                            g.DrawLine(pen, p2.X, 0, j, customGridHeight);
                            count += 1;
                            Point point = new Point(j - (customGridWidth / 2), i - (customGridHeight / 2));
                            //gridCells.Add(new Point(j - w, i - h));

                            //var x1 = j - customGridWidth;
                            //var y1 = i - customGridHeight;
                            //var x2 = j;
                            //var y2 = i;

                            //gridCellsTemp.Add(new Cell() { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2 });
                            g.DrawString(count.ToString(), f, Brushes.Green, point);
                        }
                    }
                    */
                }
            }
        }

        public List<int> GetCellsForUserSelection()
        {
            List<int> cellsOfSelection = new List<int>();
            var userW = GenerateSegment(p1.X, p2.X);
            var userH = GenerateSegment(p1.Y, p2.Y);

            //обходим все ячейки
            for (int i = 0; i < mainGridCells.Count; i++)
            {
                var lineW = GenerateSegment(mainGridCells[i].X1, mainGridCells[i].X2);
                var lineH = GenerateSegment(mainGridCells[i].Y1, mainGridCells[i].Y2);

                //проверяем совпадение по горизонтали
                for (int j = 0; j < lineW.Count; j++)
                {
                    for (int k = 0; k < userW.Count; k++)
                    {
                        if (userW[k] == lineW[j])
                        {
                            //проверяем совпадение по вертикали
                            for (int m = 0; m < lineH.Count; m++)
                            {
                                for (int l = 0; l < userH.Count; l++)
                                {
                                    if (userH[l] == lineH[m])
                                    {
                                        if (!cellsOfSelection.Contains(i))
                                        {
                                            cellsOfSelection.Add(i);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                    }

                }
            }

            foreach (var item in cellsOfSelection)
            {
                Console.WriteLine(item);
            }

            return cellsOfSelection;
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