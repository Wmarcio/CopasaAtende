using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISORRepository - Busca OS's de uma solicitação de serviço
    /// </summary>
    public class ISSCN4ISORRepository : ISRepository<SCN4ISORReceive>, IISSCN4ISORRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISORRepository(ILog log)
         : base("BuscaOrdensDeServicos:SCN4ISOR_WSD/BuscaOrdensDeServicos_SCN4ISOR_WSD_Port", "SCN4ISOR", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS Busca OS's de uma solicitação de serviço";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISORReceive baseModelReceive)
        {
            if (!"".Equals(baseModelReceive.horaBaixaSS) && !"0".Equals(baseModelReceive.horaBaixaSS))
            {
                baseModelReceive.horaBaixaSS = long.Parse(baseModelReceive.horaBaixaSS).ToString("00:00");
            }
            if (!"".Equals(baseModelReceive.horaGeracaoSS) && !"0".Equals(baseModelReceive.horaGeracaoSS))
            {
                baseModelReceive.horaGeracaoSS = long.Parse(baseModelReceive.horaGeracaoSS).ToString("00:00");
            }
            if (!"".Equals(baseModelReceive.horaPrevisaoSS) && !"0".Equals(baseModelReceive.horaPrevisaoSS))
            {
                baseModelReceive.horaPrevisaoSS = long.Parse(baseModelReceive.horaPrevisaoSS).ToString("00:00");
            }
            foreach (SCN4ISORReceiveOrdemServico os in baseModelReceive.ordensServicoSicom)
            {
                if (os.codigoServico != null && !"".Equals(os.codigoServico) && !"0".Equals(os.codigoServico))
                {
                    os.numeroOrdemServico = os.numeroOrdemServico.Replace(baseModelReceive.numeroSolicitacaoServico, "");

                    os.horaGeracao = os.horaGeracao.PadLeft(4, '0').ToDateTime("HHmm").ToString("HH:mm");
                    os.dataGeracao = os.dataGeracao.ToDateTime("yyyyMMdd").ToShortDateString() + " " + os.horaGeracao;
                    os.horaPrevisaoAtendimento = os.horaPrevisaoAtendimento.PadLeft(4, '0').ToDateTime("HHmm").ToString("HH:mm");
                    os.dataPrevisaoAtendimento = os.dataPrevisaoAtendimento.ToDateTime("yyyyMMdd").ToShortDateString() + " " + os.horaPrevisaoAtendimento;

                    if (!"".Equals(os.dataExecucao) && !"0".Equals(os.dataExecucao))
                    {
                        os.horaExecucao = os.horaExecucao.PadLeft(4, '0').ToDateTime("HHmm").ToString("HH:mm");
                        os.dataExecucao = os.dataExecucao.ToDateTime("yyyyMMdd").ToShortDateString() + " " + os.horaExecucao;
                    }else
                    {
                        os.horaExecucao = "";
                        os.dataExecucao = "";
                    }

                    if (!"".Equals(os.dataBaixa) && !"0".Equals(os.dataBaixa))
                    {
                        os.horaBaixa = os.horaBaixa.PadLeft(4, '0').ToDateTime("HHmm").ToString("HH:mm");
                        os.dataBaixa = os.dataBaixa.ToDateTime("yyyyMMdd").ToShortDateString() + " " + os.horaBaixa;
                    }
                    else
                    {
                        os.horaBaixa = "";
                        os.dataBaixa = "";
                    }
                    baseModelReceive.ordensServico.Add(os);
                }
            }
        }
    }
}
