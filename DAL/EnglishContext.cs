using DAL.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL
{
    public class EnglishContext : IdentityDbContext<User>
    {
        public EnglishContext(DbContextOptions<EnglishContext> options): base(options){}

        public DbSet<EnglishDictionary> EnglishDictionaries { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TranslatedWord> TranslatedWords { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<ResultForSpellingTest> ResultForSpellingTests { get; set; }
        public DbSet<ResultForMatchingTest> ResultForMatchingTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new EnglishDictionaryConfig());
            modelBuilder.ApplyConfiguration(new EnglishDictionaryTagConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            modelBuilder.ApplyConfiguration(new ResultForMatchingTestConfig());
            modelBuilder.ApplyConfiguration(new ResultForSpellingTestConfig());
            modelBuilder.ApplyConfiguration(new TranslatedWordConfig());
            modelBuilder.ApplyConfiguration(new WordConfig());
        }

	}
}
