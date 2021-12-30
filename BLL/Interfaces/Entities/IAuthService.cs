using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IAuthService
    {
        Task Register(User user, string password);
    }
}