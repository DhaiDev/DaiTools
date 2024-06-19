using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStructureGenerator
{
    public partial class FormProjectFoldersructureBuilder : Form
    {
        public FormProjectFoldersructureBuilder()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string selectedPath = txtPath.Text;
            if (Directory.Exists(selectedPath))
            {
                StringBuilder structure = new StringBuilder();
                GenerateStructure(selectedPath, structure, "");
                txtResult.Text = structure.ToString();
            }
            else
            {
                MessageBox.Show("Please select a valid directory.", "Invalid Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateStructure(string directory, StringBuilder structure, string indent)
        {
            string[] excludedFolders = new[] { "bin", "obj", ".vs", "debug" };

            DirectoryInfo dirInfo = new DirectoryInfo(directory);
            structure.AppendLine($"{indent}{dirInfo.Name}/");

            foreach (var dir in dirInfo.GetDirectories())
            {
                if (Array.Exists(excludedFolders, element => element.Equals(dir.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }
                GenerateStructure(dir.FullName, structure, indent + "├── ");
            }

            foreach (var file in dirInfo.GetFiles())
            {
                structure.AppendLine($"{indent}│   ├── {file.Name}");
            }
        }
    }
}
