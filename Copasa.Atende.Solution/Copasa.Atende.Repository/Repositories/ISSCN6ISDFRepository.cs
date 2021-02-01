using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISDFRepository - Detalhe fatura
    /// </summary>
    public class ISSCN6ISDFRepository : ISRepository<SCN6ISDFReceive>, IISSCN6ISDFRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISDFRepository(ILog log)
         : base("DetalheFatura:SCN6ISDF_WSD/DetalheFatura_SCN6ISDF_WSD_Port", "SCN6ISDF", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS detalhe fatura";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISDFReceive baseModelReceive)
        {
            foreach (SCN6ISDFReceiveHidrometro hidrometro in baseModelReceive.hidrometrosSicom)
            {
                if (hidrometro.numero != null && !"".Equals(hidrometro.numero))
                {
                    baseModelReceive.hidrometros.Add(hidrometro);
                }
            }
            foreach (string caterogia in baseModelReceive.categoriasSicom)
            {
                if (caterogia != null && !"".Equals(caterogia))
                {
                    baseModelReceive.categorias.Add(caterogia);
                }
            }

            if ("".Equals(baseModelReceive.dataPagamento.Trim()))
            {
                baseModelReceive.status = "Em aberto";
            }
            else
            {
                baseModelReceive.status = "Paga";
            }
        }
    }
}
