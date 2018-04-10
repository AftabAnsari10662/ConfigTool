namespace ConfigurationTool
{
    partial class ConfigTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runConfigurationTaggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runTaggingValidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.editParameterGridView = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.includeVersionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.decrementVersionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configurationParameterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.newParameterDataGridView = new System.Windows.Forms.DataGridView();
            this.newConfigurationParameterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.includeVersionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.decrementVersionDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editParameterGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configurationParameterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newParameterDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newConfigurationParameterBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.runToolStripMenuItem,
            this.printToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1070, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(158, 30);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runConfigurationTaggingToolStripMenuItem,
            this.runTaggingValidationToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(55, 29);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // runConfigurationTaggingToolStripMenuItem
            // 
            this.runConfigurationTaggingToolStripMenuItem.Name = "runConfigurationTaggingToolStripMenuItem";
            this.runConfigurationTaggingToolStripMenuItem.Size = new System.Drawing.Size(309, 30);
            this.runConfigurationTaggingToolStripMenuItem.Text = "Run Configuration Tagging";
            // 
            // runTaggingValidationToolStripMenuItem
            // 
            this.runTaggingValidationToolStripMenuItem.Name = "runTaggingValidationToolStripMenuItem";
            this.runTaggingValidationToolStripMenuItem.Size = new System.Drawing.Size(309, 30);
            this.runTaggingValidationToolStripMenuItem.Text = "Run Tagging Validation";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(60, 29);
            this.printToolStripMenuItem.Text = "Print";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // editParameterGridView
            // 
            this.editParameterGridView.AutoGenerateColumns = false;
            this.editParameterGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.editParameterGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.includeVersionDataGridViewTextBoxColumn,
            this.decrementVersionDataGridViewTextBoxColumn});
            this.editParameterGridView.DataSource = this.configurationParameterBindingSource;
            this.editParameterGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editParameterGridView.Location = new System.Drawing.Point(0, 33);
            this.editParameterGridView.Name = "editParameterGridView";
            this.editParameterGridView.RowTemplate.Height = 28;
            this.editParameterGridView.Size = new System.Drawing.Size(1070, 570);
            this.editParameterGridView.TabIndex = 1;
           
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 300;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 400;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.Width = 300;
            // 
            // includeVersionDataGridViewTextBoxColumn
            // 
            this.includeVersionDataGridViewTextBoxColumn.DataPropertyName = "IncludeVersion";
            this.includeVersionDataGridViewTextBoxColumn.HeaderText = "IncludeVersion";
            this.includeVersionDataGridViewTextBoxColumn.Name = "includeVersionDataGridViewTextBoxColumn";
            // 
            // decrementVersionDataGridViewTextBoxColumn
            // 
            this.decrementVersionDataGridViewTextBoxColumn.DataPropertyName = "DecrementVersion";
            this.decrementVersionDataGridViewTextBoxColumn.HeaderText = "DecrementVersion";
            this.decrementVersionDataGridViewTextBoxColumn.Name = "decrementVersionDataGridViewTextBoxColumn";
            // 
            // configurationParameterBindingSource
            // 
            this.configurationParameterBindingSource.DataSource = typeof(ConfigurationTool.Models.ConfigurationParameter);
            // 
            // newParameterDataGridView
            // 
            this.newParameterDataGridView.AutoGenerateColumns = false;
            this.newParameterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newParameterDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn1,
            this.valueDataGridViewTextBoxColumn1,
            this.descriptionDataGridViewTextBoxColumn1,
            this.includeVersionDataGridViewTextBoxColumn1,
            this.decrementVersionDataGridViewTextBoxColumn1});
            this.newParameterDataGridView.DataSource = this.newConfigurationParameterBindingSource;
            this.newParameterDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newParameterDataGridView.Location = new System.Drawing.Point(0, 33);
            this.newParameterDataGridView.Name = "newParameterDataGridView";
            this.newParameterDataGridView.RowTemplate.Height = 28;
            this.newParameterDataGridView.Size = new System.Drawing.Size(1070, 570);
            this.newParameterDataGridView.TabIndex = 2;
            // 
            // newConfigurationParameterBindingSource
            // 
            this.newConfigurationParameterBindingSource.DataSource = typeof(ConfigurationTool.Models.ConfigurationParameter);
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.Width = 300;
            // 
            // valueDataGridViewTextBoxColumn1
            // 
            this.valueDataGridViewTextBoxColumn1.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn1.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn1.Name = "valueDataGridViewTextBoxColumn1";
            this.valueDataGridViewTextBoxColumn1.Width = 400;
            // 
            // descriptionDataGridViewTextBoxColumn1
            // 
            this.descriptionDataGridViewTextBoxColumn1.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn1.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn1.Name = "descriptionDataGridViewTextBoxColumn1";
            this.descriptionDataGridViewTextBoxColumn1.Width = 300;
            // 
            // includeVersionDataGridViewTextBoxColumn1
            // 
            this.includeVersionDataGridViewTextBoxColumn1.DataPropertyName = "IncludeVersion";
            this.includeVersionDataGridViewTextBoxColumn1.HeaderText = "IncludeVersion";
            this.includeVersionDataGridViewTextBoxColumn1.Name = "includeVersionDataGridViewTextBoxColumn1";
            // 
            // decrementVersionDataGridViewTextBoxColumn1
            // 
            this.decrementVersionDataGridViewTextBoxColumn1.DataPropertyName = "DecrementVersion";
            this.decrementVersionDataGridViewTextBoxColumn1.HeaderText = "DecrementVersion";
            this.decrementVersionDataGridViewTextBoxColumn1.Name = "decrementVersionDataGridViewTextBoxColumn1";
            // 
            // ConfigTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 603);
            this.Controls.Add(this.newParameterDataGridView);
            this.Controls.Add(this.editParameterGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConfigTool";
            this.Text = "Configuration Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editParameterGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configurationParameterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newParameterDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newConfigurationParameterBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem runConfigurationTaggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runTaggingValidationToolStripMenuItem;
        private System.Windows.Forms.DataGridView editParameterGridView;
        private System.Windows.Forms.BindingSource configurationParameterBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn includeVersionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn decrementVersionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView newParameterDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn includeVersionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn decrementVersionDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource newConfigurationParameterBindingSource;
    }
}

