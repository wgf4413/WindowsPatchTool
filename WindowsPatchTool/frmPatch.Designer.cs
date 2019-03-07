namespace WindowsPatchTool
{
    partial class frmPatch
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tbUpdateID = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.tbDetail = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbImagePath = new System.Windows.Forms.TextBox();
            this.tbPatchPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbUpdateFile = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbCondition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tvUpdates = new System.Windows.Forms.TreeView();
            this.msUpdate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuShowDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reduceAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildDismToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dismSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.msUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(74, 339);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(713, 114);
            this.textBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(111, 363);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "installed";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(203, 363);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "UpdateId";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbUpdateID
            // 
            this.tbUpdateID.Location = new System.Drawing.Point(302, 365);
            this.tbUpdateID.Name = "tbUpdateID";
            this.tbUpdateID.Size = new System.Drawing.Size(594, 21);
            this.tbUpdateID.TabIndex = 4;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(903, 363);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tbDetail
            // 
            this.tbDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbDetail.Location = new System.Drawing.Point(0, 378);
            this.tbDetail.Multiline = true;
            this.tbDetail.Name = "tbDetail";
            this.tbDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDetail.Size = new System.Drawing.Size(809, 171);
            this.tbDetail.TabIndex = 6;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1000, 363);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbImagePath);
            this.panel1.Controls.Add(this.tbPatchPath);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.tbUpdateFile);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.tbCondition);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 70);
            this.panel1.TabIndex = 0;
            // 
            // tbImagePath
            // 
            this.tbImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbImagePath.Location = new System.Drawing.Point(467, 38);
            this.tbImagePath.Name = "tbImagePath";
            this.tbImagePath.Size = new System.Drawing.Size(330, 21);
            this.tbImagePath.TabIndex = 9;
            // 
            // tbPatchPath
            // 
            this.tbPatchPath.Location = new System.Drawing.Point(74, 38);
            this.tbPatchPath.Name = "tbPatchPath";
            this.tbPatchPath.Size = new System.Drawing.Size(346, 21);
            this.tbPatchPath.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(426, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Patch";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(762, 10);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(35, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbUpdateFile
            // 
            this.tbUpdateFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUpdateFile.Location = new System.Drawing.Point(576, 11);
            this.tbUpdateFile.Name = "tbUpdateFile";
            this.tbUpdateFile.Size = new System.Drawing.Size(180, 21);
            this.tbUpdateFile.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(495, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbCondition
            // 
            this.tbCondition.Location = new System.Drawing.Point(74, 11);
            this.tbCondition.Name = "tbCondition";
            this.tbCondition.Size = new System.Drawing.Size(415, 21);
            this.tbCondition.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Condition";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 375);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(809, 3);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // tvUpdates
            // 
            this.tvUpdates.CheckBoxes = true;
            this.tvUpdates.ContextMenuStrip = this.msUpdate;
            this.tvUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUpdates.Location = new System.Drawing.Point(0, 70);
            this.tvUpdates.Name = "tvUpdates";
            this.tvUpdates.Size = new System.Drawing.Size(809, 305);
            this.tvUpdates.TabIndex = 11;
            this.tvUpdates.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvUpdates_AfterCheck);
            // 
            // msUpdate
            // 
            this.msUpdate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShowDetail,
            this.menuDownload,
            this.saveToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.reduceAllToolStripMenuItem,
            this.buildDismToolStripMenuItem,
            this.downloadAllToolStripMenuItem,
            this.dismSelectToolStripMenuItem});
            this.msUpdate.Name = "msUpdate";
            this.msUpdate.Size = new System.Drawing.Size(151, 180);
            // 
            // menuShowDetail
            // 
            this.menuShowDetail.Name = "menuShowDetail";
            this.menuShowDetail.Size = new System.Drawing.Size(150, 22);
            this.menuShowDetail.Text = "Show Detail";
            this.menuShowDetail.Click += new System.EventHandler(this.menuShowDetail_Click);
            // 
            // menuDownload
            // 
            this.menuDownload.Name = "menuDownload";
            this.menuDownload.Size = new System.Drawing.Size(150, 22);
            this.menuDownload.Text = "Download";
            this.menuDownload.Click += new System.EventHandler(this.menuDownload_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.saveToolStripMenuItem.Text = "Save Update";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.expandAllToolStripMenuItem.Text = "ExpandAll";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // reduceAllToolStripMenuItem
            // 
            this.reduceAllToolStripMenuItem.Name = "reduceAllToolStripMenuItem";
            this.reduceAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.reduceAllToolStripMenuItem.Text = "ReduceAll";
            this.reduceAllToolStripMenuItem.Click += new System.EventHandler(this.reduceAllToolStripMenuItem_Click);
            // 
            // buildDismToolStripMenuItem
            // 
            this.buildDismToolStripMenuItem.Name = "buildDismToolStripMenuItem";
            this.buildDismToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.buildDismToolStripMenuItem.Text = "Build Dism";
            this.buildDismToolStripMenuItem.Click += new System.EventHandler(this.buildDismToolStripMenuItem_Click);
            // 
            // downloadAllToolStripMenuItem
            // 
            this.downloadAllToolStripMenuItem.Name = "downloadAllToolStripMenuItem";
            this.downloadAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.downloadAllToolStripMenuItem.Text = "DownloadAll";
            this.downloadAllToolStripMenuItem.Click += new System.EventHandler(this.downloadAllToolStripMenuItem_Click);
            // 
            // dismSelectToolStripMenuItem
            // 
            this.dismSelectToolStripMenuItem.Name = "dismSelectToolStripMenuItem";
            this.dismSelectToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.dismSelectToolStripMenuItem.Text = "Dism Select";
            this.dismSelectToolStripMenuItem.Click += new System.EventHandler(this.dismSelectToolStripMenuItem_Click);
            // 
            // frmPatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 549);
            this.Controls.Add(this.tvUpdates);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.tbDetail);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tbUpdateID);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "frmPatch";
            this.Text = "Windows Patch Tool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.msUpdate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbUpdateID;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox tbDetail;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbImagePath;
        private System.Windows.Forms.TextBox tbPatchPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox tbUpdateFile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbCondition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TreeView tvUpdates;
        private System.Windows.Forms.ContextMenuStrip msUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuShowDetail;
        private System.Windows.Forms.ToolStripMenuItem menuDownload;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reduceAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildDismToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dismSelectToolStripMenuItem;
    }
}

