using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(tag => tag.Name).IsRequired();
            builder.Property(tag => tag.Name).HasMaxLength(50);
            builder.Property(tag => tag.Description).IsRequired();
            builder.Property(tag => tag.Description).HasMaxLength(300);

            builder.HasData(
             new Tag
             {
                 Id = 1,
                 Name = "Profession",
                 Description = "Profession is a disciplined group of individuals who adhere to ethical standards and who hold themselves out as," +
                 " and are accepted by the public as possessing special knowledge and skills in a widely recognised body of learning derived from"
             },
             new Tag
             {
                 Id = 2,
                 Name = "Office",
                 Description = "An office is a space where an organization's employees perform administrative work in order to support" +
                                " and realize objects and goals of the organization."
             });

        }
    }
}
