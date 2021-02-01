using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISTPRepository - Tarifa proporcional
    /// </summary>
    public class ISSCN6ISTPRepository : ISRepository<SCN6ISTPReceive>, IISSCN6ISTPRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISTPRepository(ILog log)
         : base("TarifaProporcional:SCN6ISTP_WSD/TarifaProporcional_SCN6ISTP_WSD_Port", "SCN6ISTP", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS tarifa proporcional";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISTPReceive baseModelReceive)
        {
            //Tratando gambiarra do Sicom
            if ("ERRO1".Equals(baseModelReceive.dataLeituraAnterior.ToUpper()))
            {
                baseModelReceive.descricaoRetorno = "M0196";
                baseModelReceive.dataLeituraAnterior = "";
            }
            else if ("ERRO2".Equals(baseModelReceive.dataLeituraAnterior.ToUpper()))
            {
                baseModelReceive.descricaoRetorno = "M0197";
                baseModelReceive.dataLeituraAnterior = "";
            }
            else if ("ERRO3".Equals(baseModelReceive.dataLeituraAnterior.ToUpper()))
            {
                baseModelReceive.descricaoRetorno = "M0198";
                baseModelReceive.dataLeituraAnterior = "";
            }
            else if ("ERRO4".Equals(baseModelReceive.dataLeituraAnterior.ToUpper()))
            {
                baseModelReceive.descricaoRetorno = "M0198";
                baseModelReceive.dataLeituraAnterior = "";
            }
        }
    }
}
