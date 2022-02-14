using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ITypeOfTestingService : IBaseGenericService<TypeOfTesting>
    {
        Task<ICollection<TypeOfTesting>> GetAllAsync();
    }
}
