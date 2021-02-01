using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Util;
using System.Linq;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISSSRepository - Busca solicitações de serviços de uma matrícula
    /// </summary>
    public class ISSCN4ISSSRepository : ISRepository<SCN4ISSSReceive>, IISSCN4ISSSRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISSSRepository(ILog log)
         : base("SolicitacaoServicoMatricula:SCN4ISSS_WSD/SolicitacaoServicoMatricula_SCN4ISSS_WSD_Port", "SCN4ISSS", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS busca solicitações de serviços de uma matrícula";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISSSReceive baseModelReceive)
        {
            foreach (SCN4ISSSReceiveSolicitacaoServico ss in baseModelReceive.solicitacoesServicoSicom)
            {
                if (ss.numeroSolicitacaoServico != null && !"".Equals(ss.numeroSolicitacaoServico) && !"0".Equals(ss.numeroSolicitacaoServico))
                {
                    baseModelReceive.solicitacoesServico.Add(ss);
                }
            }
            if (baseModelReceive.solicitacoesServico.Count > 0)
                baseModelReceive.solicitacoesServico = baseModelReceive.solicitacoesServico.OrderByDescending(x => (x.dataGeracao+ x.horaGeracao.PadLeft(4, '0')).ToDateTime("yyyyMMddHHmm")).ToList<SCN4ISSSReceiveSolicitacaoServico>();
            foreach (SCN4ISSSReceiveSolicitacaoServico ss in baseModelReceive.solicitacoesServico)
            {
                ss.horaGeracao = ss.horaGeracao.PadLeft(4, '0').ToDateTime("HHmm").ToString("HH:mm");
                ss.dataGeracao = ss.dataGeracao.ToDateTime("yyyyMMdd").ToShortDateString() + " " + ss.horaGeracao;
                ss.horaPrevisaoAtendimento = ss.horaPrevisaoAtendimento.PadLeft(4, '0').ToDateTime("HHmm").ToString("HH:mm");
                ss.dataPrevisaoAtendimento = ss.dataPrevisaoAtendimento.ToDateTime("yyyyMMdd").ToShortDateString() + " " + ss.horaPrevisaoAtendimento;

                if (!"".Equals(ss.dataBaixa) && !"0".Equals(ss.dataBaixa))
                {
                    ss.horaBaixa = ss.horaBaixa.PadLeft(4, '0').ToDateTime("HHmm").ToString("HH:mm");
                    ss.dataBaixa = ss.dataBaixa.ToDateTime("yyyyMMdd").ToShortDateString() + " " + ss.horaBaixa;
                }
                else
                {
                    ss.horaBaixa = "";
                    ss.dataBaixa = "";
                }
            }
        }
    }
}
