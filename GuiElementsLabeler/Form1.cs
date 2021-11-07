using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using GuiElementsLabeler.Helpers;
using GuiElementsLabeler.ProjectModel;

namespace GuiElementsLabeler
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        private Elements el = new Elements();
        private Graphics g;
        private DrawingMembers drawingMembers;
        private bool drawingBlocker = false;
        private Rectangles userRectangles;
        private Rectangle rectangle;
        private string fileName;

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
            theDialog.Filter = "BMP|*.bmp|JPG|*.jpg;*.jpeg|PNG|*.png";
            theDialog.InitialDirectory = @"c:\Temp\!my\TestsTab\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                pathToFile = theDialog.FileName;
                fileName = theDialog.SafeFileName;
            }

            if (File.Exists(pathToFile))
            {
                pictureBox1.Load(pathToFile);
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;

                form2.SetScreenResolution(pictureBox1.Image.Width.ToString(), pictureBox1.Image.Height.ToString());

                userRectangles = new Rectangles();
                userRectangles.ListRectangles = new List<UserRectangle>();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            drawingMembers = new DrawingMembers();

            selectorCheckBox.Enabled = true;
            colorPeakerCheckBox.Enabled = true;

            g = pictureBox1.CreateGraphics();

            //drawingMembers = new DrawingMembers();
            button4.Enabled = true;
            newElementButton.Enabled = false;
            colorPeakerCheckBox.Enabled = true;
            selectorCheckBox.Enabled = true;

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
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (drawingBlocker)
            {
                if (drawingBlocker && !colorPeakerCheckBox.Enabled || selectorCheckBox.Enabled)
                {
                    drawingMembers.p1 = new Point(e.X, e.Y);
                    this.Invalidate();
                }

                /*
                else if (colorPeakerCheckBox.Checked == true)
                {
                    drawingMembers.p1 = new Point(e.X, e.Y);

                    var p = new Point(e.X, e.Y);
                    var bmp = (Bitmap)pictureBox1.Image;
                    drawingMembers.SetColor(bmp.GetPixel(p.X, p.Y)); //= bmp.GetPixel(p.X, p.Y);
                    var color = drawingMembers.GetColor().R.ToString() + " " + drawingMembers.GetColor().G.ToString() + " " + drawingMembers.GetColor().B.ToString() + " ";
                    form2.SetColorActive(color);
                    this.Invalidate();
                }
                */
            }
            else if (colorPeakerCheckBox.Checked == true)
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
                if (!colorPeakerCheckBox.Enabled || selectorCheckBox.Enabled)
                {
                    if (selectorCheckBox.Checked == true)
                    {
                        drawingMembers.p2 = new Point(e.X, e.Y);

                        if (g != null && (drawingMembers.p2.X != 0 & drawingMembers.p2.Y != 0))
                        {
                            Pen pen = new Pen(Color.Red, 2);
                            rectangle = new Rectangle(drawingMembers.p1.X, drawingMembers.p1.Y, drawingMembers.p2.X - drawingMembers.p1.X, drawingMembers.p2.Y - drawingMembers.p1.Y);
                            g.DrawRectangle(pen, rectangle);
                        }

                        form2.SetWidth((drawingMembers.p2.X - drawingMembers.p1.X).ToString());
                        form2.SetHeight((drawingMembers.p2.Y - drawingMembers.p1.Y).ToString());

                        this.Invalidate();
                    }
                }

                drawingBlocker = false;
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

            element.ImagePath = CropImageAndReturnPath(pictureBox1.Image, drawingMembers.p1.X, drawingMembers.p1.Y, drawingMembers.p2.X - drawingMembers.p1.X, drawingMembers.p2.Y - drawingMembers.p1.Y, form2.GetName());

            element.ScreenResolution = new string[] { pictureBox1.Image.Width.ToString(), pictureBox1.Image.Height.ToString() };

            form2.AddToList(element.name);

            el.elements.Add(element);

            newElementButton.Enabled = true;
            colorPeakerCheckBox.Enabled = false;
            selectorCheckBox.Enabled = false;
            button4.Enabled = false;

            userRectangles.ListRectangles.Add(new UserRectangle() 
            { 
                FileName = fileName,
                Rectangle = rectangle
            });
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (colorPeakerCheckBox.Checked == true)
            {
                pictureBox1.Cursor = Cursors.Hand;
                selectorCheckBox.Enabled = false;
            }
            else
            {
                pictureBox1.Cursor = Cursors.Default;
                selectorCheckBox.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FilesHelper.SaveJsonFile(el);
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (selectorCheckBox.Checked)
            {
                colorPeakerCheckBox.Enabled = false;
            }
            else if (!selectorCheckBox.Checked)
            {
                colorPeakerCheckBox.Enabled = true;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(!selectorCheckBox.Checked)
            {
                colorPeakerCheckBox.Enabled = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            FilesHelper.SaveProjectFile(userRectangles);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FilesHelper.ReadProjectFile();
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