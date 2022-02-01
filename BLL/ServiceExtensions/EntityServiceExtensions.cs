using Models.Entities;
using Models.Entities.Interfaces;
using System;
using System.Linq;

namespace BLL.ServiceExtensions
{
    public static class EntityServiceExtensions
    {
        public static IQueryable<T> Search<T>(this IQueryable<T> list, string searchTerm) where T : class, IEntity
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return list;
            }
            var lowerCaseSearchTerm = searchTerm.Trim().ToLowerInvariant();

            return list.Where(d => d.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<EnglishDictionary> SearchByTags(this IQueryable<EnglishDictionary> dictionaries, string tagsSearch)
        {
            if (string.IsNullOrWhiteSpace(tagsSearch))
            {
                return dictionaries;
            }
            var tags = tagsSearch.Split(',');

            return dictionaries.Where(d => d.Tags.Any(t => tags.Contains(t.Tag.Name)));
        }
    }
}
