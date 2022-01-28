using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BLL.Tests.Infrastructure.Helpers
{
    public class DbContextHelper : IDisposable
    {
        public EnglishContext Context { get; set; }
        public DbContextHelper()
        {
            var serviceProvicer = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EnglishContext>()
                .UseInMemoryDatabase("UNIT_TESTING")
                .UseInternalServiceProvider(serviceProvicer);

            var options = builder.Options;
            Context = new EnglishContext(options);

            Context.AddRange(TagHelper.GetMany());
            Context.AddRange(DictionaryHelper.GetMany());
            Context.AddRange(EnglishDictionaryTagHelper.GetMany());
            Context.AddRange(TestResultHelper.GetMany());
            Context.AddRange(TranslatedWordHelper.GetMany());
            Context.AddRange(UserHelper.GetMany());
            Context.AddRange(WordHelper.GetMany());

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
