using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISU1_IdentificadoresRepository - Busca dados do cliente sem array de matrículas
    /// </summary>
    public class ISSCN4ISU1_IdentificadoresRepository : ISRepository<SCN4ISU1_IdentificadoresReceive>, IISSCN4ISU1_IdentificadoresRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISU1_IdentificadoresRepository(ILog log)
         : base("IdentUsuario:SCN4ISU1_WSD/IdentUsuario_SCN4ISU1_WSD_Port", "SCN4ISU1", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository Busca dados do cliente";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISU1_IdentificadoresReceive baseModelReceive)
        {
            foreach (SCN4ISU1ReceiveUsuarios identificador in baseModelReceive.identificadoresSicom)
            {
                if (!"".Equals(identificador.identificador) && !"0".Equals(identificador.identificador))
                {
                    if (!"".Equals(identificador.telefoneCelular) && !"0".Equals(identificador.telefoneCelular))
                    {
                        string telCelular = identificador.telefoneCelular;
                        int tamCelular = telCelular.Length;
                        identificador.telefoneCelular = "(" + identificador.DDDCelular + ") " + telCelular.Substring(0, tamCelular - 4) + "-" + telCelular.Substring(tamCelular - 4);
                    }
                    else
                        identificador.telefoneCelular = "";

                    if (!"".Equals(identificador.telefoneComercial) && !"0".Equals(identificador.telefoneComercial))
                    {
                        string telComercial = identificador.telefoneComercial;
                        identificador.telefoneComercial = "(" + identificador.DDDComercial + ") " + telComercial.Substring(0, 4) + "-" + telComercial.Substring(4);
                    }
                    else
                        identificador.telefoneComercial = "";

                    if (!"".Equals(identificador.telefoneResidencia) && !"0".Equals(identificador.telefoneResidencia))
                    {
                        string telResidencia = identificador.telefoneResidencia;
                        identificador.telefoneResidencia = "(" + identificador.DDDResidencia + ") " + telResidencia.Substring(0, 4) + "-" + telResidencia.Substring(4);
                    }
                    else
                        identificador.telefoneResidencia = "";

                    if (identificador.cpfCnpj.Length > 0)
                    {
                        if (identificador.cpfCnpj.Length > 11)
                        {
                            identificador.cpfCnpj = Convert.ToUInt64(identificador.cpfCnpj).ToString(@"00\.000\.000\/0000-00");
                        }
                        else
                        {
                            identificador.cpfCnpj = Convert.ToUInt64(identificador.cpfCnpj).ToString(@"000\.000\.000\-00");
                        }
                    }
                    foreach (SCN4ISU1ReceiveMatriculasUsuarios matricula in identificador.matriculas)
                    {
                        if (!"".Equals(matricula.nomeLogradouro) && !"".Equals(matricula.Localidade))
                        {
                            identificador.tipoLogradouroUsuario = matricula.tipoLogradouro;
                            identificador.nomeLogradouro = matricula.nomeLogradouro;
                            identificador.numeroLogradouroUsuario = matricula.numeroLogradouro;
                            identificador.tipoComplementoLogradouroUsuario = matricula.tipoComplementoLogradouro;
                            if (!"".Equals(matricula.complementoLogradouro))
                            identificador.complementoLogradouroUsuario = matricula.tipoComplementoLogradouro + " " +matricula.complementoLogradouro;
                            identificador.numeroLogradouroUsuario = matricula.numeroLogradouro;
                            identificador.bairro = matricula.bairro;
                            identificador.localidade = matricula.Localidade;
                            break;
                        }
                    }
                    baseModelReceive.identificadores.Add(identificador);
                }
            }
        }
    }
}
