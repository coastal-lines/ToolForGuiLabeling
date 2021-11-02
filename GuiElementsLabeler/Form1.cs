using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GuiElementsLabeler.Helpers;
using GuiElementsLabeler.PictureBoxParts;

namespace GuiElementsLabeler
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        private Elements el = new Elements();
        private Graphics g;
        private DrawingMembers drawingMembers;
        private bool drawingBlocker = true;

        public Form1(Form2 form2)
        {
            InitializeComponent();
            this.form2 = form2;
            this.form2.Show();
            el.elements = new List<Element>();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string pathToFile = "";

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Image";
            theDialog.Filter = "Image files|*.bmp";
            theDialog.InitialDirectory = @"C:\Temp2\Flash\MyLabeling\ORB\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                pathToFile = theDialog.FileName;
            }

            if (File.Exists(pathToFile))
            {
                pictureBox1.Load(pathToFile);
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;
                drawingMembers = new DrawingMembers();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            drawingMembers = new DrawingMembers();
            button4.Enabled = true;
            button2.Enabled = false;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;

            drawingBlocker = true;

            //clean form2
            form2.SetName("");
            form2.SetWidth("");
            form2.SetHeight("");
            form2.SetColorActive("");
            form2.SetColorNonActive("");
            form2.SetText("");
            form2.SetColumns("");
            form2.SetVerticalScroll("");
            form2.SetHorizontalScroll("");
            form2.SetGrid("");
            form2.SetAdditionalArrow("");
            form2.SetAdditionalWidth("");
            form2.SetAdditionalHeight("");
            form2.SetAdditionalText("");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //drawingMembers = new DrawingMembers();

            drawingMembers.p1 = new Point(0, 0);
            drawingMembers.p2 = new Point(pictureBox1.Image.Width, pictureBox1.Image.Height);

            drawingMembers.w = pictureBox1.Width / 8;
            drawingMembers.h = pictureBox1.Height / 8;

            g = pictureBox1.CreateGraphics();

            //prepare main element and save
            Element element = new Element();
            element.name = "main";
            element.width = pictureBox1.Image.Width.ToString();
            element.heigth = pictureBox1.Image.Height.ToString();
            element.type = form2.GetType();
            element.color = new ElementColor()
            {
                active = form2.GetActiveColor()
            };
            element.parent = form2.GetParent();
            element.text = form2.GetText();

            element.columns = new List<string>();

            element.scroll = new ElementScroll()
            {
                vertical = form2.GetVerticalScroll(),
                horizontal = form2.GetHorizontalScroll()
            };

            ///
            //var userW = GenerateSegment(0, pictureBox1.Image.Width);
            //var userH = GenerateSegment(0, pictureBox1.Image.Height);
            
            element.additional_data = new ElementAdditionalData()
            {
                arrow = form2.GetAdditionalArrow(),
                width = form2.GetAdditionalWidth(),
                heigth = form2.GetAdditionalHeight(),
                text = form2.GetAdditionalText()
            };

            element.ImagePath = CropImageAndReturnPath(
                pictureBox1.Image, drawingMembers.p1.X, drawingMembers.p1.Y,
                drawingMembers.p2.X - drawingMembers.p1.X,
                drawingMembers.p2.Y - drawingMembers.p1.Y, "main");


            form2.AddToList(element.name);

            el.elements.Add(element);

            button3.Enabled = false;
            button2.Enabled = true;

            form2.SetName("main");
            form2.SetParent("main");

            form2.SetWidth(pictureBox1.Image.Width.ToString());
            form2.SetHeight(pictureBox1.Image.Height.ToString());
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (drawingBlocker)
            {
                if (drawingBlocker && !checkBox2.Enabled || checkBox3.Enabled)
                {
                    drawingMembers.p1 = new Point(e.X, e.Y);
                    this.Invalidate();
                }

                else if (checkBox2.Checked == true)
                {
                    drawingMembers.p1 = new Point(e.X, e.Y);

                    var p = new Point(e.X, e.Y);
                    var bmp = (Bitmap)pictureBox1.Image;
                    drawingMembers.SetColor(bmp.GetPixel(p.X, p.Y)); //= bmp.GetPixel(p.X, p.Y);
                    var color = drawingMembers.GetColor().R.ToString() + " " + drawingMembers.GetColor().G.ToString() + " " + drawingMembers.GetColor().B.ToString() + " ";
                    form2.SetColorActive(color);
                    this.Invalidate();
                }
            }
            else if (checkBox2.Checked == true)
            {
                drawingMembers.p1 = new Point(e.X, e.Y);

                var p = new Point(e.X, e.Y);
                var bmp = (Bitmap)pictureBox1.Image;
                drawingMembers.SetColor(bmp.GetPixel(p.X, p.Y)); //= bmp.GetPixel(p.X, p.Y);
                var color = drawingMembers.GetColor().R.ToString() + " " + drawingMembers.GetColor().G.ToString() + " " + drawingMembers.GetColor().B.ToString() + " ";
                form2.SetColorActive(color);
                this.Invalidate();
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (drawingBlocker)
            {
                if (!checkBox2.Enabled || checkBox3.Enabled)
                {
                    if (checkBox3.Checked == true)
                    {
                        drawingMembers.p2 = new Point(e.X, e.Y);

                        if (g != null && (drawingMembers.p2.X != 0 & drawingMembers.p2.Y != 0))
                        {
                            Pen pen = new Pen(Color.Red, 2);
                            var userRectange = new Rectangle(drawingMembers.p1.X, drawingMembers.p1.Y, drawingMembers.p2.X - drawingMembers.p1.X, drawingMembers.p2.Y - drawingMembers.p1.Y);
                            g.DrawRectangle(pen, userRectange);
                        }

                        form2.SetWidth((drawingMembers.p2.X - drawingMembers.p1.X).ToString());
                        form2.SetHeight((drawingMembers.p2.Y - drawingMembers.p1.Y).ToString());

                        this.Invalidate();
                    }
                }

                drawingBlocker = false;
            }
        }

        public List<Cell> DrawGrid()
        {
            var customWidth = drawingMembers.p2.X - drawingMembers.p1.X;
            var customHeight = drawingMembers.p2.Y - drawingMembers.p1.Y;


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

            for (int j = drawingMembers.p1.Y; j < drawingMembers.p2.Y; j = j + customCellHeight)
            {
                gr.DrawLine(pen, drawingMembers.p1.X, j, drawingMembers.p2.X, j);

                for (int i = drawingMembers.p1.X; i < drawingMembers.p2.X; i = i + customCellWidth)
                {
                    if (i < drawingMembers.p2.X - remainderDivisionX && j < drawingMembers.p2.Y - remainderDivisionY)
                    {
                        gr.DrawLine(pen, drawingMembers.p1.X, j, drawingMembers.p2.X, j);
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

            return gridCells;
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
            element.name = form2.GetName();
            element.width = (drawingMembers.p2.X - drawingMembers.p1.X).ToString();
            element.heigth = (drawingMembers.p2.Y - drawingMembers.p1.Y).ToString();
            element.type = form2.GetType();
            element.color = new ElementColor()
            {
                active = form2.GetActiveColor()
            };
            element.parent = form2.GetParent();
            element.text = form2.GetText();

            element.columns = new List<string>();
            //el.columns = textBox9.Text;

            element.scroll = new ElementScroll()
            {
                vertical = form2.GetVerticalScroll(),
                horizontal = form2.GetHorizontalScroll()
            };

            element.additional_data = new ElementAdditionalData()
            {
                arrow = form2.GetAdditionalArrow(),
                width = form2.GetAdditionalWidth(),
                heigth = form2.GetAdditionalHeight(),
                text = form2.GetAdditionalText()
            };

            element.ImagePath = CropImageAndReturnPath(pictureBox1.Image, drawingMembers.p1.X, drawingMembers.p1.Y, drawingMembers.p2.X - drawingMembers.p1.X, drawingMembers.p2.Y - drawingMembers.p1.Y, form2.GetName());


            form2.AddToList(element.name);

            el.elements.Add(element);

            button2.Enabled = true;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            button4.Enabled = false;

            form2.SetParent(form2.GetName());
            drawingBlocker = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                pictureBox1.Cursor = Cursors.Hand;
                checkBox3.Enabled = false;
            }
            else
            {
                pictureBox1.Cursor = Cursors.Default;
                checkBox3.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FilesHelper.SaveJsonFile(el);
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Enabled = false;
            }
            else if (!checkBox3.Checked)
            {
                checkBox2.Enabled = true;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkBox3.Checked)
            {
                checkBox2.Enabled = true;
            }
        }
    }

    public class DrawingMembers
    {
        private Color color = new Color();
        public Point p1 { get; set; }
        public Point p2 { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Color GetColor()
        {
            return color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }
    }
}