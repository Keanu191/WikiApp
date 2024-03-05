/*
 * 30074191 / Keanu Farro
 * Assesment 1 c# WikiAPP prototype
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void InitaliseArray()
        {
            // Loop through each row
            for (int i = 0; i < rows; i++)
            {
                // Loop through each column
                for (int j = 0; j < columns; j++)
                {
                    // Initialize the element at [i, j] to an empty string
                    wikiArray[i, j] = "";
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FormTwoDimAraysLoad(object sender, EventArgs e)
        {
            InitaliseArray();
            DisplayListViewArray();
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
                    wikiArray[currentRow, i] = textBoxes;

                }

                // Move to next row
                currentRow++;
                MessageBox.Show("Row " + currentRow + " added successfully!");

            }
            else
            {
                MessageBox.Show("2D Array is full!");
            }
            BubbleSort();
            DisplayListViewArray();

        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                currentRow = listView.SelectedIndices[0];
            }
            else
            {
                currentRow = -1; // no row selected
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 9.10 Save Button
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "dat file| *.dat";
            saveFileDialog.Title = "Save a DAT file";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            saveFileDialog.DefaultExt = "dat";
            saveFileDialog.ShowDialog();
            string fileName = saveFileDialog.FileName;
            if (saveFileDialog.FileName != "")
            {
                SaveRecords(fileName); // Save the file
            }
            else
            {
                SaveRecords("Default.dat"); // Save a default file
            }

        }

        private void SaveRecords(string saveFileName)
        {
            try
            {
                using (var stream = File.Open(saveFileName, FileMode.Create))
                {
                    using (var write = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        for (int x = 0; x < rows; x++)
                        {
                            for (int y = 0; y < columns; y++)
                            {
                                write.Write(wikiArray[x, y]);
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayListViewArray()
        {
            // Clear existing items in the ListView
            listView.Items.Clear();

            // Loop through each row in the wikiArray
            for (int i = 0; i < rows; i++)
            {
                // Create a new ListViewItem to represent each row
                ListViewItem item = new ListViewItem();

                // Add sub-items (columns) to the ListViewItem
                for (int j = 0; j < columns; j++)
                {
                    item.SubItems.Add(wikiArray[i, j]);
                }

                // Add the ListViewItem to the ListView
                listView.Items.Add(item);
            }
        }

        private void SwapRows(int row1, int row2)
        {
            // 9.6 seperate swap method
            // Loop through each column in the rows being swapped
            for (int j = 0; j < columns; j++)
            {
                // Temporary variable to hold the value of the element being swapped
                string swap = wikiArray[row1, j];
                // Swap the elements by assigning the value of one element to the other
                wikiArray[row1, j] = wikiArray[row2, j];
                wikiArray[row2, j] = swap;
            }
        }

        private void BubbleSort()
        {
            // 9.6 bubble sort
            bool swapped; // to indicate if any swaps were made in the current pass
            do
            {
                // reset the bool for the next pass
                swapped = false;
                // loop through each row except for the last one
                for (int i = 0; i < rows - 1; i++)
                {
                    // compare the names in the first column of the current row and the next row
                    // if the current rows name is alphabetically after the next row's name, swap the rows
                    if (wikiArray[i, 0].CompareTo(wikiArray[i + 1, 0]) > 0)
                    {
                        // swap the rows
                        SwapRows(i, i + 1);
                        // set the bool to true indicating that the swap was made
                        swapped = true;
                    }
                }
                // continue loop until no more swaps are required
            } while (swapped);
        }



        private void button6_Click(object sender, EventArgs e)
        {
            // 9.11 Load Button
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "dat file| *.dat";
            openFileDialog.Title = "Open a DAT file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenRecords(openFileDialog.FileName); // open file
            }
        }

        private void OpenRecords(string openFileName)
        {
            if (File.Exists(openFileName))
            {
                using (var stream = File.Open(openFileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        while (stream.Position < stream.Length)
                        {
                            for (int y = 0; y < columns; y++)
                            {
                                wikiArray[currentRow, y] = reader.ReadString();
                            }
                            currentRow++;
                        }

                        // read data
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (currentRow >= 0 && currentRow < rows)
            {
                // Update the wikiArray with the new values from the textboxes
                wikiArray[currentRow, 0] = textBox1.Text;
                wikiArray[currentRow, 1] = textBox2.Text;
                wikiArray[currentRow, 2] = textBox3.Text;
                wikiArray[currentRow, 3] = textBox4.Text;

                // Refresh the ListView to reflect the changes
                DisplayListViewArray();

                // Provide feedback to the user
                MessageBox.Show("Row " + (currentRow + 1) + " updated successfully!");
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 9.4 delete button

            // Display Message if no items selected to delete
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("You have not selected anything to delete!");
            }

            // Check if there's an item selected in the ListView
            if (listView.SelectedItems.Count > 0)
            {
                // Assuming that the first selected item is the one to be deleted
                int selectedIndex = listView.SelectedIndices[0];

                // Shift everything after the selected one, up by one
                for (int i = selectedIndex; i < rows - 1; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        wikiArray[i, j] = wikiArray[i + 1, j];
                    }
                }

                // clear last row
                for (int j = 0; j < columns; j++)
                {
                    wikiArray[rows - 1, j] = "";
                }

                // Decrease the currentRow if it's the last row
                if (currentRow == rows - 1)
                {
                    currentRow--;
                }

                // Refresh the ListView
                DisplayListViewArray();

                MessageBox.Show("Row deleted successfully!");
            }
            else
            {
                MessageBox.Show("Nothing has been selected to delete!");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // 9.5 Clear button

            // check if textboxes are already clear
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Textboxes are already clear!");
                toolStripStatusLabel1.Text = "Textboxes are already clear!";
            }
            else
            {
                // Clear all text boxes when button is clicked
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

                // display popup when text boxes are cleared
                MessageBox.Show("Textboxes cleared!");
                toolStripStatusLabel1.Text = "TextBoxes cleared";
            }

        }
    }
}

