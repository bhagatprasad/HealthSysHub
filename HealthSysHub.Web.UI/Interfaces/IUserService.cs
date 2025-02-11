using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IUserService
    {
        Task<User> InsertOrUpdateUserAsync(UserRegistration user);

        Task<IEnumerable<UserInfirmation>> FetchUsersAsync();
    }
}
