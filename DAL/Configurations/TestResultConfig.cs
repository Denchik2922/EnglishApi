using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class TestResultConfig : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.HasKey(t => new { t.UserId, t.EnglishDictionaryId });

            builder.HasOne(t => t.EnglishDictionary)
                   .WithMany(d => d.TestResults)
                   .HasForeignKey(t => t.EnglishDictionaryId);

            builder.HasOne(t => t.User)
                   .WithMany(u => u.TestResults)
                   .HasForeignKey(t => t.UserId);
            builder.Property(t => t.Score).IsRequired();
        }
    }
}
