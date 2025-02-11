using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.Managers
{
    public interface IAuthenticationManager
    {
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
    }
}
