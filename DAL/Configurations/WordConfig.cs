using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class WordConfig : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.Property(dic => dic.Name).IsRequired();
            builder.Property(dic => dic.Name).HasMaxLength(50);
            builder.Property(dic => dic.Transcription).IsRequired();
            builder.Property(dic => dic.Transcription).HasMaxLength(50);
        }
    }
}
