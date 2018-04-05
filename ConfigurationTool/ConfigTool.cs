using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConfigurationTool.Models;
namespace ConfigurationTool
{
    public partial class ConfigTool : Form
    {
        public ConfigTool()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Xml File|*.xml";
            saveFileDialog1.Title = "Save an XML File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.              
                fs.Close();
            }

            var service = new ConfigurationService();

            var data = new List<ConfigurationParameter>{
                new ConfigurationParameter
                {
                    Name = "IdentityUrl",
                    Value= "http://localhost:8080",
                    Description = "identity url",
                     IncludeVersion = "1.0",
                      DecrementVersion = "1.0"
                },new ConfigurationParameter
                {
                    Name = "clientId",
                    Value= "87327373",
                    Description = "client id",
                     IncludeVersion = "1.0",
                      DecrementVersion = "1.0"
                }
            };
            service.SaveConfigurationParameters(data, "parameters.xml");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView songsDataGridView = new DataGridView();
            songsDataGridView.ColumnCount = 5;

            songsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            songsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            songsDataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(songsDataGridView.Font, FontStyle.Bold);

            songsDataGridView.Name = "songsDataGridView";
            songsDataGridView.Location = new Point(8, 8);
            songsDataGridView.Size = new Size(500, 250);
            songsDataGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            songsDataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            songsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            songsDataGridView.GridColor = Color.Black;
            songsDataGridView.RowHeadersVisible = false;

            songsDataGridView.Columns[0].Name = "Release Date";
            songsDataGridView.Columns[1].Name = "Track";
            songsDataGridView.Columns[2].Name = "Title";
            songsDataGridView.Columns[3].Name = "Artist";
            songsDataGridView.Columns[4].Name = "Album";
            songsDataGridView.Columns[4].DefaultCellStyle.Font =
                new Font(songsDataGridView.DefaultCellStyle.Font, FontStyle.Italic);

            songsDataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            songsDataGridView.MultiSelect = false;
            songsDataGridView.Dock = DockStyle.Fill;
            this.Controls.Add(songsDataGridView);

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
