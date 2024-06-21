using DevExpress.XtraEditors;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStructureGenerator
{
    public partial class FormDatabaseConfiguration : DevExpress.XtraEditors.XtraForm
    {
        public FormDatabaseConfiguration()
        {
            InitializeComponent();
        }

        private void FormDatabaseConfiguration_Load(object sender, EventArgs e)
        {
            txtMSSQL.Text = Properties.Settings.Default.MSSQLConnectionString;
            txtPostgresSQL.Text = Properties.Settings.Default.PostgresConnectionString;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save the updated values to settings
            Properties.Settings.Default.MSSQLConnectionString = txtMSSQL.Text;
            Properties.Settings.Default.PostgresConnectionString = txtPostgresSQL.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            btnTest.Text = "Testing...";
            // Test MSSQL connection
            await TestMSSQLConnectionAsync(txtMSSQL.Text);
            btnTest.Text = "Test";

        }

        private async void btnTest2_Click(object sender, EventArgs e)
        {
            btnTest.Text = "Testing...";
            // Test PostgreSQL connection
            await TestPostgresConnectionAsync(txtPostgresSQL.Text);
            btnTest.Text = "Test";
        }

        private async Task TestMSSQLConnectionAsync(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    MessageBox.Show("MSSQL Connection OK.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MSSQL Connection Failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task TestPostgresConnectionAsync(string connectionString)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    MessageBox.Show("PostgreSQL Connection OK.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PostgreSQL Connection Failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}