using Copasa.Atende.Model.Digital;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map.Digital
{
    /// <summary>
    /// Classe de mapeamento para a entidade APP_TELA.
    /// </summary>

    public class TelaMap : EntityTypeConfiguration<TelaModel> 
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TelaMap()
        {
            ToTable("APP_TELA");

            HasKey(x => x.IdTela)
                .Property(x => x.IdTela)
                .HasColumnName("ID_TELA")
                .IsRequired();

            Property(x => x.Descricao)
              .HasColumnName("DS_TELA")
              .IsRequired();

            Property(x => x.Detalhe)
              .HasColumnName("DS_DETALHE");

            Ignore(x => x.IsAtivo);
        }
    }
}
