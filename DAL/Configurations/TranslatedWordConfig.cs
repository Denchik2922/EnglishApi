using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class TranslatedWordConfig : IEntityTypeConfiguration<TranslatedWord>
    {
        public void Configure(EntityTypeBuilder<TranslatedWord> builder)
        {
            builder.Property(dic => dic.Name).IsRequired();
            builder.Property(dic => dic.Name).HasMaxLength(50);
        }
    }
}
