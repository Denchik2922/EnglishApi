using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class TypeOfTestingService : BaseGenericService<TypeOfTesting>, ITypeOfTestingService
    {
        public TypeOfTestingService(EnglishContext context) : base(context)
        {
        }

        public async Task<ICollection<TypeOfTesting>> GetAllAsync()
        {
            return await _context.TypeOfTestings
                                 .ToListAsync();
        }
    }
}
