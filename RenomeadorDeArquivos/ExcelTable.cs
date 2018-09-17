using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace RenomeadorDeArquivos
{
    internal class ExcelTable
    {
        private string patch;
        public DataTable DataTable { get; private set; }
        
        public void LoadFile (string patch)
        {
            if (!(Path.GetExtension(patch) == ".xls") && !(Path.GetExtension(patch) == ".xlsx"))
                throw new System.ArgumentException("O arquivo selecionado inválido");
            this.patch = patch;
            this.DataTable = GetExcelTable();
        }
        
        private DataTable GetExcelTable()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string Ext = Path.GetExtension(this.patch);
                string connectionString = "";
                if (Ext == ".xls")
                {
                    connectionString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + this.patch + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                else if (Ext == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + this.patch + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                OleDbCommand dbCommand = new OleDbCommand();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dbCommand.Connection = dbConnection;
                dbConnection.Open();
                DataTable dtSchema;
                dtSchema = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string tableName = dtSchema.Rows[0]["TABLE_NAME"].ToString();
                dbConnection.Close();
                dbConnection.Open();
                dbCommand.CommandText = "SELECT * From [" + tableName + "]";
                dataAdapter.SelectCommand = dbCommand;
                dataAdapter.Fill(dataTable);
                dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public string[] GetColumnsNames()
        {
            return DataTable.Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToArray();
        }

    }
}