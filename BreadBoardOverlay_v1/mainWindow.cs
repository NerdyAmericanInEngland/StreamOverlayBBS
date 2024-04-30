using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BreadBoardOverlay_v1
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
            toolScript._topLeft = leftTop;
            // foreach(var pb in this.Controls.OfType<PictureBox>())
            // {
            //     toolScript.panelBoxes.Add(pb);
            // }
            // Debug.WriteLine($"Startup routine run");
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {  
            // ActiveForm.Size = Screen.AllScreens[0].Bounds.Size;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        
        private void leftTop_Click(object sender, EventArgs e)
        {
            
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            
        }
        
    }
    
}