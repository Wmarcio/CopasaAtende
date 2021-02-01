using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Repositorio calendário faturamento
    /// </summary>
    public class ISSCN6ISDLRepository : ISRepository<SCN6ISDLReceive>, IISSCN6ISDLRepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ISSCN6ISDLRepository(ILog log)
            : base("CalendarioFaturamento:SCN6ISDL_WSD/CalendarioFaturamento_WSD_Port", "SCN6ISDL", log)
        {

        }

        /// <summary>
        /// Nome da entidade
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS calendário faturamento";
        }

        /// <summary>
        /// Trata retorno do Sicom
        /// </summary>
        /// <param name="baseModelReceive"></param>
        protected override void TratarRetorno(SCN6ISDLReceive baseModelReceive)
        {
            foreach(SCN6ISDLReceiveDado dado in baseModelReceive.dadosSicom)
            {
                if (!"".Equals(dado.mesReferencia) && !"0".Equals(dado.mesReferencia))
                {
                    baseModelReceive.dados.Add(dado);
                }
            }
        }
    }
}
