using System.Windows.Forms;

namespace GuiElementsLabeler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.newElementButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.colorPeakerCheckBox = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.selectorCheckBox = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1083, 651);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // newElementButton
            // 
            this.newElementButton.Location = new System.Drawing.Point(345, 0);
            this.newElementButton.Name = "newElementButton";
            this.newElementButton.Size = new System.Drawing.Size(88, 23);
            this.newElementButton.TabIndex = 28;
            this.newElementButton.Text = "new element";
            this.newElementButton.UseVisualStyleBackColor = true;
            this.newElementButton.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(241, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(98, 23);
            this.button4.TabIndex = 29;
            this.button4.Text = "add to list";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // colorPeakerCheckBox
            // 
            this.colorPeakerCheckBox.AutoSize = true;
            this.colorPeakerCheckBox.Enabled = false;
            this.colorPeakerCheckBox.Location = new System.Drawing.Point(150, 4);
            this.colorPeakerCheckBox.Name = "colorPeakerCheckBox";
            this.colorPeakerCheckBox.Size = new System.Drawing.Size(85, 17);
            this.colorPeakerCheckBox.TabIndex = 40;
            this.colorPeakerCheckBox.Text = "color peaker";
            this.colorPeakerCheckBox.UseVisualStyleBackColor = true;
            this.colorPeakerCheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(439, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 41;
            this.button5.Text = "save json";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // selectorCheckBox
            // 
            this.selectorCheckBox.AutoSize = true;
            this.selectorCheckBox.Checked = true;
            this.selectorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectorCheckBox.Enabled = false;
            this.selectorCheckBox.Location = new System.Drawing.Point(81, 4);
            this.selectorCheckBox.Name = "selectorCheckBox";
            this.selectorCheckBox.Size = new System.Drawing.Size(63, 17);
            this.selectorCheckBox.TabIndex = 42;
            this.selectorCheckBox.Text = "selector";
            this.selectorCheckBox.UseVisualStyleBackColor = true;
            this.selectorCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox3_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(520, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 43;
            this.button2.Text = "save project";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 682);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.selectorCheckBox);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.colorPeakerCheckBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.newElementButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Button newElementButton;
        private Button button4;
        private CheckBox colorPeakerCheckBox;
        private Button button5;
        private CheckBox selectorCheckBox;
        private Button button2;
    }
}

