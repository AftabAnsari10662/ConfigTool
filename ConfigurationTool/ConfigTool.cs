using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ConfigurationTool.Models;
using System.IO;
using System.Xml.Linq;
using ConfigurationTool.Service;

namespace ConfigurationTool
{
    public partial class ConfigTool : Form
    {
        ConfigurationService _configurationService;
        bool hasXMLFileBeenOpened = false;
        string _xmlFilePath;
        public ConfigTool()
        {
            InitializeComponent();
            HideDataGridView();
            _configurationService = new ConfigurationService();
            _xmlFilePath = string.Empty;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configurationParameterBindingSource.Clear();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                hasXMLFileBeenOpened = true;
                var fileName = openFileDialog.FileName;
                _xmlFilePath = fileName;
                var configurationParameters = _configurationService.QueryXml(fileName);

                foreach (var parameter in configurationParameters)
                {
                    configurationParameterBindingSource.Add(parameter);
                }

                ShowEditParameterDataGridView();
                HideNewParameterDataGridView();
                editParameterGridView.DataSource = configurationParameterBindingSource;
            }

        }

        private void HideNewParameterDataGridView()
        {
            newParameterDataGridView.Visible = false;
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasXMLFileBeenOpened)
            {
                var parameters = GetConfigParametersFromEditDataGridView();
                _configurationService.SaveConfigurationParameters(parameters, _xmlFilePath);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml File|*.xml";
            saveFileDialog.Title = "Save an XML File";
            saveFileDialog.ShowDialog();
             
            if (saveFileDialog.FileName != "")
            {
                var parameters = GetConfigParametersFromNewDataGridView();
                var fileName = saveFileDialog.FileName;
                _xmlFilePath = fileName;
                _configurationService.SaveConfigurationParameters(parameters, fileName);
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
                    TagName = editParameterGridView.Rows[rows].Cells[0].Value == null ?
                               "" : editParameterGridView.Rows[rows].Cells[0].Value.ToString(),

                    Value = editParameterGridView.Rows[rows].Cells[1].Value == null ?
                                "" : editParameterGridView.Rows[rows].Cells[1].Value.ToString(),

                    Description = editParameterGridView.Rows[rows].Cells[2].Value == null ?
                                  "" : editParameterGridView.Rows[rows].Cells[2].Value.ToString(),

                    VersionAdded = editParameterGridView.Rows[rows].Cells[3].Value == null ?
                                          "" : editParameterGridView.Rows[rows].Cells[3].Value.ToString(),

                    VersionDeprecated = editParameterGridView.Rows[rows].Cells[4].Value == null ?
                                       "" : editParameterGridView.Rows[rows].Cells[4].Value.ToString()
                };
                parameters.Add(parameter);
            }
            return parameters;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hasXMLFileBeenOpened = false;
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

        private void HideDataGridView()
        {
            editParameterGridView.Visible = false;
            newParameterDataGridView.Visible = false;
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml File|*.xml";
            saveFileDialog.Title = "Save an XML File";
            saveFileDialog.ShowDialog();

            List<ConfigurationParameter> parameters = new List<ConfigurationParameter>(); 
            if (saveFileDialog.FileName != "")
            {

                if (hasXMLFileBeenOpened)
                {
                    parameters = GetConfigParametersFromEditDataGridView();
                }
                else
                {
                    parameters = GetConfigParametersFromNewDataGridView();
                }
                var fileName = saveFileDialog.FileName;
                _xmlFilePath = fileName;
                _configurationService.SaveConfigurationParameters(parameters, _xmlFilePath);

            }
        }

        private List<ConfigurationParameter> GetConfigParametersFromNewDataGridView()
        {
            var parameters = new List<ConfigurationParameter>();

            for (int rows = 0; rows < newParameterDataGridView.Rows.Count - 1; rows++)
            {

                var parameter = new ConfigurationParameter();
                parameter.ApplicationName = newParameterDataGridView.Rows[rows].Cells[0].Value == null ?
                          "" : newParameterDataGridView.Rows[rows].Cells[0].Value.ToString();

                parameter.TagName = newParameterDataGridView.Rows[rows].Cells[1].Value == null ?
                          "" : newParameterDataGridView.Rows[rows].Cells[1].Value.ToString();

                parameter.Value = newParameterDataGridView.Rows[rows].Cells[2].Value == null ?
                            "" : newParameterDataGridView.Rows[rows].Cells[2].Value.ToString();

                parameter.SampleValue = newParameterDataGridView.Rows[rows].Cells[3].Value == null ?
                            "" : newParameterDataGridView.Rows[rows].Cells[3].Value.ToString();

                parameter.Description = newParameterDataGridView.Rows[rows].Cells[4].Value == null ?
                              "" : newParameterDataGridView.Rows[rows].Cells[4].Value.ToString();

                parameter.VersionAdded = newParameterDataGridView.Rows[rows].Cells[5].Value == null ?
                                      "" : newParameterDataGridView.Rows[rows].Cells[5].Value.ToString();

                parameter.VersionDeprecated = newParameterDataGridView.Rows[rows].Cells[6].Value == null ?
                                   "" : newParameterDataGridView.Rows[rows].Cells[6].Value.ToString();

                parameters.Add(parameter);
            }
            return parameters;
        }

        private void newParameterDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (newParameterDataGridView.Columns[e.ColumnIndex].Name == "DeleteRow")
            {

                if (
                    MessageBox.Show("Are you sure want to delete this record?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                    )
                {
                    newConfigurationParameterBindingSource.RemoveCurrent();
                }
            }
        }

        private void editParameterGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (editParameterGridView.Columns[e.ColumnIndex].Name == "Delete")
            {

                if (
                    MessageBox.Show("Are you sure want to delete this record?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                    )
                {
                    configurationParameterBindingSource.RemoveCurrent();
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parametersFromUnmodifiedXmlFile = _configurationService.QueryXml("test90.xml");
            var parameters = GetConfigParametersFromEditDataGridView();
            if (IsXmlFileContentNeedsToBeSaved(parametersFromUnmodifiedXmlFile, parameters))
            {
                if (MessageBox.Show("Do you want to save the changes to file?",
                    "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _configurationService.SaveConfigurationParameters(parameters,"");
                }
            }
            Application.Exit();

        }
        private bool IsXmlFileContentNeedsToBeSaved(
           List<ConfigurationParameter> parametersFromUnmodifiedXmlFile,
           List<ConfigurationParameter> parameters)
        {
            var isParametersHaveBeenModified = parameters
                                         .SequenceEqual(parametersFromUnmodifiedXmlFile);
            return !isParametersHaveBeenModified;
        }
    }
}
