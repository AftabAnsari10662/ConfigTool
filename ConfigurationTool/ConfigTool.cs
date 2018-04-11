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
        ConfigurationService service;
        bool hasXMLFileBeenOpened = false;
        public ConfigTool()
        {
            InitializeComponent();
            HideDataGridView();
            service = new ConfigurationService();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configurationParameterBindingSource.Clear();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                hasXMLFileBeenOpened = true;
                StreamReader sr = new StreamReader(openFileDialog.FileName);

                var configurationParameters = service.QueryXml(openFileDialog.FileName);

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


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasXMLFileBeenOpened)
            {
                var parameters = GetConfigParametersFromEditDataGridView();
                service.SaveConfigurationParameters(parameters, "");
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Xml File|*.xml";
            saveFileDialog1.Title = "Save an XML File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                var parameters = GetConfigParametersFromNewDataGridView();
                service.SaveConfigurationParameters(parameters, "");
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                fs.Close();
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
                    ApplicationName = editParameterGridView.Rows[rows].Cells[0].Value == null ?
                         "" : editParameterGridView.Rows[rows].Cells[0].Value.ToString(),

                    KeyName = editParameterGridView.Rows[rows].Cells[1].Value == null ?
                               "" : editParameterGridView.Rows[rows].Cells[1].Value.ToString(),

                    Value = editParameterGridView.Rows[rows].Cells[2].Value == null ?
                                "" : editParameterGridView.Rows[rows].Cells[2].Value.ToString(),

                    TaggedValue = newParameterDataGridView.Rows[rows].Cells[3].Value == null ?
                          "" : newParameterDataGridView.Rows[rows].Cells[3].Value.ToString(),

                    Description = editParameterGridView.Rows[rows].Cells[4].Value == null ?
                                  "" : editParameterGridView.Rows[rows].Cells[4].Value.ToString(),

                    DecrementVersion = editParameterGridView.Rows[rows].Cells[5].Value == null ?
                                          "" : editParameterGridView.Rows[rows].Cells[5].Value.ToString(),

                    IncludeVersion = editParameterGridView.Rows[rows].Cells[6].Value == null ?
                                       "" : editParameterGridView.Rows[rows].Cells[6].Value.ToString()
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

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog.FileName != "")
            {

                if (hasXMLFileBeenOpened)
                {
                    var parameters = GetConfigParametersFromEditDataGridView();
                    service.SaveConfigurationParameters(parameters, "");
                }
                else
                {
                    var parameters = GetConfigParametersFromNewDataGridView();
                    service.SaveConfigurationParameters(parameters, "");
                }
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

                parameter.KeyName = newParameterDataGridView.Rows[rows].Cells[1].Value == null ?
                          "" : newParameterDataGridView.Rows[rows].Cells[1].Value.ToString();

                parameter.Value = newParameterDataGridView.Rows[rows].Cells[2].Value == null ?
                            "" : newParameterDataGridView.Rows[rows].Cells[2].Value.ToString();

                parameter.TaggedValue = newParameterDataGridView.Rows[rows].Cells[3].Value == null ?
                          "" : newParameterDataGridView.Rows[rows].Cells[3].Value.ToString();

                parameter.Description = newParameterDataGridView.Rows[rows].Cells[4].Value == null ?
                              "" : newParameterDataGridView.Rows[rows].Cells[4].Value.ToString();

                parameter.DecrementVersion = newParameterDataGridView.Rows[rows].Cells[5].Value == null ?
                                      "" : newParameterDataGridView.Rows[rows].Cells[5].Value.ToString();

                parameter.IncludeVersion = newParameterDataGridView.Rows[rows].Cells[6].Value == null ?
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
        private void editParameterGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>-1)
            {
                var value = editParameterGridView[e.ColumnIndex, e.RowIndex].Value;
                var taggedValue = editParameterGridView[3, e.RowIndex].Value;

                for (int rows = 0; rows < editParameterGridView.Rows.Count - 1; rows++)
                {
                    if (editParameterGridView.Rows[rows].Cells[3].Value == taggedValue)
                    {
                        editParameterGridView.Rows[rows].Cells[2].Value = value;
                    }

                }
                   
            }

        }

    }
}
