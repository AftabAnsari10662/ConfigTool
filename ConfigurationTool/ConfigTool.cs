using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ConfigurationTool.Models;
using System.IO;
using System.Xml.Linq;

namespace ConfigurationTool
{
    public partial class ConfigTool : Form
    {
        public ConfigTool()
        {
            InitializeComponent();
            HideDataGridView();
        }

        private void HideDataGridView()
        {
            parameterGridView.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName);

                var configurationParameters = QueryXml(openFileDialog.FileName);

                foreach (var parameter in configurationParameters)
                {
                    configurationParameterBindingSource.Add(parameter);
                }

                sr.Close();
                ShowDataGridView();
                parameterGridView.DataSource = configurationParameterBindingSource;
            }
        }

        private IEnumerable<ConfigurationParameter> QueryXml(string filePath)
        {
            var document = XDocument.Load(filePath);
            var configurationParameters =
                document.Element("ConfigurationParameters")
                .Elements("ConfigurationParameter")
                .Select((x) =>
                new ConfigurationParameter
                {
                     Name = x.Element("Name").Value,
                     Description = x.Element("Description").Value,
                     Value = x.Element("Value").Value,
                     DecrementVersion = x.Element("DecrementVersion").Value,
                     IncludeVersion = x.Element("IncludeVersion").Value,
                }).ToList();
            return configurationParameters;
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
                var filePath = saveFileDialog1.FileName.Replace(@"\\", @"\");
                var filePath2 = Path.GetFullPath(filePath);
                service.SaveConfigurationParameters(data, filePath);
                // Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the  
                // File type selected in the dialog box.  
                // NOTE that the FilterIndex property is one-based.              
                fs.Close();
            }


        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDataGridView();

        }

        private void ShowDataGridView()
        {
            parameterGridView.Visible = true;
        }

        private void CreateDataGridView()
        {


            parameterGridView.Dock = DockStyle.Fill;
            parameterGridView.ColumnCount = 5;

            parameterGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            parameterGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            parameterGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(parameterGridView.Font, FontStyle.Bold);

            parameterGridView.Name = "songsDataGridView";
            parameterGridView.Location = new Point(8, 8);
            parameterGridView.Size = new Size(500, 250);
            parameterGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            parameterGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            parameterGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            parameterGridView.GridColor = Color.Black;
            parameterGridView.RowHeadersVisible = false;

            parameterGridView.Columns[0].Name = "Release Date";
            parameterGridView.Columns[1].Name = "Track";
            parameterGridView.Columns[2].Name = "Title";
            parameterGridView.Columns[3].Name = "Artist";
            parameterGridView.Columns[4].Name = "Album";
            parameterGridView.Columns[4].DefaultCellStyle.Font =
                new Font(parameterGridView.DefaultCellStyle.Font, FontStyle.Italic);

            parameterGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            parameterGridView.MultiSelect = false;
            parameterGridView.Dock = DockStyle.Fill;
            this.Controls.Add(parameterGridView);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runConfigurationTaggingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runTaggingValidationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void parameterGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
