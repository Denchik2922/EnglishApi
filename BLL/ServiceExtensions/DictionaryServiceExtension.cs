using Models.Entities;
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

            return dictionaries.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
