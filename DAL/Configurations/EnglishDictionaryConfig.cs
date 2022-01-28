using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class EnglishDictionaryConfig : IEntityTypeConfiguration<EnglishDictionary>
    {
        public void Configure(EntityTypeBuilder<EnglishDictionary> builder)
        {
            builder.Property(dic => dic.Name).IsRequired();
            builder.Property(dic => dic.Name).HasMaxLength(50);
            builder.Property(dic => dic.Description).IsRequired();
            builder.Property(dic => dic.Description).HasMaxLength(300);
            builder.HasOne(d => d.Creator)
                .WithMany(u => u.EnglishDictionaries)
                .HasForeignKey(d => d.UserId);
        }
    }
}
