using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Util;
using System;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN5IS01Repository - Informar leitura - Entrada Matrícula
    /// </summary>
    public class ISSCN5IS01Repository : ISRepository<SCN5IS01Receive>, IISSCN5IS01Repository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN5IS01Repository()
         : base("InformarLeitura:SCN5IS01_WSD/InformarLeitura_SCN5IS01_WSD_Port", "SCN5IS01")
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS Informar leitura - Entrada Matrícula";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN5IS01Receive baseModelReceive)
        {
            if (!"".Equals(baseModelReceive.dtProgramacao) && !"0".Equals(baseModelReceive.dtProgramacao)
                && baseModelReceive.dtProgramacao.ToDateTime("yyyyMMdd") > DateTime.Today)
                baseModelReceive.dtProgramacao = DateTime.Today.ToString("yyyyMMdd");
            /*
            if (!"".Equals(baseModelReceive.dtProgramacao) && !"0".Equals(baseModelReceive.dtProgramacao))
                baseModelReceive.dtProgramacao = baseModelReceive.dtProgramacao.ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy");
            else
                baseModelReceive.dtProgramacao = "";
            if (!"".Equals(baseModelReceive.dtProgramacaoLeitura) && !"0".Equals(baseModelReceive.dtProgramacaoLeitura))
                baseModelReceive.dtProgramacaoLeitura = baseModelReceive.dtProgramacaoLeitura.ToDateTime("yyyyMMdd").ToString("dd/MM/yyyy");
            else
                baseModelReceive.dtProgramacaoLeitura = "";
            if ("0".Equals(baseModelReceive.refer))
                baseModelReceive.refer = "";
            if ("0".Equals(baseModelReceive.fatura))
                baseModelReceive.fatura = "";
            if ("0".Equals(baseModelReceive.ind))
                baseModelReceive.ind = "";
            */
            foreach (var hidrometro in baseModelReceive.tabelas)
            {
                if (hidrometro.numMedidorABNT != null && !"".Equals(hidrometro.numMedidorABNT) && !"0".Equals(hidrometro.numMedidorABNT))
                {
                    baseModelReceive.medidores.Add(hidrometro);
                }
            }
        }
    }
}
