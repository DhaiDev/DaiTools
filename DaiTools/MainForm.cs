﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStructureGenerator
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnProjectFolderSB_Click(object sender, EventArgs e)
        {

            FormProjectFoldersructureBuilder formProjectFoldersructureBuilder = new FormProjectFoldersructureBuilder();
            formProjectFoldersructureBuilder.Show();
        }

        private void btnProjectFileReader_Click(object sender, EventArgs e)
        {
            FormProjectFileReader formProjectFileReader = new FormProjectFileReader();
            formProjectFileReader.Show();
        }
    }
}