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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        #region Setters

        public void SetName(string text)
        {
            textBox1.Text = text;
        }

        public void SetWidth(string text)
        {
            textBox3.Text = text;
        }

        public void SetHeight(string text)
        {
            textBox7.Text = text;
        }

        public void SetColorActive(string text)
        {
            textBox5.Text = text;
        }

        public void SetColorNonActive(string text)
        {
            textBox8.Text = text;
        }

        public void SetParent(string text)
        {
            textBox6.Text = text;
            comboBox2.Items.Add(text);
        }

        public void SetText(string text)
        {
            textBox4.Text = text;
        }

        public void SetColumns(string text)
        {
            textBox9.Text = text;
        }

        public void SetVerticalScroll(string text)
        {
            comboBox3.SelectedItem = text;
        }

        public void SetHorizontalScroll(string text)
        {
            comboBox4.SelectedItem = text;
        }

        public void SetScreenResolution(string text)
        {
            textBox12.Text = text;
        }

        public void AddToList(string text)
        {
            listBox1.Items.Add(text);
        }

        #endregion

        #region Getters

        public string GetName()
        {
            return textBox1.Text;
        }
        
        public string GetType()
        {
            if (comboBox1.SelectedItem != null)
            {
                return comboBox1.SelectedItem.ToString();
            }

            return "";
        }
        
        public string GetActiveColor()
        {
            return textBox5.Text;
        }

        public string GetNonActiveColor()
        {
            return textBox8.Text;
        }

        public string GetParent()
        {
            return textBox6.Text;
        }

        public string GetText()
        {
            return textBox4.Text;
        }

        public string GetVerticalScroll()
        {
            return comboBox3.Items[0].ToString();
        }

        public string GetHorizontalScroll()
        {
            return comboBox4.Items[0].ToString();
        }

        #endregion

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
