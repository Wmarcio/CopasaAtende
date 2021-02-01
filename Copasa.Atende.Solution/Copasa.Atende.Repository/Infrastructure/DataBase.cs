using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Web;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Summary description for Database
    /// Allow a common database access to all application.
    /// Deals with the getting the string connection from the web.config
    /// and publish basic operation over the database with its public methods.
    /// 
    /// Should be instantiated when used.
    /// This abstracts the database access to all application.
    /// </summary>
    public abstract class Database : IDatabase
    {
        /// <summary>
        /// GetCommand
        /// </summary>
        abstract public DbCommand GetCommand();

        /// <summary>
        /// GetConnection
        /// </summary>
        abstract public DbConnection GetConnection();

        /// <summary>
        /// Public nested class EmptyStringException. Happens when some database operation is done when the connection string is empty.
        /// </summary>
        public class EmptyStringException : Exception
        { }

        /// <summary>
        /// ConnectionString
        /// </summary>
        public string ConnectionString { get { return connectionString; } }

        /// <summary>
        /// connectionString
        /// </summary>
        protected String connectionString;
        /// <summary>
        /// Create a new instance of database object that abstract the database access to all application.
        /// </summary>
        public Database()
        {
            // Buscar a conexão de string padrão
            if (ConfigurationManager.ConnectionStrings["CopasaAtendeDataContext"] != null)
                connectionString = ConfigurationManager.ConnectionStrings["CopasaAtendeDataContext"].ConnectionString;
            else
                connectionString = ""; // Connection string is empty, no database connection will happen;

            manterConexaoAberta = false;

            //initialize();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conexao"></param>
        public Database(string conexao)
        {
            // Buscar a conexão de string padrão
            if (ConfigurationManager.ConnectionStrings[conexao] != null)
                connectionString = ConfigurationManager.ConnectionStrings[conexao].ConnectionString;
            else
                connectionString = ""; // Connection string is empty, no database connection will happen;

            manterConexaoAberta = false;

            //initialize();
        }
        /// <summary>
        /// Construtor que reaproveita o componente existente de uma conexão um database.
        /// </summary>
        /// <param name="database"></param>
        public Database(Database database)
        {
            manterConexaoAberta = true;

            connectionString = database.connectionString;
            databaseCommand = database.databaseCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        ~Database()
        {
            FecharConnexao();
        }
        /// <summary>
        /// 
        /// </summary>
        public void FecharConnexao()
        {
            if (databaseCommand != null)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                    dbTransaction = null;
                }

                string s = databaseCommand.CommandText;
                databaseCommand.Connection.Close();
                //Inserido pelo cristiano
                databaseCommand.Dispose();
                databaseCommand = null;

                #if DEBUG
                System.Diagnostics.Debug.WriteLine("Fechada para : " + s);
                #endif
            }
        }
        /// <summary>
        /// Execute a sql command in database. Returns the number of rows affected.
        /// This should be used for sql that does not return a result set like UPDATE and INSERT statements.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        virtual public int executeSql(String sql)
        {
            int result;

            if (IsConexaoIndisponivel())
                initialize();

            databaseCommand.CommandText = sql;
            result = databaseCommand.ExecuteNonQuery();

            return result;
        }

        /// <summary>
        /// Execute a Select sql statement and return a DataTable containing the
        /// data retrieved from the database.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        virtual public DataTable fetch(String sql)
        {
            return fetch(sql, null);
        }

        /// <summary>
        /// fetch
        /// </summary>
        virtual public DataTable fetch(String sql, DbParameter[] parameters)
        {
            DbDataReader sqlReader;
            DataTable table, schemaTable;
            DataTableReader schemaReader;
            DataColumn column;
            DataRow row;

            String columnName;
            Type columnDataType;

            if (IsConexaoIndisponivel())
                initialize();

            databaseCommand.CommandText = sql;
            databaseCommand.CommandType = CommandType.Text;

            databaseCommand.Parameters.Clear();
            if (parameters != null)
                foreach (DbParameter p in parameters)
                    databaseCommand.Parameters.Add(p);

            sqlReader = databaseCommand.ExecuteReader();

            #region Creating the table with the schema table

            schemaTable = sqlReader.GetSchemaTable();
            schemaReader = schemaTable.CreateDataReader();

            table = new DataTable();

            while (schemaReader.Read())
            {
                columnName = schemaReader.GetString(schemaReader.GetOrdinal("ColumnName"));
                columnDataType = (Type)schemaReader.GetValue(schemaReader.GetOrdinal("DataType"));
                column = new DataColumn(columnName, columnDataType);
                table.Columns.Add(column);
            }   //while

            #endregion

            while (sqlReader.Read())
            {
                row = table.NewRow();
                for (int i = 0; i < sqlReader.FieldCount; i++)
                {
                    if (!sqlReader.IsDBNull(i))
                        row[i] = sqlReader.GetValue(i);
                }
                table.Rows.Add(row);
            }   // while

            if (!manterConexaoAberta)
                FecharConnexao();

            return table;
        }

        /// <summary>
        /// IniciarTransacao
        /// </summary>
        public void IniciarTransacao()
        {
            // Somente consegue iniciar uma transação
            if (dbTransaction == null)
            {
                if (IsConexaoIndisponivel())
                    initialize();

                dbTransaction = databaseCommand.Connection.BeginTransaction();
                // automaticamente considera que a conexao deve prevalecer aberta:
                manterConexaoAberta = true;
            }
        }
        /// <summary>
        /// Realizar commit Commit da transação.
        /// </summary>
        public void EncerrarTransacao()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Commit();
                dbTransaction = null;
            }
        }

        /// <summary>
        /// CancelarTransacao
        /// </summary>
        public void CancelarTransacao()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Rollback();
                dbTransaction = null;
            }
        }

        /// <summary>
        /// executeStoredProcedure
        /// </summary>
        virtual public int executeStoredProcedure(String sql, DbParameter[] parameters)
        {
            int result;

            if (IsConexaoIndisponivel())
                initialize();

            databaseCommand.CommandText = sql;
            databaseCommand.CommandType = CommandType.StoredProcedure;

            databaseCommand.Parameters.Clear();
            if (parameters != null)
                foreach (DbParameter p in parameters)
                    databaseCommand.Parameters.Add(p);

            result = databaseCommand.ExecuteNonQuery();

            if (!manterConexaoAberta)
                FecharConnexao();

            return result;
        }

        DbCommand databaseCommand = null;
        DbTransaction dbTransaction = null;

        /// <summary>
        /// Verifica se comando e conexao com banco não está disponivel para uso, retorta true se necessitar de inicialização do comando.
        /// </summary>
        /// <returns></returns>
        protected bool IsConexaoIndisponivel()
        {
            return (databaseCommand == null || databaseCommand.Connection == null || databaseCommand.Connection.State == ConnectionState.Closed || databaseCommand.Connection.State == ConnectionState.Broken);
        }

        private void initialize()
        {
            if (String.IsNullOrEmpty(connectionString))
                throw new EmptyStringException();

            databaseCommand = GetCommand();
            databaseCommand.Connection = GetConnection();
            databaseCommand.Connection.Open();

            #if DEBUG
            System.Diagnostics.Debug.WriteLine("Aberta em: " + HttpContext.Current.Request.Url);
            #endif

        }

        private bool manterConexaoAberta;

        /// <summary>
        /// ManterConexaoAberta
        /// </summary>
        public bool ManterConexaoAberta
        {
            get { return manterConexaoAberta; }
            set { manterConexaoAberta = value; }
        }
    }
}
