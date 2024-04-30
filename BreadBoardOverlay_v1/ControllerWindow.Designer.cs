using System.ComponentModel;

namespace BreadBoardOverlay_v1
{
    partial class ControllerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(51, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 27);
            this.button1.TabIndex = 1;
            this.button1.Tag = "0";
            this.button1.Text = "Play/Stop";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.PlayClick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gainsboro;
            this.button3.Location = new System.Drawing.Point(51, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 27);
            this.button3.TabIndex = 3;
            this.button3.Tag = "0";
            this.button3.Text = "Select Gif";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.selectGif);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(42, 113);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 27);
            this.button4.TabIndex = 4;
            this.button4.Tag = "0";
            this.button4.Text = "Select Sound";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.selectWav);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(25, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 255);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Playback Speed";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(35, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 16;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(23, 152);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(136, 27);
            this.button6.TabIndex = 15;
            this.button6.Tag = "0";
            this.button6.Text = "Bind Stream Deck Button";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.mapButtonToImage);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(27, 209);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(111, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.Tag = "0";
            this.textBox1.Text = "25";
            this.textBox1.TextChanged += new System.EventHandler(this.TextChangedGeneric);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(51, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 27);
            this.button2.TabIndex = 6;
            this.button2.Text = "Add Interaction";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.addInteraction);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(171, 2);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(102, 27);
            this.button20.TabIndex = 12;
            this.button20.Text = "Save Layout";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.saveLayout);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(403, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(102, 27);
            this.button5.TabIndex = 13;
            this.button5.Text = "Prev Page Map";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(521, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(102, 27);
            this.button7.TabIndex = 14;
            this.button7.Text = "Next Page Map";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(291, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(102, 27);
            this.button8.TabIndex = 15;
            this.button8.Text = "Load Layout";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.loadLayout);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(643, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(111, 27);
            this.button9.TabIndex = 16;
            this.button9.Text = "Hold";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(771, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(111, 27);
            this.button10.TabIndex = 17;
            this.button10.Text = "Stop All";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(402, 44);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(123, 21);
            this.button11.TabIndex = 18;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(261, 38);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(103, 32);
            this.button12.TabIndex = 19;
            this.button12.Text = "Frame Count";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // ControllerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(998, 761);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "ControllerWindow";
            this.Load += new System.EventHandler(this.ControllerWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button button11;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button button9;

        private System.Windows.Forms.Button button7;

        private System.Windows.Forms.Button button5;

        private System.Windows.Forms.Button button6;

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;

        private System.Windows.Forms.Button button20;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckedListBox checkedListBox3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckedListBox checkedListBox4;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.Button button1;

        #endregion
    }
}