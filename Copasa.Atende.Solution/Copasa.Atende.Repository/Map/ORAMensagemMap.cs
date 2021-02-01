using Copasa.Atende.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map
{
    /// <summary>
    /// ORAMensagemMap - Mensagens de retorno do sistema
    /// </summary>
    public class ORAMensagemMap : EntityTypeConfiguration<ORAMensagem>
    {
        /// <summary>
        /// Construtor ORAMensagemMap.
        /// </summary>
        public ORAMensagemMap()
        {
            // Primary Key
            HasKey(t => t.idMensagem);

            // Properties
            Property(t => t.idMensagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("COMERCIAL_CRM_MENSAGEM", "COMERCIAL");
            Property(t => t.idMensagem).HasColumnName("ID_MENSAGEM");
            Property(t => t.descricaoServico).HasColumnName("DS_SERVICO");
            Property(t => t.descricaoMensagem).HasColumnName("DS_MENSAGEM");
        }
    }
}
