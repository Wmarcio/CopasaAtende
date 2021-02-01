using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISPDRepository - Consulta parcelamento de débito
    /// </summary>
    public class ISSCN6ISPDRepository : ISRepository<SCN6ISPDReceive>, IISSCN6ISPDRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISPDRepository(ILog log)
         : base("ParcelamentoDebito:SCN6ISPD_WSD/ParcelamentoDebito_SCN6ISPD_WSD_Port", "SCN6ISPD", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS consulta parcelamento de débito";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISPDReceive baseModelReceive)
        {
            foreach(SCN6ISPDReceiveParcelas detalheParcela in baseModelReceive.detalhesParcelasSicom)
            {
                if (!"".Equals(detalheParcela.valorTotal) && !"0.00".Equals(detalheParcela.valorTotal))
                {
                    baseModelReceive.detalhesParcelas.Add(detalheParcela);
                }
            }

            foreach (SCN6ISPDReceiveFatura fatura in baseModelReceive.faturasSicom)
            {
                if (!"".Equals(fatura.numero) && !"0".Equals(fatura.numero))
                {
                    baseModelReceive.faturas.Add(fatura);
                }
            }
        }
    }
}
