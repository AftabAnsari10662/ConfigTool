using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ConfigurationTool.Models;
using ConfigurationTool.Service;
using System.Configuration;

namespace ConfigurationTool
{
    public partial class ConfigTool : Form
    {
        ConfigurationService _configurationService;
        string _xmlFilePath;
        string _applicationName;
        string _repositoryXmlFileName;
        public ConfigTool()
        {
            InitializeComponent();
            _configurationService = new ConfigurationService();
            _xmlFilePath = string.Empty;
            _applicationName = string.Empty;
            _repositoryXmlFileName = ConfigurationManager.AppSettings["xmlRepoistoryFilePath"];
            taggedParameterDataGridView.Visible = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taggedParameterBindingSource.Clear();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;
                _xmlFilePath = fileName;
                ActiveForm.Text = _xmlFilePath;
                var configurationParameters = _configurationService.QueryXml(fileName);

                foreach (var parameter in configurationParameters)
                {
                    taggedParameterBindingSource.Add(parameter);
                }

                taggedParameterDataGridView.DataSource = taggedParameterBindingSource;
            }

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_xmlFilePath != string.Empty)
            {
                var parameters = GetTaggedParametersFromDataGridView();
                _configurationService.SaveConfigurationParameters(parameters, _xmlFilePath);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml File|*.xml";
            saveFileDialog.Title = "Save an XML File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != string.Empty)
            {
                var parameters = GetTaggedParametersFromDataGridView();
                var fileName = saveFileDialog.FileName;
                _xmlFilePath = fileName;
                ActiveForm.Text = _xmlFilePath;
                _configurationService.SaveConfigurationParameters(parameters, fileName);
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xml File|*.xml";
            saveFileDialog.Title = "Save an XML File";
            saveFileDialog.ShowDialog();

            var parameters = new List<ConfigurationParameter>();
            if (saveFileDialog.FileName != string.Empty)
            {
                var fileName = saveFileDialog.FileName;
                _xmlFilePath = fileName;
                ActiveForm.Text = _xmlFilePath;
                parameters = GetTaggedParametersFromDataGridView();

                _configurationService.SaveConfigurationParameters(parameters, _xmlFilePath);

            }
        }

        private List<ConfigurationParameter> GetTaggedParametersFromDataGridView()
        {
            var parameters = new List<ConfigurationParameter>();

            for (int rows = 0; rows < taggedParameterDataGridView.Rows.Count - 1; rows++)
            {

                var parameter = new ConfigurationParameter();
                parameter.ApplicationName = _applicationName;

                parameter.TagName = taggedParameterDataGridView.Rows[rows].Cells[0].Value == null ?
                          "" : taggedParameterDataGridView.Rows[rows].Cells[0].Value.ToString();

                parameter.Value = taggedParameterDataGridView.Rows[rows].Cells[1].Value == null ?
                            "" : taggedParameterDataGridView.Rows[rows].Cells[1].Value.ToString();

                parameter.SampleValue = taggedParameterDataGridView.Rows[rows].Cells[2].Value == null ?
                            "" : taggedParameterDataGridView.Rows[rows].Cells[2].Value.ToString();

                parameter.Description = taggedParameterDataGridView.Rows[rows].Cells[3].Value == null ?
                              "" : taggedParameterDataGridView.Rows[rows].Cells[3].Value.ToString();

                parameter.VersionAdded = taggedParameterDataGridView.Rows[rows].Cells[4].Value == null ?
                                      "" : taggedParameterDataGridView.Rows[rows].Cells[4].Value.ToString();

                parameter.VersionDeprecated = taggedParameterDataGridView.Rows[rows].Cells[5].Value == null ?
                                   "" : taggedParameterDataGridView.Rows[rows].Cells[5].Value.ToString();

                parameters.Add(parameter);
            }
            return parameters;
        }

        private void taggedParameterDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (taggedParameterDataGridView.Columns[e.ColumnIndex].Name == "DeleteRow")
            {

                if (
                    MessageBox.Show("Are you sure want to delete this record?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                    )
                {
                    taggedParameterBindingSource.RemoveCurrent();
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parametersFromUnmodifiedXmlFile = _configurationService.QueryXml("test90.xml");
            var parameters = GetTaggedParametersFromDataGridView();
            if (_configurationService.IsXmlFileContentNeedsToBeSaved(parametersFromUnmodifiedXmlFile, parameters))
            {
                if (MessageBox.Show("Do you want to save the changes to file?",
                    "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _configurationService.SaveConfigurationParameters(parameters, "");
                }
            }
            Application.Exit();

        }

        private void aMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _xmlFilePath = string.Empty;
            // ActiveForm.Text = _xmlFilePath;
            var amConfigParameters = _configurationService
                                        .QueryXml(_repositoryXmlFileName)
                                        .Where(c => c.ApplicationName == "AM")
                                        .ToList();

            taggedParameterBindingSource.Clear();
            foreach (var parameter in amConfigParameters)
            {
                taggedParameterBindingSource.Add(parameter);
            }

            taggedParameterDataGridView.DataSource = taggedParameterBindingSource;
        }

        private void pMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _xmlFilePath = string.Empty;
            ActiveForm.Text = _xmlFilePath;
            var pmConfigParameters = _configurationService
                                                .QueryXml(_repositoryXmlFileName)
                                                .Where(c => c.ApplicationName == "PM")
                                                .ToList();

            taggedParameterBindingSource.Clear();
            foreach (var parameter in pmConfigParameters)
            {
                taggedParameterBindingSource.Add(parameter);
            }


            taggedParameterDataGridView.DataSource = taggedParameterBindingSource;
            taggedParameterDataGridView.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var oldVersionOfXmlFileName = openFileDialog.FileName;
                var oldTaggedConfigurations = _configurationService.QueryXml(oldVersionOfXmlFileName);

                var latestVersionOfXmlFileName = _repositoryXmlFileName;
                var latestTaggedConfigurations = _configurationService.QueryXml(latestVersionOfXmlFileName);

                var latestTaggedConfigurationWithValues = _configurationService
                                                                 .ReplaceValuesFromOldTaggedConfigurationIntoLatestTaggedConfiguration(
                                                                         oldTaggedConfigurations,
                                                                         latestTaggedConfigurations
                                                                 );

                taggedParameterBindingSource.Clear();

                foreach (var taggedConfig in latestTaggedConfigurationWithValues)
                {
                    taggedParameterBindingSource.Add(taggedConfig);
                }

                taggedParameterDataGridView.DataSource = taggedParameterBindingSource;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Resize DataGridView to full height.
            int height = taggedParameterDataGridView.Height;
            taggedParameterDataGridView.Height = taggedParameterDataGridView.RowCount * taggedParameterDataGridView.RowTemplate.Height;

            //Create a Bitmap and draw the DataGridView on it.
            Bitmap bitmap = new Bitmap(this.taggedParameterDataGridView.Width,
                this.taggedParameterDataGridView.Height);

            taggedParameterDataGridView.DrawToBitmap(bitmap,
                new Rectangle(
                    0,
                0,
                this.taggedParameterDataGridView.Width,
                this.taggedParameterDataGridView.Height
                ));

            //Resize DataGridView back to original height.
            taggedParameterDataGridView.Height = height;

            //Show the Print Preview Dialog.
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
