using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ITagService : IBaseGenericService<Tag>
    {
        Task<ICollection<Tag>> GetAllAsync();
    }
}