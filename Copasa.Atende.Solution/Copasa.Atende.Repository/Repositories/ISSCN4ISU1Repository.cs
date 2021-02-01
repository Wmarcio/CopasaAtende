using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISU1Repository - Busca dados do cliente
    /// </summary>
    public class ISSCN4ISU1Repository : ISRepository<SCN4ISU1Receive>, IISSCN4ISU1Repository
    {
        private ILog _log;

        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISU1Repository(ILog log)
         : base("IdentUsuario:SCN4ISU1_WSD/IdentUsuario_SCN4ISU1_WSD_Port", "SCN4ISU1", log)
        {
            _log = log;
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
        protected override void TratarRetorno(SCN4ISU1Receive baseModelReceive)
        {
            SCN4ISU1Send sCN4ISU1Send = null;
            if (baseModelSend != null)
            {
                sCN4ISU1Send = (SCN4ISU1Send)baseModelSend;
            }
            List<SCN4ISU1ReceiveMatriculas> matriculasRetorno = new List<SCN4ISU1ReceiveMatriculas>();
            if (baseModelReceive.usuariosIS != null)
            {
                foreach (SCN4ISU1ReceiveUsuarios us in baseModelReceive.usuariosIS)
                {
                    if (us.matriculas != null)
                    {
                        foreach (SCN4ISU1ReceiveMatriculasUsuarios matriculaSicom in us.matriculas)
                        {
                            if (!"0".Equals(matriculaSicom.matricula))
                            {
                                baseModelReceive.descricaoRetorno = "";
                                SCN4ISU1ReceiveMatriculas matriculaRetorno = new SCN4ISU1ReceiveMatriculas();
                                matriculaSicom.situacaoAgua = matriculaSicom.situacaoAgua + matriculaSicom.produtoAgua;
                                matriculaSicom.situacaoEsgoto = matriculaSicom.situacaoEsgoto + matriculaSicom.produtoEsgoto;
                                if (matriculaSicom.CEP != null && !"".Equals(matriculaSicom.CEP))
                                {
                                    if ("0".Equals(matriculaSicom.CEP))
                                        matriculaSicom.CEP = "";
                                    else
                                        matriculaSicom.CEP = matriculaSicom.CEP.Substring(0, 2) + "." + matriculaSicom.CEP.Substring(2, 3) + "-" + matriculaSicom.CEP.Substring(5);
                                }

                                matriculaRetorno.setValues(matriculaSicom);
                                matriculaRetorno.logradouro = (matriculaSicom.tipoLogradouro + " " + matriculaSicom.nomeLogradouro).Trim();
                                string logradouro = matriculaSicom.tipoLogradouro + " " + matriculaSicom.nomeLogradouro;
                                matriculaRetorno.logradouro = logradouro;
                                matriculaRetorno.cpfCnpj = us.cpfCnpj;
                                if (!"".Equals(us.telefoneCelular) && !"0".Equals(us.telefoneCelular))
                                {
                                    string telCelular = us.telefoneCelular;
                                    int tamCelular = telCelular.Length;
                                    matriculaRetorno.telefoneCelular = "(" + us.DDDCelular + ") " + telCelular.Substring(0, tamCelular - 4) + "-" + telCelular.Substring(tamCelular - 4);
                                }
                                else
                                    matriculaRetorno.telefoneCelular = "";
                                if (!"".Equals(us.telefoneComercial) && !"0".Equals(us.telefoneComercial))
                                {
                                    string telComercial = us.telefoneComercial;
                                    matriculaRetorno.telefoneComercial = "(" + us.DDDComercial + ") " + telComercial.Substring(0, 4) + "-" + telComercial.Substring(4);
                                }
                                else
                                    matriculaRetorno.telefoneComercial = "";
                                if (!"".Equals(us.telefoneResidencia) && !"0".Equals(us.telefoneResidencia))
                                {
                                    string telResidencia = us.telefoneResidencia;
                                    matriculaRetorno.telefoneResidencia = "(" + us.DDDResidencia + ") " + telResidencia.Substring(0, 4) + "-" + telResidencia.Substring(4);
                                }
                                else
                                    matriculaRetorno.telefoneResidencia = "";

                                if (matriculaRetorno.cpfCnpj.Length > 0)
                                {
                                    if (matriculaRetorno.cpfCnpj.Length > 11)
                                    {
                                        matriculaRetorno.cpfCnpj = Convert.ToUInt64(matriculaRetorno.cpfCnpj).ToString(@"00\.000\.000\/0000-00");
                                    }
                                    else
                                    {
                                        matriculaRetorno.cpfCnpj = Convert.ToUInt64(matriculaRetorno.cpfCnpj).ToString(@"000\.000\.000\-00");
                                    }
                                }


                                matriculaRetorno.situacao = matriculaRetorno.situacaoAgua + matriculaRetorno.situacaoEsgoto;
                                if ("".Equals(matriculaRetorno.situacao.Trim()))
                                    matriculaRetorno.situacao = "N/A";
                                matriculaRetorno.codigoBairro = matriculaRetorno.codigoLocalidade + Util.Util.formataNumero(matriculaRetorno.codigoBairro, 6);
                                matriculaRetorno.codigoLogradouro = matriculaRetorno.codigoBairro + Util.Util.formataNumero(matriculaRetorno.codigoLogradouro, 6);
                                string dtIniVig = matriculaRetorno.dataInicioVigencia;
                                string dtFimVig = matriculaRetorno.dataFimVigencia;
                                matriculaRetorno.dataInicioVigencia = dtIniVig.Substring(6, 2) + "/" + dtIniVig.Substring(4, 2) + "/" + dtIniVig.Substring(0, 4)
                                    + " até " + dtFimVig.Substring(6, 2) + "/" + dtFimVig.Substring(4, 2) + "/" + dtFimVig.Substring(0, 4);
                                matriculaRetorno.vigencia = matriculaRetorno.dataInicioVigencia;
                                if (!"".Equals(matriculaRetorno.vigencia))
                                {
                                    int pos = matriculaRetorno.vigencia.IndexOf(" até ");
                                    if (pos > 0)
                                        matriculaRetorno.dataInicioVigenciaOrdenacao = DateTime.ParseExact(matriculaRetorno.vigencia.Substring(0, pos), "dd/MM/yyyy", null);
                                }
                                bool incluir = true;
                                if (("FAT5100".Equals(sCN4ISU1Send.codigoServicoSolicitado) ||
                                    "FAT5400".Equals(sCN4ISU1Send.codigoServicoSolicitado) ||
                                    (sCN4ISU1Send.codigoServicoSolicitado.Length > 5 && "60215".Equals(sCN4ISU1Send.codigoServicoSolicitado.Substring(0, 5))))
                                    && (matriculaRetorno.dataFimVigencia.ToDateTime("yyyyMMdd") < DateTime.Today || !"RA".Equals(matriculaSicom.situacaoAgua.ToUpper())))
                                    incluir = false;
                                if (incluir)
                                    matriculasRetorno.Add(matriculaRetorno);
                            }
                        }
                    }
                }
            }

            var matriculasOrdenadas = matriculasRetorno.OrderBy(x => long.Parse(x.identificador)).ThenBy(x => long.Parse(x.matricula)).ThenBy(x => x.dataInicioVigenciaOrdenacao).ToList();
            string identificadorAnterior = "";
            string matriculaAnterior = "";
            string vigenciAnterior = "";
            foreach (SCN4ISU1ReceiveMatriculas matricula in matriculasOrdenadas)
            {
                if (!matricula.identificador.Equals(identificadorAnterior)
                    || !matricula.matricula.Equals(matriculaAnterior)
                    || !matricula.vigencia.Equals(vigenciAnterior))
                {
                    baseModelReceive.matriculas.Add(matricula);
                }
                identificadorAnterior = matricula.identificador;
                matriculaAnterior = matricula.matricula;
                vigenciAnterior = matricula.vigencia;
            }
        }
    }
}
