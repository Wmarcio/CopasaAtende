using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN3ISPSRepository - Lista Pontos serviço
    /// </summary>
    public class ISSCN3ISPSRepository : ISRepository<SCN3ISPSReceive>, IISSCN3ISPSRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN3ISPSRepository(ILog log)
         : base("PontosServico:SCN3ISPS_WSD/PontosServico_SCN3ISPS_WSD_Port", "SCN3ISPS",log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS Lista Pontos serviço";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN3ISPSReceive baseModelReceive)
        {
            foreach (SCN3ISPSReceivePSAgua psAgua in baseModelReceive.pontosServicoAguaSicom)
            {
                if (psAgua.tipo != null && !"".Equals(psAgua.tipo) && !"0".Equals(psAgua.tipo))
                {
                    baseModelReceive.pontosServicoAgua.Add(psAgua);
                }
            }
            foreach (SCN3ISPSReceivePSEsgoto psEsgoto in baseModelReceive.pontosServicoEsgotoSicom)
            {
                if (psEsgoto.tipo != null && !"".Equals(psEsgoto.tipo) && !"0".Equals(psEsgoto.tipo))
                {
                    baseModelReceive.pontosServicoEsgoto.Add(psEsgoto);
                }
            }
            foreach (SCN3ISPSReceiveFonteAlternativa psFonte in baseModelReceive.pontosServicoFonteAlternativaSicom)
            {
                if (psFonte.tipo != null && !"".Equals(psFonte.tipo) && !"0".Equals(psFonte.tipo))
                {
                    baseModelReceive.pontosServicoFonteAlternativa.Add(psFonte);
                }
            }
            foreach (SCN3ISPSReceiveDeducaoEsgoto psDeducao in baseModelReceive.pontosServicoDeducaoEsgotoSicom)
            {
                if (psDeducao.tipo != null && !"".Equals(psDeducao.tipo) && !"0".Equals(psDeducao.tipo))
                {
                    baseModelReceive.pontosServicoDeducaoEsgoto.Add(psDeducao);
                }
            }
            foreach (SCN3ISPSReceiveMedicaoEsgoto psMedicaoEsgoto in baseModelReceive.pontosServicoMedicaoEsgotoSicom)
            {
                if (psMedicaoEsgoto.tipo != null && !"".Equals(psMedicaoEsgoto.tipo) && !"0".Equals(psMedicaoEsgoto.tipo))
                {
                    baseModelReceive.pontosServicoMedicaoEsgoto.Add(psMedicaoEsgoto);
                }
            }
        }
    }
}
