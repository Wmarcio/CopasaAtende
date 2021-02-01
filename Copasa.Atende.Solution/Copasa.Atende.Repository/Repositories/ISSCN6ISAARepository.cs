using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISAARepository - Lista agnências
    /// </summary>
    public class ISSCN6ISAARepository : ISRepository<SCN6ISAAReceive>, IISSCN6ISAARepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ISSCN6ISAARepository(ILog log)
         : base("ListaAgenciasAtendimento:SCN6ISAA_WSD/ListaAgenciasAtendimento_SCN6ISAA_WSD_Port", "SCN6ISAA", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISAAReceive baseModelReceive)
        {
            foreach (SCN6ISAAReceiveAgenciaSicom agencia in baseModelReceive.agenciasAtendimentoSicom)
            {
                if (!String.IsNullOrEmpty(agencia.nomeAgencia))
                {
                    var sCN6ISAAReceiveAgencia = new SCN6ISAAReceiveAgencia();
                    sCN6ISAAReceiveAgencia.nomeAgencia = agencia.nomeAgencia;
                    sCN6ISAAReceiveAgencia.unidadeAtendimento = agencia.unidadeAtendimento;
                    sCN6ISAAReceiveAgencia.enderecoAgencia = agencia.enderecoAgencia1 + " " + agencia.enderecoAgencia2;
                    sCN6ISAAReceiveAgencia.horarioAtendimento = agencia.horarioAtendimento;
                    sCN6ISAAReceiveAgencia.telefoneAgencia = agencia.telefoneAgencia;

                    baseModelReceive.agenciasAtendimento.Add(sCN6ISAAReceiveAgencia);
                }
            }
        }
    }
}
