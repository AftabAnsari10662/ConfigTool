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
        string _currentXmlFilePath;
        string _applicationName;
        string _repositoryXmlFilePath;
        public ConfigTool()
        {
            InitializeComponent();
            _configurationService = new ConfigurationService();
            _currentXmlFilePath = string.Empty;
            _applicationName = string.Empty;
            _repositoryXmlFilePath = ConfigurationManager.AppSettings["xmlRepoistoryFilePath"];
            taggedParameterDataGridView.Visible = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;
                _currentXmlFilePath = fileName;
                ActiveForm.Text = _currentXmlFilePath;
                var configurationParameters = _configurationService.QueryXml(fileName);
                PopulateTaggedParametersGridViewWithNewDataSource(configurationParameters);
            }

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentXmlFilePath != string.Empty)
            {
                var parameters = GetTaggedParametersFromDataGridView();
                _configurationService.SaveConfigurationParameters(parameters, _currentXmlFilePath);
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
                _currentXmlFilePath = fileName;
                ActiveForm.Text = _currentXmlFilePath;
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
                _currentXmlFilePath = fileName;
                ActiveForm.Text = _currentXmlFilePath;
                parameters = GetTaggedParametersFromDataGridView();
                _configurationService.SaveConfigurationParameters(parameters, _currentXmlFilePath);

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

                parameter.Description = taggedParameterDataGridView.Rows[rows].Cells[2].Value == null ?
                                        "" : taggedParameterDataGridView.Rows[rows].Cells[2].Value.ToString();

                parameter.SampleValue = taggedParameterDataGridView.Rows[rows].Cells[3].Value == null ?
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
            var taggedParametersFromUnmodifiedXmlFile = _configurationService.QueryXml(_repositoryXmlFilePath);
            var taggedParametersInDataGridView = GetTaggedParametersFromDataGridView();
            if (
                _configurationService.IsXmlFileContentNeedsToBeSaved(
                taggedParametersFromUnmodifiedXmlFile,
                taggedParametersInDataGridView)
                )
            {
                if (MessageBox.Show("Do you want to save the changes to file?",
                    "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _configurationService.SaveConfigurationParameters(taggedParametersInDataGridView, _currentXmlFilePath);
                }
            }
            Application.Exit();

        }

        private void aMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTaggedParameterDataGridView();
            _currentXmlFilePath = string.Empty;
            // ActiveForm.Text = _xmlFilePath;
            var amConfigParameters = _configurationService
                                        .getTaggedPrametersForActionManager(_repositoryXmlFilePath);

            PopulateTaggedParametersGridViewWithNewDataSource(amConfigParameters);

        }

        private void PopulateTaggedParametersGridViewWithNewDataSource(
            List<ConfigurationParameter> amConfigParameters)
        {
            taggedParameterBindingSource.Clear();
            foreach (var parameter in amConfigParameters)
            {
                taggedParameterBindingSource.Add(parameter);
            }

            taggedParameterDataGridView.DataSource = taggedParameterBindingSource;
        }

        private void ShowTaggedParameterDataGridView()
        {
            taggedParameterDataGridView.Show();
        }

        private void pMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTaggedParameterDataGridView();
            _currentXmlFilePath = string.Empty;
            ActiveForm.Text = _currentXmlFilePath;
            var pmConfigParameters = _configurationService
                                              .getTaggedPrametersForPerformanceManager(_repositoryXmlFilePath);

            PopulateTaggedParametersGridViewWithNewDataSource(pmConfigParameters);
            taggedParameterDataGridView.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ShowTaggedParameterDataGridView();

                var oldVersionOfXmlFileName = openFileDialog.FileName;
                var oldTaggedConfigurations = _configurationService.QueryXml(oldVersionOfXmlFileName);

                var latestVersionOfXmlFileName = _repositoryXmlFilePath;
                var latestTaggedConfigurations = _configurationService.QueryXml(latestVersionOfXmlFileName);

                var latestTaggedConfigurationWithValues = _configurationService
                                                                 .ReplaceValuesFromOldTaggedConfigurationIntoLatestTaggedConfiguration(
                                                                         oldTaggedConfigurations,
                                                                         latestTaggedConfigurations
                                                                 );
                PopulateTaggedParametersGridViewWithNewDataSource(latestTaggedConfigurationWithValues);
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
