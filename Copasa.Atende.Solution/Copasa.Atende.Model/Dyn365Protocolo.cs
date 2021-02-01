using Copasa.Atende.Model.Core;
using Newtonsoft.Json;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365Protocolo - Tabela de protocolo/incident no Dynamics
    /// </summary>
    [Dyn365Name("incidents")]
    public class Dyn365Protocolo : BaseModel
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [Dyn365Name("copasa_cpf_cnpj")]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [Dyn365Name("copasa_matriculaintegracao")]
        public string matricula { get; set; }

        /// <summary>
        /// IdentificadorIntegracao.
        /// </summary>
        [Dyn365Name("copasa_identificadorintegracao")]
        public string identificadorIntegracao { get; set; }

        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [Dyn365Name("copasa_codigoerro")]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        [Dyn365IdCopasa("copasa_protocolo")]
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// NumeroSolicitacaoServico.
        /// </summary>
        [Dyn365Name("copasa_codigodass")]
        public string numeroSolicitacaoServico { get; set; }

        /// <summary>
        /// CodigoServicoSS.
        /// </summary>
        [Dyn365Name("copasa_codigoservico")]
        [Dyn365KeyBind("copasa_codigo", "copasa_tipodepavimentacaoid", "idTipoPavimentacao", "copasa_tipodepavimentacaos")]
        public string codigoServico { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        public string descricaoServicoSS { get; set; }

        /// <summary>
        /// dataGeracaoSSDyn365.
        /// </summary>
        [Dyn365Name("copasa_geracaoss")]
        public System.DateTime? dataGeracaoSSDyn365 { get; set; }

        /// <summary>
        /// DataGeracaoSS.
        /// </summary>
        [JsonIgnore]
        public string dataGeracaoSS { get; set; }

        /// <summary>
        /// HoraGeracaoSS.
        /// </summary>
        [JsonIgnore]
        public string horaGeracaoSS { get; set; }

        /// <summary>
        /// DataPrevicaoSSDyn365.
        /// </summary>
        [Dyn365Name("copasa_previsaoatendimentoss")]
        public System.DateTime? dataPrevisaoSSDyn365 { get; set; }

        /// <summary>
        /// DataPrevicsoSS.
        /// </summary>
        [JsonIgnore]
        public string dataPrevisaoSS { get; set; }

        /// <summary>
        /// HoraPrevicsoSS.
        /// </summary>
        [JsonIgnore]
        public string horaPrevisaoSS { get; set; }

        /// <summary>
        /// DataBaixaSSDyn365.
        /// </summary>
        [Dyn365Name("copasa_execucaobaixa")]
        public System.DateTime? dataBaixaSSDyn365 { get; set; }

        /// <summary>
        /// DataBaixaSSDyn365.
        /// </summary>
        [Dyn365Name("copasa_terminodoatendimento")]
        public System.DateTime? dataTerminoAtendimentoDyn365 { get; set; }

        /// <summary>
        /// DataBaixaSS.
        /// </summary>
        [JsonIgnore]
        public string dataBaixaSS { get; set; }

        /// <summary>
        /// HoraBaixaSS.
        /// </summary>
        [JsonIgnore]
        public string horaBaixaSS { get; set; }

        /// <summary>
        /// SituacaoSS.
        /// </summary>
        [Dyn365Name("copasa_codigostatusss")]
        public string situacaoSS { get; set; }

        /// <summary>
        /// EmailUnidadeResponsavel.
        /// </summary>
        [Dyn365Name("copasa_emailunidaderesponsavel")]
        public string emailUnidadeResponsavel { get; set; }

        /// <summary>
        /// NumeroPriorizacao.
        /// </summary>
        [Dyn365Name("copasa_numeropriorizacao")]
        public string numeroPriorizacao { get; set; }

        /// <summary>
        /// DescricaoSituacaoSS.
        /// </summary>
        [Dyn365Name("copasa_statusdoservico")]
        public string descricaoSituacaoSS { get; set; }

        /// <summary>
        /// ObservacaoSSSicom.
        /// </summary>
        public string[] observacaoSSSicom { get; set; }

        /// <summary>
        /// IdDyn365.
        /// </summary>
        [Dyn365Id("incidentid")]
        public string incidentid { get; set; }

        /// <summary>
        /// StatusCode.
        /// </summary>
        [Dyn365Name("statuscode")]
        public int situacaoSolicitacao { get; set; }

        /// <summary>
        /// StateCode.
        /// </summary>
        [Dyn365Name("statecode")]
        public int situacaoProtocolo { get; set; }

        /// <summary>
        /// Titulo.
        /// </summary>
        [Dyn365Name("title")]
        public string titulo { get; set; }

        /// <summary>
        /// DescricaoServico.
        /// </summary>
        [Dyn365DisplayBind("copasa_servicosid")]
        public object descricaoServico { get; set; }

        /// <summary>
        /// IdSolicitacaoRelacionadaBind.
        /// </summary>
        [Dyn365DisplayBind("copasa_subtipoid")]
        public object descricaoSubTipoServico { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        [Dyn365Name("createdon")]
        public System.DateTime? CreatedOn { get; set; }

        /// <summary>
        /// RespostaAoSolicitante
        /// </summary>
        [Dyn365Name("copasa_respostaaosolicitante")]
        public string RespostaAoSolicitante { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        [Dyn365Name("caseorigincode")]
        public int Origem { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [Dyn365Name("copasa_identificadorintegracao")]
        public string identificador { get; set; }

        /// <summary>
        /// TipoOcorrencia.
        /// </summary>
        [Dyn365Name("casetypecode")]
        public string tipoOcorrencia { get; set; }

        /// <summary>
        /// InformarProtocolo. Valores válidos 1=Sim 2=Não
        /// </summary>
        [Dyn365Name("copasa_informarprotocolo")]
        public int informarProtocolo { get; set; }

        /// <summary>
        /// ProtocoloRelacionado.
        /// </summary>
        [Dyn365Name("copasa_protocrelacionado")]
        public string protocoloRelacionado { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// CodigotipoLogradouro.
        /// </summary>
        [Dyn365Name("copasa_tipodelogradouro")]
        public int codigotipoLogradouro { get; set; }

        /// <summary>
        /// NumeroImovel.
        /// </summary>
        [Dyn365Name("copasa_numeroimovel")]
        public string numeroImovel { get; set; }

        /// <summary>
        /// TipoComplementoImovel.
        /// </summary>
        [Dyn365Name("copasa_tipodecomplemento")]
        public string tipoComplementoImovel { get; set; }

        /// <summary>
        /// ComplementoImovel.
        /// </summary>
        [Dyn365Name("copasa_complemento")]
        public string complementoImovel { get; set; }

        /// <summary>
        /// EntreRuas.
        /// </summary>
        [Dyn365Name("copasa_referenciaentreruas")]
        public string entreRuas { get; set; }

        /// <summary>
        /// TelefoneURA.
        /// </summary>
        [Dyn365Name("copasa_telefoneura")]
        public string telefoneURA { get; set; }

        /// <summary>
        /// NomeSolicitante.
        /// </summary>
        [Dyn365Name("copasa_nomecompleto")]
        public string nomeSolicitante { get; set; }

        /// <summary>
        /// TelefoneSolicitante.
        /// </summary>
        [Dyn365Name("copasa_telephone2")]
        public string telefoneSolicitante { get; set; }

        /// <summary>
        /// EmailSolicitante.
        /// </summary>
        [Dyn365Name("copasa_email")]
        public string emailSolicitante { get; set; }

        /// <summary>
        /// IdSolicitacaoRelacionadaBind.
        /// </summary>
        [Dyn365Bind("incidents", "copasa_SolicitacaoRelacionadaId")]
        public string idSolicitacaoRelacionadaBind { get; set; }
           
        /// <summary>
        /// IdLocalidade.
        /// </summary>
        [Dyn365Bind("copasa_localidades", "copasa_localidadeid")]
        public string idLocalidade { get; set; }
        
        /// <summary>
        /// IdeSuperintendenciaBind.
        /// </summary>
        [Dyn365Bind("copasa_superintendencias", "copasa_superintendenciaid")]
        public string ideSuperintendenciaBind { get; set; }

        /// <summary>
        /// IdSolicitacaoRelacionadaBind.
        /// </summary>
        [Dyn365Bind("copasa_unidades", "copasa_unidadeenvolvidaid")]
        public string idUnidadeEnvolvidaBind { get; set; }

        /// <summary>
        /// IdSubtipoServico.
        /// </summary>
        [Dyn365Bind("copasa_subtipodeservicos", "copasa_subtipoid")]
        [Dyn365KeyBind("copasa_subtipodeservicoid", "_copasa_portfoliodeservicoid_value", "idPortfolioServico", "copasa_subtipodeservicos")]
        public string idSubtipoServico { get; set; }

        /// <summary>
        /// IdPortfolioServico.
        /// </summary>
        [Dyn365Bind("copasa_portfoliodeservicos", "copasa_servicosid")]
        public string idPortfolioServico { get; set; }

        /// <summary>
        /// IdLogradouro.
        /// </summary>
        [Dyn365Bind("copasa_logradouros", "copasa_Logradouroid")]
        public string idLogradouro { get; set; }

        /// <summary>
        /// IdBairro.
        /// </summary>
        [Dyn365Bind("copasa_bairros", "copasa_Bairroid")]
        public string idBairro { get; set; }

        /// <summary>
        /// IdBairro.
        /// </summary>
        [Dyn365Bind("copasa_tipodepavimentacaos", "copasa_tipopavimentacaoid")]
        public string idTipoPavimentacao { get; set; }
        
        /// <summary>
        /// IdSolicitante.
        /// </summary>
        [Dyn365Bind("contacts", "customerid_contact")]
        public string idSolicitante { get; set; }

        /// <summary>
        /// CodigoSubtipoServico.
        /// </summary>
        [Dyn365KeyBind("copasa_codigosicom", "copasa_subtipodeservicoid", "idSubtipoServico", "copasa_subtipodeservicos")]
        public string codigoSubtipoServico { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [Dyn365KeyBind("copasa_codigo", "copasa_localidadeid", "idLocalidade", "copasa_localidades")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        [Dyn365KeyBind("copasa_codigo", "copasa_bairroid", "idBairro", "copasa_bairros")]
        public string codigoBairro { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        [Dyn365KeyBind("copasa_codigo", "copasa_logradouroid", "idLogradouro", "copasa_logradouros")]
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// DescricaoSolicitacao.
        /// </summary>
        [Dyn365Name("description")]
        public string descricaoSolicitacao { get; set; }

        /// <summary>
        /// InformacoesAtendente.
        /// </summary>
        [Dyn365Name("copasa_informacoesdoatendente")]
        public string informacoesAtendente { get; set; }
    }
}
