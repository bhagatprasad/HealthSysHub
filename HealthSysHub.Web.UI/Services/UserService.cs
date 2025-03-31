using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory _repository;

        public UserService(IRepositoryFactory repository)
        {
            _repository = repository;

        }
        public async Task<IEnumerable<UserInfirmation>> FetchUsersAsync(Guid? hospitalId)
        {
            var requestUrl = "User/FetchUsersAsync";

            if (hospitalId.HasValue)
            {
                requestUrl = Path.Combine(requestUrl, hospitalId.ToString());
            }
            return await _repository.SendAsync<IEnumerable<UserInfirmation>>(HttpMethod.Get, requestUrl);
        }

        public async Task<User> InsertOrUpdateUserAsync(UserRegistration user)
        {
            return await _repository.SendAsync<UserRegistration, User>(HttpMethod.Post, "User/InsertOrUpdateUserAsync", user);
        }
    }
}
