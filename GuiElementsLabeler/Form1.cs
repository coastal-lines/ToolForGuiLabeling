﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GuiElementsLabeler
{
    public partial class Form1 : Form
    {
        public List<Cell> mainGridCells = new List<Cell>();
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
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
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
                                X1 = x1, X2 = x2, Y1 = y1, Y2 = y2
                            });

                            g.DrawString(count.ToString(), f, Brushes.LawnGreen, point);
                            Console.WriteLine(x2);
                            Console.WriteLine(j);

                            count += 1;
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

        private void Button2_Click_1(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Element el = new Element();
            el.name = "main";
            el.width = pictureBox1.Image.Width.ToString();
            el.heigth = pictureBox1.Image.Height.ToString();

            el.cells = new List<Cell>();
            foreach (var cell in mainGridCells)
            {
                el.cells.Add(new Cell()
                {
                    X1 = cell.X1,
                    X2 = cell.X2,
                    Y1 = cell.Y1,
                    Y2 = cell.Y2
                });
            }

            listBox1.Items.Add(el);

            string output = JsonConvert.SerializeObject(el, Formatting.Indented);
            
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Temp\Photos\json.json");
                sw.WriteLine(output);
                sw.Close();
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }

    public class Cell
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }

    /*
    public class GuiElement
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Parent { get; set; }
        public string ColorActive { get; set; }
        public string ColorNonActive { get; set; }
        public string Text { get; set; }
        public List<string> Columns { get; set; }
        public bool ScrollVertical { get; set; }
        public bool ScrollHorizontal { get; set; }
        public List<Cell> Grid { get; set; }
        public string ExtraDataExpandDirection { get; set; }
        public string ExtraDataSize { get; set; }
        public string ExtraDataText { get; set; }

        public GuiElement()
        {
            this.Grid = new List<Cell>();
        }

        */
}