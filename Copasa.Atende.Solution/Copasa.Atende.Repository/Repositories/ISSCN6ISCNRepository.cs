using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISCNRepository - Certidão negativa de débito
    /// </summary>
    public class ISSCN6ISCNRepository : ISRepository<SCN6ISCNReceive>, IISSCN6ISCNRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISCNRepository(ILog log)
         : base("CertidaoNegativaDebitos:SCN6ISCN_WSD/CertidaoNegativaDebitos_SCN6ISCN_WSD_Port", "SCN6ISCN", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS certidão negativa de débito";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISCNReceive baseModelReceive)
        {
            foreach (SCN6ISCNReceiveMatriculas matricula in baseModelReceive.matriculasSicom)
            {
                if (matricula.matricula != null && !"".Equals(matricula.matricula) && !"0".Equals(matricula.matricula))
                {
                    baseModelReceive.matriculas.Add(matricula);
                }
            }

            foreach (SCN6ISCNReceiveLancamento lancamento in baseModelReceive.lancamentosSicom)
            {
                if (lancamento.numeroFatura != null && !"".Equals(lancamento.numeroFatura) && !"0".Equals(lancamento.numeroFatura))
                {
                    baseModelReceive.lancamentos.Add(lancamento);
                }
            }

            foreach (SCN6ISCNReceiveParcelamento parcelamento in baseModelReceive.parcelamentoSicom)
            {
                if (parcelamento.parcelasParcial != null && !"".Equals(parcelamento.parcelasParcial) && !"0".Equals(parcelamento.parcelasParcial))
                {
                    baseModelReceive.parcelamento.Add(parcelamento);
                }
            }

            foreach(SCN6ISCNReceiveFinanciamento financiamento in baseModelReceive.financiamentoSicom)
            {
                if(financiamento.parcelasFinanciamento != null && !"".Equals(financiamento.parcelasFinanciamento) && !"0".Equals(financiamento.parcelasFinanciamento))
                {
                    baseModelReceive.financiamento.Add(financiamento);
                }
            }
        }
    }
}
