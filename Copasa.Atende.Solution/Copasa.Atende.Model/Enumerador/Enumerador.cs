using System.ComponentModel;

namespace Copasa.Atende.Model.Enumerador
{
    /// <summary>
    /// Enumerador para Ambiente
    /// </summary>
    public enum EnvironmentEnum
    {
        /// <summary>
        /// Homologacao.
        /// </summary>
        H,
        /// <summary>
        /// Producao.
        /// </summary>
        P,
        /// <summary>
        /// Desenvolvimento
        /// </summary>
        D
    }

    /// <summary>
    /// Tipo de Operação.
    /// </summary>
    public enum TipoOperacaoEnum
    {
        /// <summary>
        /// INSERT.
        /// </summary>
        INSERT = 1,
        /// <summary>
        /// UPDATE.
        /// </summary>
        UPDATE = 2,
        /// <summary>
        /// DELETE.
        /// </summary>
        DELETE = 3,
        /// <summary>
        /// SELECT.
        /// </summary>
        SELECT = 4
    }

    /// <summary>
    /// Sistema que está chamando o ws
    /// </summary>
    public static class TipoOrigem
    {
        /// <summary>
        /// App compasa atende
        /// </summary>
        public static string APP = "APP",
        #region PRODEMGE em produção        
        // MGAPP(já registra origem)        
        MG = "MG",
        // MGAPP Empresarial
        MGE = "MGE",
        // WebMGAPP        
        WMG = "WMG",
        // Toten
        TMG = "TMG",
        #endregion
        #region IMPLY em teste
        // MGAPP
        IMP = "IMP",
        // MGAPP Empresarial
        IEP = "IEP",
        // WevMGAPP        
        WIM = "WIM",
        // Toter
        TIM = "WIM";
        #endregion        
    }
}
