using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GuiElementsLabeler
{
    public partial class Form1 : Form
    {
        //temporary members
        private Point p1;
        private Point p2;
        private Graphics g;
        private int w;
        private int h;

        //members for element
        public List<Cell> mainGridCells = new List<Cell>();
        private Color color = new Color();
        private int Width;
        private int Height;
        Elements el = new Elements();

        public Form1()
        {
            InitializeComponent();
            el.elements = new List<Element>();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Load(@"C:\Temp\Photos\Tests.bmp");
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            p1 = new Point(0, 0);
            p2 = new Point(pictureBox1.Image.Width, pictureBox1.Image.Height);

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
                    g.DrawLine(pen, 0, i, pictureBox1.Width, i);

                    for (int j = 1; j < pictureBox1.Width; j++)
                    {
                        if (j % w == 0)
                        {
                            g.DrawLine(pen, j, 0, j, pictureBox1.Height);
                            Point point = new Point(j - (w / 2), i - (h / 2));

                            var x1 = j - w;
                            var y1 = i - h;
                            var x2 = j;
                            var y2 = i;

                            mainGridCells.Add(new Cell()
                            {
                                X1 = x1,
                                X2 = x2,
                                Y1 = y1,
                                Y2 = y2
                            });

                            g.DrawString(count.ToString(), f, Brushes.LawnGreen, point);

                            count += 1;
                        }
                    }
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                var p = new Point(e.X, e.Y);
                var bmp = (Bitmap)pictureBox1.Image;
                color = bmp.GetPixel(p.X, p.Y);
                textBox5.Text = color.R.ToString() + " " + color.G.ToString() + " " + color.B.ToString() + " ";
            }
            else if (checkBox3.Checked == true)
            {
                p1 = new Point(e.X, e.Y);
            }
            else
            {
                
            }

            this.Invalidate();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                p2 = new Point(e.X, e.Y);

                if (g != null && (p2.X != 0 & p2.Y != 0))
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

            for (int j = p1.Y; j < p2.Y; j = j + customCellHeight)
            {
                gr.DrawLine(pen, p1.X, j, p2.X, j);

                for (int i = p1.X; i < p2.X; i = i + customCellWidth)
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
        }

        public string CropImageAndReturnPath(Image image, int x, int y, int width, int height, string name)
        {
            var rectange = new Rectangle(0, 0, width, height);
            var bmp = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bmp);
            graphics.DrawImage(image, rectange, x, y, width, height, GraphicsUnit.Pixel);

            string path = @"C:\Temp\Photos\data\" + name + ".bmp";
            bmp.Save(path, ImageFormat.Bmp);

            //image.Dispose();
            //bmp.Dispose();
            //graphics.Dispose();

            return path;
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
                //Console.WriteLine(item);
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

        private void Button4_Click(object sender, EventArgs e)
        {
            Element element = new Element();
            element.name = textBox1.Text;
            element.width = pictureBox1.Image.Width.ToString();
            element.heigth = pictureBox1.Image.Height.ToString();
            element.type = textBox2.Text;
            element.color = new ElementColor()
            {
                active = textBox5.Text
            };
            element.parent = textBox6.Text;
            element.text = textBox4.Text;

            element.columns = new List<string>();
            //el.columns = textBox9.Text;

            element.scroll = new ElementScroll()
            {
                vertical = textBox11.Text,
                horizontal = textBox10.Text
            };

            element.grid = new List<int>();
            element.grid = GetCellsForUserSelection();

            element.cells = new List<Cell>();
            foreach (var cell in mainGridCells)
            {
                element.cells.Add(new Cell()
                {
                    X1 = cell.X1,
                    X2 = cell.X2,
                    Y1 = cell.Y1,
                    Y2 = cell.Y2
                });
            }

            element.additional_data = new ElementAdditionalData()
            {
                arrow = textBox14.Text,
                width = textBox15.Text,
                heigth = textBox13.Text,
                text = textBox16.Text
            };

            element.ImagePath = CropImageAndReturnPath(pictureBox1.Image, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y, "main");


            listBox1.Items.Add(element);

            el.elements.Add(element);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                pictureBox1.Cursor = Cursors.Hand;
            }
            else
            {
                pictureBox1.Cursor = Cursors.Default;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string output = JsonConvert.SerializeObject(el, Formatting.Indented);
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Temp\Photos\data\json.json");
                sw.WriteLine(output);
                sw.Close();
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }
    }

    public class Cell
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }
}