using DevExpress.XtraEditors;

namespace ProjectStructureGenerator
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnProjectFolderSB = new DevExpress.XtraEditors.SimpleButton();
            this.btnProjectFileReader = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 34);
            this.panel1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 8);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(124, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Development Tools";
            // 
            // btnProjectFolderSB
            // 
            this.btnProjectFolderSB.Location = new System.Drawing.Point(14, 50);
            this.btnProjectFolderSB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProjectFolderSB.Name = "btnProjectFolderSB";
            this.btnProjectFolderSB.Size = new System.Drawing.Size(177, 55);
            this.btnProjectFolderSB.TabIndex = 1;
            this.btnProjectFolderSB.Text = "Project Folder sructure Builder";
            this.btnProjectFolderSB.Click += new System.EventHandler(this.btnProjectFolderSB_Click);
            // 
            // btnProjectFileReader
            // 
            this.btnProjectFileReader.Location = new System.Drawing.Point(14, 119);
            this.btnProjectFileReader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProjectFileReader.Name = "btnProjectFileReader";
            this.btnProjectFileReader.Size = new System.Drawing.Size(177, 55);
            this.btnProjectFileReader.TabIndex = 2;
            this.btnProjectFileReader.Text = "Project File Reader";
            this.btnProjectFileReader.Click += new System.EventHandler(this.btnProjectFileReader_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 451);
            this.Controls.Add(this.btnProjectFileReader);
            this.Controls.Add(this.btnProjectFolderSB);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Dhai Development Tools";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private LabelControl labelControl1;
        private SimpleButton btnProjectFolderSB;
        private SimpleButton btnProjectFileReader;
    }
}