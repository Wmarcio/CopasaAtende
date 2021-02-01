namespace Copasa.Atende.Util.Attributes
{
    using Interfaces;

    /// <summary>
    /// Attribute used to mark all entities which should be filtered based on tenantId
    /// </summary>
    public class TenantAwareAttribute :  ITenantAwareAttribute
    {
        /// <summary>
        /// Identificador da Unidade Organizacional que o usuário pertence.
        /// </summary>
        public string IdUnidadeOrganizacional { get; set; }

        /// <summary>
        /// Identificador da Unidade Operacional que o usuário pertence.
        /// </summary>
        public string IdUnidadeOperacional { get; set; }
    }
}
