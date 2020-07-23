using SubcontractProfile.WebApi.Services.Model;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(string id);

        Task<User> GetAsync(string id);
    }
}
