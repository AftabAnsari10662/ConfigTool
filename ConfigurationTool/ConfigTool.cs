﻿using System;
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
        ConfigurationService service;
        public ConfigTool()
        {
            InitializeComponent();
            HideDataGridView();
            service = new ConfigurationService();
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

                FileStream fs = (FileStream)saveFileDialog1.OpenFile();

                fs.Close();
            }
            else
            {
                var parameters = GetConfigParametersFromEditDataGridView();

                service.SaveConfigurationParameters(parameters, "");
            }

        }

        private List<ConfigurationParameter> GetConfigParametersFromEditDataGridView()
        {
            // Save for edit Grid view
            var parameters = new List<ConfigurationParameter>();
            for (int rows = 0; rows < editParameterGridView.Rows.Count - 1; rows++)
            {

                var parameter = new ConfigurationParameter
                {
                    Name = editParameterGridView.Rows[rows].Cells[0].Value.ToString(),
                    Value = editParameterGridView.Rows[rows].Cells[1].Value.ToString(),
                    Description = editParameterGridView.Rows[rows].Cells[2].Value.ToString(),
                    DecrementVersion = editParameterGridView.Rows[rows].Cells[3].Value.ToString(),
                    IncludeVersion = editParameterGridView.Rows[rows].Cells[4].Value.ToString()
                };
                parameters.Add(parameter);
            }
            return parameters;
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
