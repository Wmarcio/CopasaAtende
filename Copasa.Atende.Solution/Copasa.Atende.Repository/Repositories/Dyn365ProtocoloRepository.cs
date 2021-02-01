using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365AtualizaSSRepository - Inclui e atualiza dados na tabela de protocolo no Dynamics 365
    /// </summary>
    public class Dyn365ProtocoloRepository : DynamicsRepository<Dyn365Protocolo>, IDyn365ProtocoloRepository
    {
        private Dictionary<string, int> TabelaTipoLogradouro = new Dictionary<string, int>();


        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365ProtocoloRepository()
         : base("incidents", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
            CarregaTabelaTipoLogradouro();
        }

        private void CarregaTabelaTipoLogradouro()
        {
            TabelaTipoLogradouro.Add("AL", 176410000);
            TabelaTipoLogradouro.Add("AT", 176410001);
            TabelaTipoLogradouro.Add("AV", 176410002);
            TabelaTipoLogradouro.Add("BC", 176410003);
            TabelaTipoLogradouro.Add("C", 176410004);
            TabelaTipoLogradouro.Add("EC", 176410005);
            TabelaTipoLogradouro.Add("EL", 176410006);
            TabelaTipoLogradouro.Add("EP", 176410007);
            TabelaTipoLogradouro.Add("ET", 176410008);
            TabelaTipoLogradouro.Add("F", 176410009);
            TabelaTipoLogradouro.Add("G", 176410010);
            TabelaTipoLogradouro.Add("IL", 176410011);
            TabelaTipoLogradouro.Add("LD", 176410012);
            TabelaTipoLogradouro.Add("LG", 176410013);
            TabelaTipoLogradouro.Add("MR", 176410014);
            TabelaTipoLogradouro.Add("PQ", 176410015);
            TabelaTipoLogradouro.Add("PR", 176410016);
            TabelaTipoLogradouro.Add("OS", 176410017);
            TabelaTipoLogradouro.Add("R", 176410018);
            TabelaTipoLogradouro.Add("RD", 176410019);
            TabelaTipoLogradouro.Add("SI", 176410020);
            TabelaTipoLogradouro.Add("T", 176410021);
            TabelaTipoLogradouro.Add("V", 176410022);
            TabelaTipoLogradouro.Add("VD", 176410023);
            TabelaTipoLogradouro.Add("VE", 176410024);
            TabelaTipoLogradouro.Add("VI", 176410025);
            TabelaTipoLogradouro.Add("VP", 176410026);
            TabelaTipoLogradouro.Add("TR", 176410028);
            TabelaTipoLogradouro.Add("SV", 176410029);
            TabelaTipoLogradouro.Add("DT", 176410030);
            TabelaTipoLogradouro.Add("CD", 176410031);
            TabelaTipoLogradouro.Add("ES", 176410032);
            TabelaTipoLogradouro.Add("BR", 176410033);
            TabelaTipoLogradouro.Add("PA", 176410034);
            TabelaTipoLogradouro.Add("", 176410027);
        }

        /// <summary>
        /// Retorna o código do tipoLogradouro
        /// </summary>
        public int getCodigoTipoLogradouro(string tipoLogradouro)
        {
            return TabelaTipoLogradouro[tipoLogradouro];
        }

        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365Protocolo baseModel)
        {
            if (baseModel.dataPrevisaoSS != null && !"".Equals(baseModel.dataPrevisaoSS) && !"0".Equals(baseModel.dataPrevisaoSS))
            {
                string dataHoraPrevisaoSS = baseModel.dataPrevisaoSS + baseModel.horaPrevisaoSS.PadLeft(4, '0');
                baseModel.dataPrevisaoSSDyn365 = dataHoraPrevisaoSS.ToDateTime("yyyyMMddHHmm");
            }

            if (baseModel.dataGeracaoSS != null && !"".Equals(baseModel.dataGeracaoSS) && !"0".Equals(baseModel.dataGeracaoSS))
            {
                string dataHoraGeracaoSS = baseModel.dataGeracaoSS + baseModel.horaGeracaoSS.PadLeft(4, '0');
                baseModel.dataGeracaoSSDyn365 = dataHoraGeracaoSS.ToDateTime("yyyyMMddHHmm").AddHours(3);
            }

            if (baseModel.dataBaixaSS != null && !"".Equals(baseModel.dataBaixaSS) && !"0".Equals(baseModel.dataBaixaSS))
            {
                string dataHoraBaixaSS = baseModel.dataBaixaSS + baseModel.horaBaixaSS.PadLeft(4, '0');
                baseModel.dataBaixaSSDyn365 = dataHoraBaixaSS.ToDateTime("yyyyMMddHHmm");
                baseModel.dataTerminoAtendimentoDyn365 = baseModel.dataBaixaSSDyn365;
            }
        }


    }
}
