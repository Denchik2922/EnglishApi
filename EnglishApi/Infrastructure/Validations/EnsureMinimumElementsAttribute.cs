using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Infrastructure.Validations
{
    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;
        public EnsureMinimumElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null)
            {
                return collection.Count >= _minElements;
            }
            return false;
        }
    }
}
