using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class ResultForMatchingTestConfig : IEntityTypeConfiguration<ResultForMatchingTest>
    {
        public void Configure(EntityTypeBuilder<ResultForMatchingTest> builder)
        {
            builder.HasKey(t => new { t.UserId, t.EnglishDictionaryId });

            builder.HasOne(t => t.EnglishDictionary)
                   .WithMany(d => d.MatchingTestResults)
                   .HasForeignKey(t => t.EnglishDictionaryId);

            builder.HasOne(t => t.User)
                   .WithMany(u => u.MatchingTestResults)
                   .HasForeignKey(t => t.UserId);
            builder.Property(t => t.Score).IsRequired();
        }
    }
}
