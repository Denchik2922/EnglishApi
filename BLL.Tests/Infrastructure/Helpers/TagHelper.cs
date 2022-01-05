using Models.Entities;
using System.Collections.Generic;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class TagHelper
    {
        public static Tag GetOne(int id)
        {
            return new Tag()
            {
                Id = id,
                Name = $"Color {id}",
                Description = $"Color {id} Description"
            };
        }

        public static IEnumerable<Tag> GetMany()
        {
            yield return GetOne(1);
        }
    }
}
