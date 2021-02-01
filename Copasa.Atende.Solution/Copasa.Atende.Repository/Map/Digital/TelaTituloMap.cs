using Copasa.Atende.Model.Digital;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map.Digital
{
    /// <summary>
    /// Classe de mapeamento para a entidade APP_TELA_TITULO.
    /// </summary>
    public class TelaTituloMap : EntityTypeConfiguration<TelaTituloModel>
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TelaTituloMap()
        {
            ToTable("APP_TELA_TITULO");

            HasKey(x => new { x.IdTela, x.IdTitulo });

            Property(x => x.IdTela)
                .HasColumnName("ID_TELA")
                .IsRequired();

            Property(x => x.IdTitulo)
                .HasColumnName("ID_TITULO")
                .IsRequired();

            Property(x => x.Ordem)
                .HasColumnName("NU_ORDEM")
                .IsRequired();

            HasRequired(x => x.Tela)
              .WithMany(x => x.TelaTitulos)
              .HasForeignKey(x => x.IdTela);

            HasRequired(x => x.Titulo)
             .WithMany(x => x.TelaTitulos)
             .HasForeignKey(x => x.IdTitulo);

        }
    }
}

 
 
