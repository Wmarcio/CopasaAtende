using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4CRALRepository - Gera eventos prioridade
    /// </summary>
    public class ISSCN4CRALRepository : ISRepository<SCN4CRALReceive>, IISSCN4CRALRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4CRALRepository(ILog log)
         : base("GeraEventosPrioridade:SCN4CRAL_WSD/GeraEventosPrioridade_SCN4CRAL_WSD_Port", "SCN4CRAL",log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS gera eventos prioridade";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4CRALReceive baseModelReceive)
        {
            if (!"".Equals(baseModelReceive.dataGeracaoSS) && !"0".Equals(baseModelReceive.dataGeracaoSS))
            {
                baseModelReceive.dataGeracaoSS = baseModelReceive.dataGeracaoSS.ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy");
            }
            if (!"".Equals(baseModelReceive.horaGeracaoSS) && !"0".Equals(baseModelReceive.horaGeracaoSS))
            {
                baseModelReceive.horaGeracaoSS = long.Parse(baseModelReceive.horaGeracaoSS).ToString("00:00");
            }
        }
    }
}
