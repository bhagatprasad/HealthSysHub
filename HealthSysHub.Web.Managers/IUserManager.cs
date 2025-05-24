using HealthSysHub.Web.Utility.Models;
namespace HealthSysHub.Web.Managers
{
    public interface IUserManager
    {

        Task<IEnumerable<UserInfirmation>> FetchUsersAsync(Guid? hospitalId);
        Task<ChangePasswordResult> ChangePasswordAsync(ChangePassword changePassword);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassword forgotPassword);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPassword resetPassword);
        Task<ActivateOrInActivateUserResponse> ActivateOrInActivateUserAsync(ActivateOrInActivateUser activateOrInActivateUser);
    }
}
