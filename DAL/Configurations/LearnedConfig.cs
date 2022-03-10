using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class LearnedConfig : IEntityTypeConfiguration<LearnedWord>
    {
        public void Configure(EntityTypeBuilder<LearnedWord> builder)
        {
            builder.Property(l => l.CountTrueAnswers).HasMaxLength(10);
        }
    }
}
