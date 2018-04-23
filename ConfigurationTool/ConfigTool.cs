using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ConfigurationTool.Models;
using ConfigurationTool.Service;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.Collections;

namespace ConfigurationTool
{
    public partial class ConfigTool : Form
    {
        ConfigurationService _configurationService;
        string _currentXmlFilePath;
        string _applicationName;
        string _repositoryXmlFilePath;
        readonly string _actionManager;
        readonly string _performanceManager;
        Bitmap bitmap;
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        public ConfigTool()
        {
            InitializeComponent();
            _configurationService = new ConfigurationService();
            _currentXmlFilePath = string.Empty;
            _applicationName = string.Empty;
            _repositoryXmlFilePath = ConfigurationManager.AppSettings["xmlRepoistoryFilePath"];
            _actionManager = "ActionManager";
            _performanceManager = "PerformanceManager";
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
                ShowTaggedParameterDataGridView();
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
                                        .getTaggedPrametersForApplication(
                                                    _repositoryXmlFilePath,
                                                    _actionManager
                                                    );

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
                                              .getTaggedPrametersForApplication(
                                                                    _repositoryXmlFilePath,
                                                                    _performanceManager
                                                                    );

            PopulateTaggedParametersGridViewWithNewDataSource(pmConfigParameters);
            taggedParameterDataGridView.Show();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;

            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "Tagged Repository Print";
                printDocument1.Print();
            }

            //Open the print preview dialog
            //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            //objPPdialog.Document = printDocument1;
            //objPPdialog.ShowDialog();

        }

        private void aMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportTaggedParameterForSpecificApplication(_actionManager);
        }

        private void pMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImportTaggedParameterForSpecificApplication(_performanceManager);
        }

        private void ImportTaggedParameterForSpecificApplication(string applicationName)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ShowTaggedParameterDataGridView();

                var oldVersionOfXmlFileName = openFileDialog.FileName;
                var oldTaggedConfigurations = _configurationService.QueryXml(oldVersionOfXmlFileName);

                var latestVersionOfXmlFileName = _repositoryXmlFilePath;
                var latestTaggedConfigurations = _configurationService
                                                                .getTaggedPrametersForApplication(
                                                                            latestVersionOfXmlFileName,
                                                                            applicationName
                                                                            );

                var latestTaggedConfigurationWithValues = _configurationService
                                                                 .ReplaceValuesFromOldTaggedConfigurationIntoLatestTaggedConfiguration(
                                                                         oldTaggedConfigurations,
                                                                         latestTaggedConfigurations
                                                                 );
                PopulateTaggedParametersGridViewWithNewDataSource(latestTaggedConfigurationWithValues);
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in taggedParameterDataGridView.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in taggedParameterDataGridView.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= taggedParameterDataGridView.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = taggedParameterDataGridView.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Tag Summary", new Font(taggedParameterDataGridView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Tag Summary", new Font(taggedParameterDataGridView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(taggedParameterDataGridView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(taggedParameterDataGridView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Tag Summary", new Font(
                                        new Font(taggedParameterDataGridView.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in taggedParameterDataGridView.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
