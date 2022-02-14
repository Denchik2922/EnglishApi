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
    public class TestResultConfig : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.HasOne(t => t.EnglishDictionary)
                   .WithMany(d => d.TestResults)
                   .HasForeignKey(t => t.EnglishDictionaryId);

            builder.HasOne(t => t.User)
                   .WithMany(u => u.TestResults)
                   .HasForeignKey(t => t.UserId);

            builder.Property(t => t.Score).IsRequired();
            builder.Property(t => t.Date).IsRequired();
        }
    }
}
