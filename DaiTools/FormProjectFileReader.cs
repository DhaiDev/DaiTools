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
    public partial class FormProjectFileReader : Form
    {
        public FormProjectFileReader()
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
                ReadFilesRecursively(selectedPath, structure);
                txtResult.Text = structure.ToString();
            }
            else
            {
                MessageBox.Show("Please select a valid directory.", "Invalid Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadFilesRecursively(string directory, StringBuilder structure)
        {
            string[] excludedFolders = new[] { "bin", "obj", ".vs", "debug", "Migrations" };

            foreach (var dir in Directory.GetDirectories(directory))
            {
                if (!excludedFolders.Any(excluded => dir.EndsWith(excluded, StringComparison.OrdinalIgnoreCase)))
                {
                    ReadFilesRecursively(dir, structure);
                }
            }

            foreach (var file in Directory.GetFiles(directory))
            {
                structure.AppendLine($"File: {file}");
                try
                {
                    string fileContent = File.ReadAllText(file);
                    structure.AppendLine(fileContent);
                    structure.AppendLine();
                }
                catch (Exception ex)
                {
                    structure.AppendLine($"Error reading file: {ex.Message}");
                }
            }
        }
    }
}
