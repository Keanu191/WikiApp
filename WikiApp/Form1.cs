/*
 * 30074191 / Keanu Farro
 * Assesment 1 c# WikiAPP prototype
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
        // create a 2d array to store data
        public static string[,] wikiArray = new string[rows, columns];
        public Form1()
        {
            InitializeComponent();
        }

        // initalise the array with empty strings
        private void InitaliseArray()
        {
            // Loop through each row and column to initalise elements to empty  strings
            for (int i = 0; i < rows; i++)
            {
                // Loop through each column
                for (int j = 0; j < columns; j++)
                {
                    // Initialize the element at [i, j] to an empty string
                    wikiArray[i, j] = "";
                }
            }
            currentRow = 0;
        }

        // method to fill the array and show it in the listview when the wiki app/form loads
        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialise the array and display it in the ListView when the form loads
            InitaliseArray(); // initalise array
            BubbleSort(); // sort the array
            InitializeListView(listView); // initalise list view
        }

        private void FormTwoDimAraysLoad(object sender, EventArgs e)
        {
            InitaliseArray();
            DisplayListViewArray();
        }

        // method to handle adding new items to the array
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 9.2 Add button
            if (currentRow < rows)
            {
                bool Added = false; // flag to track if the item is successfully added
                // check if the textboxes are empty
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
                {
                    // Assign textbox values to the corresponding position in the 2d array
                    wikiArray[currentRow, 0] = textBox1.Text.ToUpper(); // to upper as lowercase letters go to the bottom of the listview instead of being sorted
                    wikiArray[currentRow, 1] = textBox2.Text;
                    wikiArray[currentRow, 2] = textBox3.Text;
                    wikiArray[currentRow, 3] = textBox4.Text;
                    Added = true; // set added flag to true
                    currentRow++; // increment
                }
                // if all textboxes were filled properly
                if (Added)
                {
                    InitializeListView(listView); // update listview
                    BubbleSort();
                    btnClear_Click(sender, e); // clear the textboxes
                    toolStripStatusLabel1.Text = "The item was succesfully added to the list, index: " + (currentRow - 1);
                }
                // if not all textboxes were filled properly
                else
                {
                    MessageBox.Show("Make sure you fill all the textboxes!");
                    toolStripStatusLabel1.Text = "make sure you fill all the textboxes!";
                }
            }
            // if max limit in the array is reached
            else
            {
                MessageBox.Show("2D Array is full!");
                toolStripStatusLabel1.Text = "2D Array is full!";
            }

        }




        // method to handle saving records to a file
        private void button3_Click(object sender, EventArgs e)
        {
            // 9.10 Save Button
            SaveFileDialog saveFileDialog = new SaveFileDialog(); // Create a SaveFileDialog instance
            saveFileDialog.Filter = "dat file| *.dat"; // Set filter for the file dialog (.dat file)
            saveFileDialog.Title = "Save a DAT file"; // set the title for the file dialog
            saveFileDialog.InitialDirectory = Application.StartupPath; // set directory for file dialog
            saveFileDialog.DefaultExt = "dat"; // default extension (.dat files)
            saveFileDialog.ShowDialog(); // show file dialog
            string fileName = saveFileDialog.FileName;

            // check if the file name is not empty
            if (saveFileDialog.FileName != "")
            {
                SaveRecords(fileName); // Save the file
                toolStripStatusLabel1.Text = "File successfully saved!";
            }
            else
            {
                SaveRecords("Default.dat"); // Save a default file
                toolStripStatusLabel1.Text = "Successfully saved!";
            }

        }

        // method to handle saving records to a file
        private void SaveRecords(string saveFileName)
        {
            try
            {
                // open file stream for writing
                using (var stream = File.Open(saveFileName, FileMode.Create))
                {
                    using (var write = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        // write array data to the file
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
                MessageBox.Show(ex.Message); // display error messages
            }
        }

        private void DisplayListViewArray()
        {

        }

        private void SwapRows(int selectedIndex, int selectedIndex2)
        {
            // 9.6 seperate swap method
            // method to swap rows in the array

            String[] temp = new string[columns];

            temp[0] = wikiArray[selectedIndex, 0];
            temp[1] = wikiArray[selectedIndex, 1];
            temp[2] = wikiArray[selectedIndex, 2];
            temp[3] = wikiArray[selectedIndex, 3];

            wikiArray[selectedIndex, 0] = wikiArray[selectedIndex2, 0];
            wikiArray[selectedIndex, 1] = wikiArray[selectedIndex2, 1];
            wikiArray[selectedIndex, 2] = wikiArray[selectedIndex2, 2];
            wikiArray[selectedIndex, 3] = wikiArray[selectedIndex2, 3];

            wikiArray[selectedIndex2, 0] = temp[0];
            wikiArray[selectedIndex2, 1] = temp[1];
            wikiArray[selectedIndex2, 2] = temp[2];
            wikiArray[selectedIndex2, 3] = temp[3];
        }

        private void BubbleSort()
        {
            // 9.6 bubble sort
            // method to perform bubble sort on the array
            for (int j = 0; j < wikiArray.GetLength(0); j++)
            {
                for (int k = 0; k < wikiArray.GetLength(0) - 1; k++)
                {
                    if (!string.IsNullOrEmpty(wikiArray[k + 1, 0]) &&
                        string.CompareOrdinal(wikiArray[k, 0], wikiArray[k + 1, 0]) > 0)
                    {
                        SwapRows(k, k + 1);
                    }
                }
            }
        }



        // method to handle loading records from a file
        private void button6_Click(object sender, EventArgs e)
        {
            // 9.11 Load Button
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Create an OpenFileDialog instance
            openFileDialog.InitialDirectory = Application.StartupPath; // Set initial directory for the file dialog
            openFileDialog.Filter = "dat file| *.dat";  // set filter for file dialog
            openFileDialog.Title = "Open a DAT file"; // set title
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenRecords(openFileDialog.FileName); // open file
            }
        }

        // method to handle loading records from a file
        private void OpenRecords(string openFileName)
        {
            if (File.Exists(openFileName))
            {
                using (var stream = File.Open(openFileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        currentRow = 0; // reset current row before loading new data
                        while (stream.Position < stream.Length)
                        {
                            for (int y = 0; y < columns; y++)
                            {
                                wikiArray[currentRow, y] = reader.ReadString();
                            }
                            currentRow++; // increment
                        }

                        // Sort the array after loading
                        BubbleSort();

                        // read data
                    }
                }
            }
            InitializeListView(listView); // Refresh the ListView after loading
            toolStripStatusLabel1.Text = "Successfully loaded file!";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // 9.3 edit button
            // Check if an item is selected and textboxes are not empty
            if (listView.SelectedIndices.Count > 0 && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {
                int selectedIndex = listView.SelectedIndices[0];  // Get the index of the selected item
                wikiArray[selectedIndex, 0] = textBox1.Text.ToUpper();      // update values in the array
                wikiArray[selectedIndex, 1] = textBox2.Text;
                wikiArray[selectedIndex, 2] = textBox3.Text;
                wikiArray[selectedIndex, 3] = textBox4.Text;

                BubbleSort(); // sort the array after editing

                // update listview, clear textboxes and sort
                InitializeListView(listView);
                btnClear_Click(sender, e); // clear textboxes
                toolStripStatusLabel1.Text = "Succesfully edited item at index: " + selectedIndex;

            }
            else if (listView.SelectedIndices.Count == 0)
            {
                toolStripStatusLabel1.Text = "Could not edit item as there is no item selected.";
            }
            else
            {
                toolStripStatusLabel1.Text = "Could not edit item as textboxes for data are empty.";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 9.4 delete button

            if (listView.SelectedIndices.Count > 0) // Check if an item is selected
            {
                int selectedIndex = listView.SelectedIndices[0]; // get the index of the selected item
                string selectedIndex2 = wikiArray[selectedIndex, 0];
                string tilde = "~";

                // Prompt user for confirmation before deleting
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo);

                if (selectedIndex2 == tilde)
                {
                    toolStripStatusLabel1.Text = "Empty cell cannot be deleted!";
                }
                // Check if the user gave permission
                else if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < columns; i++)
                    {
                        wikiArray[selectedIndex, i] = "~"; // turn item into a tilde
                    }

                    // remove the item from the listview
                    InitializeListView(listView); // update listview
                    btnClear_Click(sender, e); // clear textboxes
                    currentRow--;
                    toolStripStatusLabel1.Text = "Succesfully deleted item at index: " + selectedIndex;
                    
                }
                else
                {
                    toolStripStatusLabel1.Text = "User chose not to delete.";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Cannot delete item, no item selected and no data availiable.";
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

                textBox1.Focus(); // refocus


                toolStripStatusLabel1.Text = "TextBoxes cleared";
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 9.7 binary search
            // method for performing binary search
            string search = textBox5.Text.Trim();
            if (!string.IsNullOrEmpty(search) && currentRow > 0) // check if the searchbox is not empty and data exists in the array
            {
                int foundIndex = -1; // initalise the index of the found item
                int left = 0; // initalise the left boundary for binary search
                int right = currentRow - 1; // initalise the right boundary for binary search

                try
                {
                    // perform the binary search
                    while (left <= right)
                    {
                        // find the middle index
                        int mid = left + (right - left) / 2;

                        
                        string midselection = wikiArray[mid, 0].Trim(); // get the value at the middle index

                        // compare the middle value with the search term
                        int comparisonResult = string.Compare(midselection, search, StringComparison.OrdinalIgnoreCase);

                        if (comparisonResult == 0) // if the middle value matches the search term
                        { 
                            foundIndex = mid; // set the index of the found term

                           
                            break; // exit loop 
                        }
                         
                        else if (comparisonResult > 0) // if the middle value is greater than the search term
                        {
                            right = mid - 1; // adjust the right boundary
                        }
                        else // if the middle value is less then the search term
                        {
                            left = mid + 1; // adjust the left boundary
                        }
                    }
                }
                catch (Exception exc)
                {
                    toolStripStatusLabel1.Text = exc.Message; // handle exceptions
                }

                // display search result
                if (foundIndex != -1)
                {
                    // success messages
                    MessageBox.Show("Item found! Binary search successful, index: " + foundIndex);
                    toolStripStatusLabel1.Text = "Item found! Binary search successful, index: " + foundIndex;
                }
                else
                {
                    MessageBox.Show("ERROR 404: item not found :(");
                    toolStripStatusLabel1.Text = "ERROR 404: item not found :(";
                }
            }
            else
            {
                // cant search with an empty text box message
                MessageBox.Show("can't search with an empty text box!");
                toolStripStatusLabel1.Text = "can't search with an empty text box!";
            }
        }


        // a method to initalise the listview with array data
        private void InitializeListView(ListView listView)
        {
            // 9.8 Display method
            listView.Items.Clear(); // clear listview items
            BubbleSort();

            for (int i = 0; i < currentRow; i++)
            {
               if (!string.IsNullOrEmpty(wikiArray[i, 0]))
                {
                    string display = wikiArray[i, 0];
                    string display2 = wikiArray[i, 1];

                    ListViewItem item = new ListViewItem(display);
                    item.SubItems.Add(display2);
                    listView.Items.Add(item);
                }
            }
        }


        // method to handle selecting an item from the listview
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 9.9 Select definition from ListView
            if (listView.SelectedIndices.Count > 0)
            {
                int selectedIndex = listView.SelectedIndices[0];
                textBox1.Text = wikiArray[selectedIndex, 0];
                textBox2.Text = wikiArray[selectedIndex, 1];
                textBox3.Text = wikiArray[selectedIndex, 2];
                textBox4.Text = wikiArray[selectedIndex, 3];

                textBox1.Focus(); // refocus
            }



        }
    }
}

