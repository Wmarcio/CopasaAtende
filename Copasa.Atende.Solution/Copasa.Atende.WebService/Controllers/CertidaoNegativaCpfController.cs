using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Util;
using Copasa.Atende.WebService.Models;
using Newtonsoft.Json;
using Rotativa;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Copasa.Atende.WebService.Controllers
{
    /// <summary>
    /// CertidaoNegativaCpfController
    /// </summary>
    public class CertidaoNegativaCpfController : Controller
    {
        /// <summary>
        /// Método que gera o PDF da nova certidão negativa de débito
        /// </summary>
        /// <returns></returns>
        public ActionResult GerarPdfPost()
        {
            ILog log = new Log();
            try
            {
                Stream req = Request.InputStream;
                req.Seek(0, System.IO.SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();
                SCN6ISCNView sCN6ISCNView = new SCN6ISCNView();
                try
                {
                    sCN6ISCNView = JsonConvert.DeserializeObject<SCN6ISCNView>(json);
                }
                catch (Exception)
                {
                    // Try and handle malformed POST body
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if ("PEDIDO INDEFERIDO".Equals(sCN6ISCNView.deferimento.ToUpper()))
                    sCN6ISCNView.mensagemRetorno = sCN6ISCNView.deferimento.ToUpper();
                var certidaoNegativaViewModel = new CertidaoNegativaViewModel();
                AutoMapper.Mapper.Map(sCN6ISCNView, certidaoNegativaViewModel);
                var gerarPdf = new ViewAsPdf()
                {
                    Model = certidaoNegativaViewModel,
                    ViewName = "Index",
                    PageSize = Rotativa.Options.Size.A4
                };

                return gerarPdf;
            }
            catch (Exception e)
            {
                log.GravaLog("Erro na certidao negativa:"+e.Message);
                return null;
            }
        }

        /// <summary>
        /// Método temporário para testar geração de PDF
        /// </summary>
        /// <returns></returns>
        public ActionResult TestePdf()
        {
            try
            {
                string json = "{\"mensagemRetorno\":\"\",\"textoCorpo\":\"DECLARAMOS, QUE A(S) MATRICULA(S) RELACI0NADA(S) ABAIXO, CADASTRADA(S) EM NOME DO USUÁRIO RAULINSON JOSE CORREA MARTINS, CPF/CNPJ 05828418602, APRESENTA, NA PRESENTE DATA, DÉBITO(S) VENCIDO(S) COM A COPASA-MG.\",\"local\":\"CARATINGA, 03/01/2020 12:32:16\",\"deferimento\":\"PEDIDO INDEFERIDO\",\"textoDebitos\":\"\",\"textoParcelamentos\":\"\",\"textoLancamentos\":\"\",\"identificadores\":[{\"IdentificadorCliente\":\"127208640\",\"Enderecos\":[{\"MatriculaCliente\":\"22916024\",\"CodigoRetorno\":0,\"descricaoTipoLogradouro\":\"R. DONA LELECA, 10, SANTA CRUZ, 35300000, CARATINGA, MG\",\"Debitos\":[{\"numeroFatura\":117569904669,\"valorTotalfatura\":\"466,80\",\"dataVencimentoFatura\":\"01/11/2017\"},{\"numeroFatura\":117630798665,\"valorTotalfatura\":\"433,01\",\"dataVencimentoFatura\":\"03/12/2017\"},{\"numeroFatura\":117689774380,\"valorTotalfatura\":\"423,92\",\"dataVencimentoFatura\":\"02/01/2018\"},{\"numeroFatura\":118029146550,\"valorTotalfatura\":\"385,45\",\"dataVencimentoFatura\":\"03/02/2018\"},{\"numeroFatura\":118098360875,\"valorTotalfatura\":\"337,45\",\"dataVencimentoFatura\":\"05/03/2018\"},{\"numeroFatura\":118161721787,\"valorTotalfatura\":\"336,51\",\"dataVencimentoFatura\":\"03/04/2018\"},{\"numeroFatura\":118224870741,\"valorTotalfatura\":\"336,51\",\"dataVencimentoFatura\":\"05/05/2018\"},{\"numeroFatura\":118286380055,\"valorTotalfatura\":\"336,51\",\"dataVencimentoFatura\":\"19/06/2018\"},{\"numeroFatura\":118347630668,\"valorTotalfatura\":\"330,89\",\"dataVencimentoFatura\":\"04/07/2018\"},{\"numeroFatura\":118401484903,\"valorTotalfatura\":\"342,26\",\"dataVencimentoFatura\":\"04/08/2018\"}],\"Parcelamentos\":[],\"Lancamentos\":[],\"DebitosVencer\":[]},{\"MatriculaCliente\":\"24147885\",\"CodigoRetorno\":0,\"descricaoTipoLogradouro\":\"AV. DARIO DA ANUNCIACAO GROSSI, 35, DARIO GROSSI, 35300000, CARATINGA, MG\",\"Debitos\":[{\"numeroFatura\":119579087236,\"valorTotalfatura\":\"123,55\",\"dataVencimentoFatura\":\"28/10/2019\"},{\"numeroFatura\":119641462937,\"valorTotalfatura\":\"124,10\",\"dataVencimentoFatura\":\"26/11/2019\"},{\"numeroFatura\":119704449011,\"valorTotalfatura\":\"106,69\",\"dataVencimentoFatura\":\"28/12/2019\"}],\"Parcelamentos\":[],\"Lancamentos\":[],\"DebitosVencer\":[]},{\"MatriculaCliente\":\"127208917\",\"CodigoRetorno\":0,\"descricaoTipoLogradouro\":\"R. JORGE TEIXEIRA SIQUEIRA, 410, SANTA CRUZ, 35300000, CARATINGA, MG\",\"Debitos\":[{\"numeroFatura\":119654204907,\"valorTotalfatura\":\"70,65\",\"dataVencimentoFatura\":\"03/12/2019\"},{\"numeroFatura\":119717243734,\"valorTotalfatura\":\"58,20\",\"dataVencimentoFatura\":\"01/01/2020\"}],\"Parcelamentos\":[],\"Lancamentos\":[],\"DebitosVencer\":[]},{\"MatriculaCliente\":\"148702023\",\"CodigoRetorno\":0,\"descricaoTipoLogradouro\":\"R. DONA LELECA, 10, SANTA CRUZ, 35300000, CARATINGA, MG\",\"Debitos\":[{\"numeroFatura\":117517496047,\"valorTotalfatura\":\"1.202,58\",\"dataVencimentoFatura\":\"02/10/2017\"},{\"numeroFatura\":117569904685,\"valorTotalfatura\":\"1.219,35\",\"dataVencimentoFatura\":\"01/11/2017\"},{\"numeroFatura\":117630798681,\"valorTotalfatura\":\"3.468,24\",\"dataVencimentoFatura\":\"03/12/2017\"},{\"numeroFatura\":117689774401,\"valorTotalfatura\":\"1.319,04\",\"dataVencimentoFatura\":\"02/01/2018\"},{\"numeroFatura\":118029146576,\"valorTotalfatura\":\"76,45\",\"dataVencimentoFatura\":\"03/02/2018\"},{\"numeroFatura\":118161721809,\"valorTotalfatura\":\"35,68\",\"dataVencimentoFatura\":\"03/04/2018\"},{\"numeroFatura\":118224870768,\"valorTotalfatura\":\"35,68\",\"dataVencimentoFatura\":\"05/05/2018\"},{\"numeroFatura\":118286380080,\"valorTotalfatura\":\"35,68\",\"dataVencimentoFatura\":\"19/06/2018\"},{\"numeroFatura\":118347630684,\"valorTotalfatura\":\"34,99\",\"dataVencimentoFatura\":\"04/07/2018\"},{\"numeroFatura\":118401484938,\"valorTotalfatura\":\"36,37\",\"dataVencimentoFatura\":\"04/08/2018\"},{\"numeroFatura\":118467360668,\"valorTotalfatura\":\"35,68\",\"dataVencimentoFatura\":\"03/09/2018\"}],\"Parcelamentos\":[],\"Lancamentos\":[],\"DebitosVencer\":[]}]}],\"textoRodape\":\"ESTA CERTIDÃO NÃO CONTEMPLA EVENTUAIS DÉBITOS POSTERIORMENTE APURADOS EM FUNÇÃO DE IDENTIFICAÇÃO DE IRREGULARIDADE(S) OU DE REVISÃO DE FATURAMENTO.\",\"textoComplementar\":\"\",\"textoComplementar1\":\"OS VALORES AQUI APRESENTADOS, JÁ VENCIDOS, SERÃO ACRESCIDOS DE MULTA, JUROS DE MORA E CORREÇÃO MONETÁRIA.\",\"textoComplementar2\":\"CASO O(S) DÉBITO(S) ACIMA MENCIONADO(S) JÁ TENHA(M) SIDO QUITADO(S), FINEZA PROCURAR UMA AGÊNCIA DE ATENDIMENTO E APRESENTAR A(S) FATURA(S) PAGA(S), PARA EMISSÃO DA CERTIDÃO NEGATIVA DE DÉBITO.\"}";
                SCN6ISCNView sCN6ISCNView = new SCN6ISCNView();

                try
                {
                    sCN6ISCNView = JsonConvert.DeserializeObject<SCN6ISCNView>(json);
                }
                catch (Exception)
                {
                    // Try and handle malformed POST body
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var certidaoNegativaViewModel = new CertidaoNegativaViewModel();
                AutoMapper.Mapper.Map(sCN6ISCNView, certidaoNegativaViewModel);
                var gerarPdf = new ViewAsPdf()
                {
                    Model = certidaoNegativaViewModel,
                    ViewName = "Index",
                    PageSize = Rotativa.Options.Size.A4
                };
                var controler = this.ControllerContext;

                var controllerContext = this.ControllerContext;
                byte[] bytes = gerarPdf.BuildPdf(controllerContext);


                return gerarPdf;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}