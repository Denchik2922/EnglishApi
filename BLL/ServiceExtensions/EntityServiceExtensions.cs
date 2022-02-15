using Models.Entities;
using Models.Entities.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using BLL.RequestFeatures;

namespace BLL.ServiceExtensions
{
    public static class EntityServiceExtensions
    {
        public static IQueryable<T> SearchAndSort<T>(this IQueryable<T> list, PaginationParameters parameters) where T : class, IEntity
        {
            return list.Search(parameters.SearchTerm)
                       .Sort(parameters.OrderBy);
        }

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

        public static IQueryable<T> Sort<T>(this IQueryable<T> list, string orderByQueryString) where T : class
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return list.OrderBy(l => l);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return list.OrderBy(e => e);

            return list.OrderBy(orderQuery);
        }
    }
}
