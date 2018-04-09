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
            editParameterGridView.Visible = false;
            newParameterDataGridView.Visible = false;
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

                ShowEditParameterDataGridView();
                HideNewParameterDataGridView();
                editParameterGridView.DataSource = configurationParameterBindingSource;
            }
        }

        private void HideNewParameterDataGridView()
        {
            newParameterDataGridView.Visible = false;
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
            ShowNewParameterDataGridView();
            HideEditParameterDataGridView();
        }

        private void HideEditParameterDataGridView()
        {
            editParameterGridView.Visible = false;
        }

        private void ShowNewParameterDataGridView()
        {
            newParameterDataGridView.Visible = true;
        }

        private void ShowEditParameterDataGridView()
        {
            editParameterGridView.Visible = true;
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
