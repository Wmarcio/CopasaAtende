using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISQARepository - Quitação anual de débito
    /// </summary>
    public class ISSCN6ISQARepository : ISRepository<SCN6ISQAReceive>, IISSCN6ISQARepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISQARepository(ILog log)
         : base("QuitacaoAnualDebito:SCN6ISQA_WSD/QuitacaoAnualDebito_SCN6ISQA_WSD_Port", "SCN6ISQA", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS quitação anual de débito";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISQAReceive baseModelReceive)
        {
            foreach (SCN6ISQAReceiveFaturaEmDebito faturas in baseModelReceive.faturasEmDebito)
            {
                if (!"".Equals(faturas.dataVencimento))
                faturas.dataVencimento = faturas.dataVencimento.ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy");
                if (!"".Equals(faturas.valor))
                    faturas.valor = (double.Parse(faturas.valor)/100).ToString("#,###,###0.00");
            }
        }
    }
}
