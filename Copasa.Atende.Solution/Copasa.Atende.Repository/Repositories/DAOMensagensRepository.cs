using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.Common;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// DAOMensagensRepository - Mensagens de retorno
    /// </summary>
    public class DAOMensagensRepository : BaseDao, IDAOMensagensRepository
    {
        /// <summary>
        /// Retorno descrição da mensagem
        /// </summary>
        public string getDescricaoMensagem(string idMensagem)
        {
            /*
            DataTable table = OraDatabase.fetch(sqlBuscaDescricaoMensagem, new DbParameter[] {
                        new OracleParameter("idMensagem",idMensagem)
                });
            */
            DataTable table = OraDatabase.fetch(string.Format(sqlBuscaDescricaoMensagem, idMensagem));

            DbDataReader reader = table.CreateDataReader();
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            else
            {
                return "";
            }

        }

        const string sqlBuscaDescricaoMensagem = "select ds_mensagem from comercial_crm_mensagem where id_mensagem = '{0}'";

    }
}
