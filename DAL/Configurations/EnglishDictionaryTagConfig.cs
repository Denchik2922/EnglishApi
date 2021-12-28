using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class EnglishDictionaryTagConfig : IEntityTypeConfiguration<EnglishDictionaryTag>
    {
        public void Configure(EntityTypeBuilder<EnglishDictionaryTag> builder)
        {
            builder.HasKey(dt => new { dt.EnglishDictionaryId, dt.TagId });

            builder.HasOne(dt => dt.EnglishDictionary)
                   .WithMany(d => d.Tags)
                   .HasForeignKey(dt => dt.EnglishDictionaryId);

            builder.HasOne(dt => dt.Tag)
                   .WithMany(t => t.EnglishDictionaries)
                   .HasForeignKey(dt => dt.TagId);
        }
    }
}
