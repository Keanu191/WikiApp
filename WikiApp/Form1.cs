/*
 * 30074191 / Keanu Farro
 * Assesment 1 c# WikiAPP prototype
 */
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
        public int currentRow = 0; // Tracks the current row 9.2


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
            if (currentRow < rows)
            {
                string textBoxes = textBox1.Text + textBox2.Text + textBox3.Text + textBox4.Text; // Combine all textboxes together into a string

                // Store information from the 4 textboxes into the 2d array
                for (int i = 0; i < columns; i++)
                {
                    wikiArray[currentRow, i] = wikiArray[currentRow, i] = textBoxes;
                }

                // Move to next row
                currentRow++;
                MessageBox.Show("Row " + currentRow + " added successfully!");

            }
            else
            {
                MessageBox.Show("2D Array is full!");
            }

        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
