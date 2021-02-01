using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository
{
    /// <summary>
    /// ISSCN6ISFDRepository - Lista faturas em débito
    /// </summary>
    public class ISSCN6ISFDRepository : ISRepository<SCN6ISFDReceive>, IISSCN6ISFDRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISFDRepository(ILog log)
         : base("FaturasDebitos:SCN6ISFD_WSD/FaturasDebitos_SCN6ISFD_WSD_Port", "SCN6ISFD", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS lista faturas em débito";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISFDReceive baseModelReceive)
        {
            foreach(SCN6ISFDReceiveFaturas fatura in baseModelReceive.faturasSicom)
            {
                if (fatura.numeroFatura != null && !"".Equals(fatura.numeroFatura) && !"0".Equals(fatura.numeroFatura))
                {
                    if ("".Equals(fatura.valorFatura))
                        fatura.valorFatura = "0,00";
                    baseModelReceive.faturas.Add(fatura);
                }
            }
        }
    }
}
