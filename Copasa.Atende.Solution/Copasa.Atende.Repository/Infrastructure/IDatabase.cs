using System.Data.Common;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// IDatabase
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// GetCommand
        /// </summary>
        DbCommand GetCommand();

        /// <summary>
        /// GetConnection
        /// </summary>
        DbConnection GetConnection();
    }
}
