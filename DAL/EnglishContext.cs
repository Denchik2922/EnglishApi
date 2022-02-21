using DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL
{
    public class EnglishContext : IdentityDbContext<
        User, CustomRole, string,
        IdentityUserClaim<string>, CustomUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public EnglishContext(DbContextOptions<EnglishContext> options) : base(options) { }

        public DbSet<EnglishDictionary> EnglishDictionaries { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TranslatedWord> TranslatedWords { get; set; }
        public DbSet<WordExample> WordExamples { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TypeOfTesting> TypeOfTestings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EnglishDictionaryConfig());
            modelBuilder.ApplyConfiguration(new EnglishDictionaryTagConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            modelBuilder.ApplyConfiguration(new TestResultConfig());
            modelBuilder.ApplyConfiguration(new TranslatedWordConfig());
            modelBuilder.ApplyConfiguration(new WordConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }

    }
}
