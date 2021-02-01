using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN5ISHCRepository - Histórico de consumo
    /// </summary>
    public class ISSCN5ISHCRepository : ISRepository<SCN5ISHCReceive>, IISSCN5ISHCRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN5ISHCRepository(ILog log)
         : base("HistoricoConsumo:SCN5ISHC_WSD/HistoricoConsumo_SCN5ISHC_WSD_Port", "SCN5ISHC", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS histórico de consumo";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN5ISHCReceive baseModelReceive)
        {
            foreach (SCN5ISHCReceiveDados dado in baseModelReceive.dadosSicom)
            {
                if (dado.referencia != null && !"".Equals(dado.referencia) && dado.leitura != null && !"0".Equals(dado.leitura))
                {
                    baseModelReceive.ocorrencias.Add(dado);
                }
            }
        }
    }
}
