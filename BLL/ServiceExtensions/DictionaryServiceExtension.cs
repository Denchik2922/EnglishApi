using Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BLL.ServiceExtensions
{
    public static class DictionaryServiceExtension
    {
        public static IQueryable<EnglishDictionary> Search(this IQueryable<EnglishDictionary> dictionaries, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return dictionaries;
            }
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return dictionaries.Where(d => d.Name.ToLower().Contains(lowerCaseSearchTerm));
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
