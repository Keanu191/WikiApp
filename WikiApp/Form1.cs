using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WikiApp
{
    public partial class Form1 : Form
    {
        // Define static variables for the dimensions 9.1
        public static int rows = 12;
        public static int columns = 4;

        // Global 2D string array 9.1
        public static string[,] wikiArray = new string[rows, columns];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 9.2 Add button
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
