using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStructureGenerator
{
    public partial class FormSQLToEntities : DevExpress.XtraEditors.XtraForm
    {
        private string connectionString;

        public FormSQLToEntities()
        {
            InitializeComponent();
            LoadConnectionString();
            LoadDatabases();
        }

        private void LoadConnectionString()
        {
            connectionString = Properties.Settings.Default.MSSQLConnectionString;
        }

        private void LoadDatabases()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable databases = connection.GetSchema("Databases");
                lkDatabase.Properties.DataSource = databases;
                lkDatabase.Properties.DisplayMember = "database_name";
                lkDatabase.Properties.ValueMember = "database_name";
            }
        }

        private void lkDatabase_EditValueChanged(object sender, EventArgs e)
        {
            string selectedDatabase = lkDatabase.EditValue.ToString();
            LoadTables(selectedDatabase);
        }

        private void LoadTables(string database)
        {
            using (SqlConnection connection = new SqlConnection($"{connectionString};Initial Catalog={database}"))
            {
                connection.Open();
                DataTable tables = connection.GetSchema("Tables");
                cklistTable.DataSource = tables;
                cklistTable.DisplayMember = "TABLE_NAME";
                cklistTable.ValueMember = "TABLE_NAME";
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();
            foreach (DataRowView checkedItem in cklistTable.CheckedItems)
            {
                string tableName = checkedItem["TABLE_NAME"].ToString();
                string entityClass = GenerateEntityClass(tableName);
                result.AppendLine(entityClass);
            }
            txtMemoEdit.Text = result.ToString();
        }
        private string GenerateEntityClass(string tableName)
        {
            DataTable schema = GetTableSchema(tableName);
            StringBuilder classBuilder = new StringBuilder();

            classBuilder.AppendLine($"public class {tableName}");
            classBuilder.AppendLine("{");

            foreach (DataRow row in schema.Rows)
            {
                string columnName = row["COLUMN_NAME"].ToString();
                string dataType = row["DATA_TYPE"].ToString();
                bool isNullable = row["IS_NULLABLE"].ToString() == "YES";
                bool isPrimaryKey = Convert.ToBoolean(row["IsPrimaryKey"]);
                bool isIdentity = Convert.ToBoolean(row["IsIdentity"]);
                int maxLength = row["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value ? Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]) : -1;

                string csharpType = GetCSharpType(dataType, isNullable);

                if (isPrimaryKey)
                {
                    classBuilder.AppendLine("    [Key]");
                    if (isIdentity)
                    {
                        classBuilder.AppendLine("    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    }
                }

                if (!isNullable && !isPrimaryKey)
                {
                    classBuilder.AppendLine("    [Required]");
                }

                if (maxLength > 0)
                {
                    classBuilder.AppendLine($"    [MaxLength({maxLength})]");
                }

                classBuilder.AppendLine($"    public {csharpType} {columnName} {{ get; set; }}");
                classBuilder.AppendLine(); // Add a new line after each property definition


            }

            classBuilder.AppendLine("}");

            return classBuilder.ToString();
        }
        private DataTable GetTableSchema(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable schemaTable = new DataTable();

                string query = $@"USE  {lkDatabase.Text};
                SELECT 
                    c.TABLE_NAME,
                    c.COLUMN_NAME,
                    c.DATA_TYPE, 
                    c.IS_NULLABLE, 
                    c.CHARACTER_MAXIMUM_LENGTH,
                    CASE 
                        WHEN kcu.COLUMN_NAME IS NOT NULL THEN 1 
                        ELSE 0 
                    END AS IsPrimaryKey,
                    CASE 
                        WHEN ic.is_identity = 1 THEN 1 
                        ELSE 0 
                    END AS IsIdentity
                FROM 
                    INFORMATION_SCHEMA.COLUMNS c
                LEFT JOIN 
                    INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu 
                    ON c.TABLE_NAME = kcu.TABLE_NAME 
                    AND c.COLUMN_NAME = kcu.COLUMN_NAME 
                    AND kcu.CONSTRAINT_NAME LIKE 'PK_%'
                LEFT JOIN 
                    sys.columns ic 
                    ON c.TABLE_NAME = OBJECT_NAME(ic.object_id) 
                    AND c.COLUMN_NAME = ic.name
                LEFT JOIN 
                    sys.tables t 
                    ON c.TABLE_NAME = t.name
                WHERE 
                    c.TABLE_CATALOG =  '{lkDatabase.Text}' AND
                    c.TABLE_NAME =  '{tableName}'
                    AND c.TABLE_SCHEMA = 'dbo'
                ORDER BY 
                    c.TABLE_NAME, c.COLUMN_NAME;


                        ";


                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                adapter.Fill(schemaTable);
            }

            return schemaTable;
        }
    }

        private string GetCSharpType(string sqlType, bool isNullable)
        {
            string csharpType;

            switch (sqlType.ToLower())
            {
                case "int":
                    csharpType = "int";
                    break;
                case "bigint":
                    csharpType = "long";
                    break;
                case "smallint":
                    csharpType = "short";
                    break;
                case "bit":
                    csharpType = "bool";
                    break;
                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    csharpType = "decimal";
                    break;
                case "float":
                    csharpType = "double";
                    break;
                case "real":
                    csharpType = "float";
                    break;
                case "datetime":
                case "smalldatetime":
                case "date":
                case "time":
                case "datetime2":
                case "datetimeoffset":
                    csharpType = "DateTime";
                    break;
                case "char":
                case "varchar":
                case "text":
                case "nchar":
                case "nvarchar":
                case "ntext":
                    csharpType = "string";
                    break;
                case "binary":
                case "varbinary":
                case "image":
                    csharpType = "byte[]";
                    break;
                case "uniqueidentifier":
                    csharpType = "Guid";
                    break;
                default:
                    csharpType = "object";
                    break;
            }

            if (isNullable && csharpType != "string" && csharpType != "byte[]")
            {
                csharpType += "?";
            }

            return csharpType;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cklistTable.UnCheckAll();
            txtMemoEdit.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "C# Files (*.cs)|*.cs";
                saveFileDialog.DefaultExt = "cs";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, txtMemoEdit.Text);
                }
            }
        }
    }
}