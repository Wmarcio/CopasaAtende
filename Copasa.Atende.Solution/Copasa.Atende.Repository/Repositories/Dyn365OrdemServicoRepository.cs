using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Util;
using System.Configuration;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365CriaOSRepository - Inclui e atualiza dados na tabela de ordem de serviço no Dynamics 365
    /// </summary>
    public class Dyn365OrdemServicoRepository : DynamicsRepository<Dyn365OrdemServico> , IDyn365OrdemServicoRepository
    {

        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365OrdemServicoRepository()
         : base("copasa_ordemdeservicos", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
        }

        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365OrdemServico baseModel)
        {
            if (baseModel.dataPrevisaoOS != null && !"".Equals(baseModel.dataPrevisaoOS) && !"0".Equals(baseModel.dataPrevisaoOS))
            {
                string dataHoraPrevisaoSS = baseModel.dataPrevisaoOS + baseModel.horaPrevisaoOS.PadLeft(4, '0');
                baseModel.dataPrevisaoOSDyn365 = (dataHoraPrevisaoSS.ToDateTime("yyyyMMddHHmm")).ToString("MM/dd/yyyy HH:mm:ss");
            }

            if (baseModel.dataBaixaOS != null && !"".Equals(baseModel.dataBaixaOS) && !"0".Equals(baseModel.dataBaixaOS))
            {
                string dataHoraBaixaOS = baseModel.dataBaixaOS + baseModel.horaBaixaOS.PadLeft(4, '0');
                baseModel.dataBaixaOSDyn365 = (dataHoraBaixaOS.ToDateTime("yyyyMMddHHmm").AddHours(3)).ToString("MM/dd/yyyy HH:mm:ss");
            }
        }
    }
}
