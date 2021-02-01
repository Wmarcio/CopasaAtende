using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN3ISMTRepository - Busca matrícula pelo endereço
    /// </summary>
    public class ISSCN3ISMTRepository : ISRepository<SCN3ISMTReceive>, IISSCN3ISMTRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN3ISMTRepository(ILog log)
         : base("BuscaMatriculaEndereco:SCN3ISMT_WSD/BuscaMatriculaEndereco_SCN3ISMT_WSD_Port", "SCN3ISMT", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS busca matrícula pelo endereço";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN3ISMTReceive baseModelReceive)
        {
            foreach (SCN3ISMTReceiveMatricula matricula in baseModelReceive.matriculasSicom)
            {
                if (matricula.matricula != null && !"".Equals(matricula.matricula) && !"0".Equals(matricula.matricula))
                {
                    matricula.codigoBairro = matricula.codigoLocalidade + Util.Util.formataNumero(matricula.codigoBairro, 6);
                    matricula.codigoLogradouro = matricula.codigoBairro + Util.Util.formataNumero(matricula.codigoLogradouro, 6);
                    baseModelReceive.matriculas.Add(matricula);
                }
            }
        }
    }
}
