using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Repositorio Religação
    /// </summary>
    public class ISSCN4ISCPRepository : ISRepository<SCN4ISCPReceive>, IISSCN4ISCPRepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ISSCN4ISCPRepository(ILog log)
            :base("Religacao:SCN4ISCP_WSD/Religacao_SCN4ISCP_WSD_Port", "SCN4ISCP", log)
        {

        }

        /// <summary>
        /// Nome da entidade
        /// </summary>
        /// <returns></returns>
        public override string GetEntidadeNome()
        {
            return "Repository IS religação";
        }

        /// <summary>
        /// Trata retorno do Sicom
        /// </summary>
        /// <param name="baseModelReceive"></param>
        protected override void TratarRetorno(SCN4ISCPReceive baseModelReceive)
        {
            foreach(SCN4ISCPReceiveParcelamento parcelamento in baseModelReceive.parcelamentosSicom)
            {
                if (parcelamento.numeroParcelas != null && !"".Equals(parcelamento.numeroParcelas) && !"0".Equals(parcelamento.numeroParcelas))
                {
                    if ("".Equals(parcelamento.numeroParcelas))
                        parcelamento.numeroParcelas = "0";
                    if ("".Equals(parcelamento.taxaJuros))
                        parcelamento.taxaJuros = "0,0";
                    if ("".Equals(parcelamento.valorParcela))
                        parcelamento.valorParcela = "0,00";
                    baseModelReceive.parcelamento.Add(parcelamento);
                }
            }
        }
    }
}
