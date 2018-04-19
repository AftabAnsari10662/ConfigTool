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
        bool _hasXMLFileBeenOpened = false;
        string _xmlFilePath;
        string _applicationName;
        string _repositoryXmlFileName;
        public ConfigTool()
        {
            InitializeComponent();
            _configurationService = new ConfigurationService();
            _xmlFilePath = string.Empty;
            _applicationName = string.Empty;
            _repositoryXmlFileName = "taggedXml.xml";

            newConfigurationParameterBindingSource.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newConfigurationParameterBindingSource.Clear();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _hasXMLFileBeenOpened = true;
                var fileName = openFileDialog.FileName;
                _xmlFilePath = fileName;
                ActiveForm.Text = _xmlFilePath;
                var configurationParameters = _configurationService.QueryXml(fileName);

                foreach (var parameter in configurationParameters)
                {
                    newConfigurationParameterBindingSource.Add(parameter);
                }

                newParameterDataGridView.DataSource = newConfigurationParameterBindingSource;
            }

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_xmlFilePath !=string.Empty)
            {
                var parameters = GetConfigParametersFromNewDataGridView();
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
                ActiveForm.Text = _xmlFilePath;
                _configurationService.SaveConfigurationParameters(parameters, fileName);
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _hasXMLFileBeenOpened = false;
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

                if (_hasXMLFileBeenOpened)
                {
                    parameters = GetConfigParametersFromNewDataGridView();
                }
                else
                {
                    parameters = GetConfigParametersFromNewDataGridView();
                }
                var fileName = saveFileDialog.FileName;
                _xmlFilePath = fileName;
                ActiveForm.Text = _xmlFilePath;
                _configurationService.SaveConfigurationParameters(parameters, _xmlFilePath);

            }
        }

        private List<ConfigurationParameter> GetConfigParametersFromNewDataGridView()
        {
            var parameters = new List<ConfigurationParameter>();

            for (int rows = 0; rows < newParameterDataGridView.Rows.Count - 1; rows++)
            {

                var parameter = new ConfigurationParameter();
                parameter.ApplicationName = _applicationName;

                parameter.TagName = newParameterDataGridView.Rows[rows].Cells[0].Value == null ?
                          "" : newParameterDataGridView.Rows[rows].Cells[0].Value.ToString();

                parameter.Value = newParameterDataGridView.Rows[rows].Cells[1].Value == null ?
                            "" : newParameterDataGridView.Rows[rows].Cells[1].Value.ToString();

                parameter.SampleValue = newParameterDataGridView.Rows[rows].Cells[2].Value == null ?
                            "" : newParameterDataGridView.Rows[rows].Cells[2].Value.ToString();

                parameter.Description = newParameterDataGridView.Rows[rows].Cells[3].Value == null ?
                              "" : newParameterDataGridView.Rows[rows].Cells[3].Value.ToString();

                parameter.VersionAdded = newParameterDataGridView.Rows[rows].Cells[4].Value == null ?
                                      "" : newParameterDataGridView.Rows[rows].Cells[4].Value.ToString();

                parameter.VersionDeprecated = newParameterDataGridView.Rows[rows].Cells[5].Value == null ?
                                   "" : newParameterDataGridView.Rows[rows].Cells[5].Value.ToString();

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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parametersFromUnmodifiedXmlFile = _configurationService.QueryXml("test90.xml");
            var parameters = GetConfigParametersFromNewDataGridView();
            if (_configurationService.IsXmlFileContentNeedsToBeSaved(parametersFromUnmodifiedXmlFile, parameters))
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

        private void aMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _xmlFilePath = string.Empty;
            ActiveForm.Text = _xmlFilePath;
            var amConfigParameters = _configurationService
                .QueryXml(_repositoryXmlFileName)
                .Where(c=>c.ApplicationName == "AM")
                .ToList();

            newConfigurationParameterBindingSource.Clear();
            foreach (var parameter in amConfigParameters)
            {
                newConfigurationParameterBindingSource.Add(parameter);
            }

            newParameterDataGridView.DataSource = newConfigurationParameterBindingSource;
            newParameterDataGridView.Show();
    }

        private void pMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _xmlFilePath = string.Empty;
            ActiveForm.Text = _xmlFilePath;
            var pmConfigParameters = _configurationService
                .QueryXml(_repositoryXmlFileName)
                .Where(c => c.ApplicationName == "PM")
                .ToList();

            newConfigurationParameterBindingSource.Clear();
            foreach (var parameter in pmConfigParameters)
            {
                newConfigurationParameterBindingSource.Add(parameter);
            }

            newParameterDataGridView.DataSource = newConfigurationParameterBindingSource;
            newParameterDataGridView.Show();
        }
    }
}
