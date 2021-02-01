using Oracle.DataAccess.Client;
using System.Data.Common;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// OraDatabase
    /// </summary>
    public class OraDatabase : Database
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public OraDatabase() { }

        /// <summary>
        /// Construtor
        /// </summary>
        public OraDatabase(string conexao)
            : base(conexao)
        {
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public OraDatabase(Database database)
            : base(database)
        {
        }
        /// <summary>
        /// GetCommand
        /// </summary>
        public override DbCommand GetCommand()
        {
            return new OracleCommand();
        }

        /// <summary>
        /// GetConnection
        /// </summary>
        public override DbConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }
    }
}
