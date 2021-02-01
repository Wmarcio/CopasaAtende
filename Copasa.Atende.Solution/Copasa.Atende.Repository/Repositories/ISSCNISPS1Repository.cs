using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository
{
    /// <summary>
    /// ISSCNISPS1Repository - Lista hidrômetros de uma matrícula
    /// </summary>
    public class ISSCNISPS1Repository : ISRepository<SCNISPS1Receive>, IISSCNISPS1Repository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCNISPS1Repository(ILog log)
         : base("ListaHidrometros:SCNISPS1_WSD/ListaHidrometros_SCNISPS1_WSD_Port", "SCNISPS1", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS lista hidrômetros de uma matrícula";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCNISPS1Receive baseModelReceive)
        {
            foreach (var hidrometro in baseModelReceive.hidrometrosSicom)
            {
                if (hidrometro.numeroMedidoABNT != null && !"".Equals(hidrometro.numeroMedidoABNT) && !"0".Equals(hidrometro.numeroMedidoABNT))
                {
                    baseModelReceive.hidrometros.Add(hidrometro);
                }
            }
        }
    }
}
