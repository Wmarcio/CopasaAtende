using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISCSRepository - Busca informações de matrícula
    /// </summary>
    public class ISSCN4ISCSRepository : ISRepository<SCN4ISCSReceive>, IISSCN4ISCSRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISCSRepository(ILog log)
         : base("BuscaInformacoesMatricula:SCN4ISCS_WSD/BuscaInformacoesMatricula_SCN4ISCS_WSD_Port", "SCN4ISCS", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS busca informações de matrícula";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISCSReceive baseModelReceive)
        {
        }
    }
}
