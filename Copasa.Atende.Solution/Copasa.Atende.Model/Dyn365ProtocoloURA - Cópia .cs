using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Dyn365ProtocoloURA - Tabela de protocolo/incident com dados da URA no Dynamics
    /// </summary>
    [Dyn365Name("incidents")]
    public class Dyn365ProtocoloURA : BaseModel
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [Dyn365Name("copasa_cpf_cnpj")]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// IDSolicitante.
        /// </summary>
        [Dyn365Name("_customerid_value")]
        public string IDSolicitante { get; set; }

        /// <summary>
        /// IDProtocolo.
        /// </summary>
        [Dyn365Name("incidentid")]
        public string IDProtocolo { get; set; }

        /// <summary>
        /// NumeroProtocolo.
        /// </summary>
        [Dyn365Name("copasa_protocolo")]
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [Dyn365Name("copasa_identificadorintegracao")]
        public string identificador { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [Dyn365Name("copasa_matriculaintegracao")]
        public string matricula { get; set; }

        /// <summary>
        /// TipoOcorrencia.
        /// </summary>
        [Dyn365Name("casetypecode")]
        public string tipoOcorrencia { get; set; }

        /// <summary>
        /// CodigoServico.
        /// </summary>
        [Dyn365Name("copasa_codigoservico")]
        public string codigoServico { get; set; }

        /// <summary>
        /// InformarProtocolo.
        /// </summary>
        [Dyn365Name("copasa_informarprotocolo")]
        public string informarProtocolo { get; set; }

        /// <summary>
        /// ProtocoloRelacionado.
        /// </summary>
        [Dyn365Name("copasa_protocrelacionado")]
        public string protocoloRelacionado { get; set; }

        /// <summary>
        /// IDLocalidade.
        /// </summary>
        [Dyn365Name("_copasa_localidadeid_value")]
        public string IDLocalidade { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [Dyn365Name("copasa_tipodelogradouro")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// IDLogradouro.
        /// </summary>
        [Dyn365Name("_copasa_logradouroid_value")]
        public string IDLogradouro { get; set; }

        /// <summary>
        /// IDBairro.
        /// </summary>
        [Dyn365Name("_copasa_bairroid_value")]
        public string IDBairro { get; set; }

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
        /// DescricaoSolicitacao.
        /// </summary>
        [Dyn365Name("Description")]
        public string descricaoSolicitacao { get; set; }

        /// <summary>
        /// InformacoesAtendente.
        /// </summary>
        [Dyn365Name("copasa_informacoesdoatendente")]
        public string informacoesAtendente { get; set; }

        /// <summary>
        /// SituacaoProtocolo.
        /// </summary>
        [Dyn365Name("Statecode")]
        public string situacaoProtocolo { get; set; }

        /// <summary>
        /// SituacaoSolicitacao.
        /// </summary>
        [Dyn365Name("Statuscode")]
        public string situacaoSolicitacao { get; set; }

    }
}
