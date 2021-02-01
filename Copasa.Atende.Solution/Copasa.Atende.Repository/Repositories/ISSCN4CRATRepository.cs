using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4CRATRepository - Enviar serviço
    /// </summary>
    public class ISSCN4CRATRepository : ISRepository<SCN4CRATReceive>, IISSCN4CRATRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4CRATRepository(ILog log)
         : base("EnvioServicoSICOM:SCN4CRAT_WSD/EnvioServicoSICOM_SCN4CRAT_WSD_Port", "SCN4CRAT",log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS enviar serviço";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4CRATReceive baseModelReceive)
        {
        }
    }
}
