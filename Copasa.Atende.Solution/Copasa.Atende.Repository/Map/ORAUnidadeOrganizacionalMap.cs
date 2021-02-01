using Copasa.Atende.Model;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map
{
    /// <summary>
    /// Mapa de campos da tabela Oracle Unidade Organizacional
    /// </summary>
    public class ORAUnidadeOrganizacionalMap : EntityTypeConfiguration<ORAUnidadeOrganizacional>
    {
        /// <summary>
        /// Mapeamento
        /// </summary>
        public ORAUnidadeOrganizacionalMap()
        {
            ToTable("CORP_UNIDADEORGANIZACIONAL");

            HasKey(x => x.codigo);

            Property(x => x.codigo).HasColumnName("CDUNIDADEORGANIZ");
            Property(x => x.sigla).HasColumnName("SGUNIDADEORGANIZ");
            Property(x => x.siglao).HasColumnName("SGSIGLAOUNIDADEORGANIZ");
        }
    }
}
