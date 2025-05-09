﻿
using HealthSysHub.Web.Utility.Models;

namespace HealthSysHub.Web.UI.Interfaces
{
    public interface IAuthenticateService
    {
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
    }
}
