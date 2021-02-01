using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRSSSend - cria solicitação de serviço
    /// </summary>
    public class SCN4CRSSSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-MATRICULA-ATEND")]
        public string matricula { get; set; }

        /// <summary>
        /// CodigoUsuario.
        /// </summary>
        [XmlElement("_RCV-COD-USUARIO-ATEND")]
        public string codigoUsuario { get; set; }

        /// <summary>
        /// NomeUsuario.
        /// </summary>
        [XmlElement("_RCV-NOME-USUARIO-ATEND")]
        public string nomeUsuario { get; set; }

        /// <summary>
        /// UnidadeUsuario.
        /// </summary>
        [XmlElement("_RCV-UNID-USUARIO-ATEND")]
        public string agenciaUsuario { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_RCV-COD-LOCAL-SOLICITACAO")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// CodigoLogradouro.
        /// </summary>
        [XmlElement("_RCV-COD-LOGRAD-SOLICITACAO")]
        public string codigoLogradouro { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_RCV-TIPO-LOGRAD-SOLICITACAO")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// NumeroLogradouro.
        /// </summary>
        [XmlElement("_RCV-NUM-IMOVEL-SOLICITACAO")]
        [IS("N", 5)]
        public string numeroLogradouro { get; set; }

        /// <summary>
        /// TipoComplementoLogradouro.
        /// </summary>
        [XmlElement("_RCV-TIPO-COMP-SOLICITACAO")]
        public string tipoComplementoLogradouro { get; set; }

        /// <summary>
        /// ComplementoLogradouro.
        /// </summary>
        [XmlElement("_RCV-COMPLEMENTO-SOLICITACAO")]
        public string complementoLogradouro { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        [XmlElement("_RCV-COD-BAIRRO-SOLICITACAO")]
        public string codigoBairro { get; set; }

        /// <summary>
        /// CodigoServicoSolicitado.
        /// </summary>
        [XmlElement("_RCV-COD-SERVICO-SOLICITADO")]
        public string codigoServicoSolicitado { get; set; }

        /// <summary>
        /// NumeroProtocolo.
        /// </summary>
        [XmlElement("_RCV-NUM-PROTOCOLO")]
        public string numeroProtocolo { get; set; }

        /// <summary>
        /// NomeSolicitante.
        /// </summary>
        [XmlElement("_RCV-NOME-SOLICITANTE")]
        public string nomeSolicitante { get; set; }

        /// <summary>
        /// TelefoneSolicitante.
        /// </summary>
        [XmlElement("_RCV-TELEFONE-SOLICITANTE")]
        public string telefoneSolicitante { get; set; }

        /// <summary>
        /// ReferenciaEndereco.
        /// </summary>
        [XmlElement("_RCV-REF-END-SOLICITACAO")]
        public string referenciaEndereco { get; set; }

        /// <summary>
        /// ObservacaoSicom.
        /// </summary>
        [XmlArray("Array")]
        [XmlArrayItem("_RCV-OBS-SOLICITACAO")]
        [JsonIgnore]
        public string[] observacaoSicom { get; set; }

        /// <summary>
        /// EmergenciaRisco.
        /// </summary>
        public string emergenciaRisco { get; set; }

        /// <summary>
        /// JustificativaEmergenciaRisco.
        /// </summary>
        public string justificativaEmergenciaRisco { get; set; }

        /// <summary>
        /// Observacao.
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// VerificaVazamentoProximo.
        /// </summary>
        [XmlElement("_RCV-GERA-VZMT-PROXIMO")]
        public string verificaVazamentoProximo { get; set; }

        /// <summary>
        /// UsuarioInterno.
        /// </summary>
        [JsonIgnore]
        public bool usuarioInterno { get; set; }
    }
}
