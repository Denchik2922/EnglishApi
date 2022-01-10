using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class ResultForSpellingTestConfig : IEntityTypeConfiguration<ResultForSpellingTest>
    {
        public void Configure(EntityTypeBuilder<ResultForSpellingTest> builder)
        {
            builder.HasKey(t => new { t.UserId, t.EnglishDictionaryId });

            builder.HasOne(t => t.EnglishDictionary)
                   .WithMany(d => d.SpellingTestResults)
                   .HasForeignKey(t => t.EnglishDictionaryId);

            builder.HasOne(t => t.User)
                   .WithMany(u => u.SpellingTestResults)
                   .HasForeignKey(t => t.UserId);
            builder.Property(t => t.Score).IsRequired();
        }
    }
}
