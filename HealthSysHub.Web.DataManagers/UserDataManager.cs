using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthSysHub.Web.DataManagers
{
    public class UserDataManager : IUserManager
    {
        private readonly ApplicationDBContext _dBContext;
        public UserDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<ChangePasswordResult> ChangePasswordAsync(ChangePassword changePassword)
        {
            var result = new ChangePasswordResult();

            if (changePassword == null)
            {
                result.Success = false;
                result.Message = "Change password request cannot be null.";
                return result;
            }

            var user = await _dBContext.users.FindAsync(changePassword.Id);
            if (user == null)
            {
                result.Success = false;
                result.Message = "User  not found.";
                return result;
            }

            if (!user.IsActive.Value)
            {
                result.Success = false;
                result.Message = "User  account is not active.";
                return result;
            }

            if (string.IsNullOrEmpty(changePassword.Password))
            {
                result.Success = false;
                result.Message = "Password cannot be empty.";
                return result;
            }
            HealthSysHubHashSalt hashSalt = HealthSysHubHashSalt.GenerateSaltedHash(changePassword.Password);
            user.PasswordHash = hashSalt.Hash;
            user.PasswordSalt = hashSalt.Salt;
            user.LastPasswordChangedOn = changePassword.ModifiedOn;
            user.ModifiedOn = changePassword.ModifiedOn;
            user.ModifiedBy = changePassword.ModifiedBy;

            await _dBContext.SaveChangesAsync();

            result.Success = true;
            result.Message = "Password changed successfully.";
            return result;
        }


        public async Task<IEnumerable<UserInfirmation>> FetchUsersAsync(Guid? hospitalId)
        {
            try
            {
                var userInfirmations = await (from user in _dBContext.users
                                              join role in _dBContext.roles on user.RoleId equals role.RoleId into roleJoinInfo
                                              from roleInfo in roleJoinInfo.DefaultIfEmpty()
                                              join hospital in _dBContext.hospitals on user.HospitalId equals hospital.HospitalId into hospitalInfo
                                              from hospitalJoinInfo in hospitalInfo.DefaultIfEmpty()
                                              join staff in _dBContext.hospitalStaffs on user.StaffId equals staff.StaffId into staffInfo
                                              from staffJoinInfo in staffInfo.DefaultIfEmpty()
                                              join department in _dBContext.departments on staffJoinInfo.DepartmentId equals department.DepartmentId into departmentInfo
                                              from departmentJoinInfo in departmentInfo.DefaultIfEmpty()
                                              where hospitalId == null || user.HospitalId == hospitalId
                                              select new UserInfirmation
                                              {
                                                  Id = user.Id,
                                                  FirstName = user.FirstName,
                                                  LastName = user.LastName,
                                                  FullName = user.FirstName + " " + user.LastName,
                                                  Email = user.Email,
                                                  Phone = user.Phone,
                                                  RoleId = user.RoleId,
                                                  RoleName = roleInfo != null ? roleInfo.Name : null,
                                                  HospitalId = user.HospitalId,
                                                  StaffId = user.StaffId,
                                                  HospitalName = hospitalJoinInfo != null ? hospitalJoinInfo.HospitalName : null,
                                                  Designation = staffJoinInfo != null ? staffJoinInfo.Designation : null,
                                                  DepartmentId = staffJoinInfo != null ? staffJoinInfo.DepartmentId : null,
                                                  DepartmentName = departmentJoinInfo != null ? departmentJoinInfo.DepartmentName : null,
                                                  LastPasswordChangedOn = user.LastPasswordChangedOn,
                                                  IsBlocked = user.IsBlocked,
                                                  CreatedBy = user.CreatedBy,
                                                  CreatedOn = user.CreatedOn,
                                                  ModifiedBy = user.ModifiedBy,
                                                  ModifiedOn = user.ModifiedOn,
                                                  IsActive = user.IsActive.Value
                                              }).ToListAsync();

                return userInfirmations;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                throw new ApplicationException("An error occurred while fetching users.", ex);
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            var result = new ForgotPasswordResponse();

            if (forgotPassword == null)
            {
                result.Success = false;
                result.Message = "Forgot password request cannot be null.";
                return result;
            }

            switch (forgotPassword.EntityType)
            {
                case "Hospital":
                    result = await HandleForgotPasswordForHospitalAsync(forgotPassword);
                    break;

                case "Pharmacy":
                    result = await HandleForgotPasswordForPharmacyAsync(forgotPassword);
                    break;

                case "Lab":
                    result = await HandleForgotPasswordForLabAsync(forgotPassword);
                    break;

                default:
                    result.Success = false;
                    result.Message = "Invalid entity type.";
                    break;
            }

            return result;
        }
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPassword resetPassword)
        {
            var result = new ResetPasswordResponse();

            // Validate the input
            if (resetPassword == null)
            {
                result.Success = false;
                result.Message = "Reset password request cannot be null.";
                return result;
            }

            // Retrieve the user from the database
            var user = await _dBContext.users.FindAsync(resetPassword.Id);

            // Check if the user exists
            if (user == null)
            {
                result.Success = false;
                result.Message = "User  not found for reset password.";
                return result;
            }

            // Validate the new password
            if (string.IsNullOrEmpty(resetPassword.Password))
            {
                result.Success = false;
                result.Message = "Password cannot be empty.";
                return result;
            }

            // Generate the salted hash for the new password
            HealthSysHubHashSalt hashSalt = HealthSysHubHashSalt.GenerateSaltedHash(resetPassword.Password);
            user.PasswordHash = hashSalt.Hash;
            user.PasswordSalt = hashSalt.Salt;
            user.ModifiedBy = resetPassword.ModifiedBy;
            user.ModifiedOn = resetPassword.ModifiedOn;

            // Save changes to the database
            await _dBContext.SaveChangesAsync();

            // Set success response
            result.Success = true;
            result.Message = "Reset password successful.";

            return result;
        }

        public async Task<ActivateOrInActivateUserResponse> ActivateOrInActivateUserAsync(ActivateOrInActivateUser activateOrInActivateUser)
        {
            var result = new ActivateOrInActivateUserResponse();

            if (activateOrInActivateUser == null)
            {
                result.Success = false;
                result.Message = "Activate/De-Activate request cannot be null.";
                return result;
            }

            if (!activateOrInActivateUser.HospitalId.HasValue &&
                !activateOrInActivateUser.PharmacyId.HasValue &&
                !activateOrInActivateUser.LabId.HasValue)
            {
                result.Success = false;
                result.Message = "At least one of HospitalId, PharmacyId, or LabId must be provided.";
                return result;
            }

            if (activateOrInActivateUser.HospitalId.HasValue)
            {
                result = await ActivateOrInActivateHospitalStaffAsync(activateOrInActivateUser);
            }
            else if (activateOrInActivateUser.PharmacyId.HasValue)
            {
                result = await ActivateOrInActivatePharmacyStaffAsync(activateOrInActivateUser);
            }
            else if (activateOrInActivateUser.LabId.HasValue)
            {
                result = await ActivateOrInActivateLabStaffAsync(activateOrInActivateUser);
            }

            return result;
        }
        private async Task<ForgotPasswordResponse> HandleForgotPasswordForHospitalAsync(ForgotPassword forgotPassword)
        {
            var result = new ForgotPasswordResponse();
            var hospital = await _dBContext.hospitals
                .FirstOrDefaultAsync(x => x.HospitalCode.ToLower() == forgotPassword.EntityCode.ToLower());

            if (hospital != null)
            {
                var user = await _dBContext.users
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == forgotPassword.Email.ToLower() && x.HospitalId == hospital.HospitalId);

                if (user != null)
                {
                    result.Id = user.Id;
                    result.StaffId = user.StaffId;
                    result.Success = true;
                    result.Message = $"Request for forgot password with Hospital Code {forgotPassword.EntityCode} found.";
                }
                else
                {
                    result.Success = false;
                    result.Message = $"Request for forgot password with Hospital Code {forgotPassword.EntityCode} not found.";
                }
            }
            else
            {
                result.Success = false;
                result.Message = $"Request for forgot password with Hospital Code {forgotPassword.EntityCode} not found.";
            }

            return result;
        }

        private async Task<ForgotPasswordResponse> HandleForgotPasswordForPharmacyAsync(ForgotPassword forgotPassword)
        {
            var result = new ForgotPasswordResponse();
            var pharmacy = await _dBContext.pharmacies
                .FirstOrDefaultAsync(x => x.PharmacyCode.ToLower() == forgotPassword.EntityCode.ToLower());

            if (pharmacy != null)
            {
                var user = await _dBContext.users
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == forgotPassword.Email.ToLower() && x.PharmacyId == pharmacy.PharmacyId);

                if (user != null)
                {
                    result.Id = user.Id;
                    result.StaffId = user.StaffId;
                    result.Success = true;
                    result.Message = $"Request for forgot password with Pharmacy Code {forgotPassword.EntityCode} found.";
                }
                else
                {
                    result.Success = false;
                    result.Message = $"Request for forgot password with Pharmacy Code {forgotPassword.EntityCode} not found.";
                }
            }
            else
            {
                result.Success = false;
                result.Message = $"Request for forgot password with Pharmacy Code {forgotPassword.EntityCode} not found.";
            }

            return result;
        }

        private async Task<ForgotPasswordResponse> HandleForgotPasswordForLabAsync(ForgotPassword forgotPassword)
        {
            var result = new ForgotPasswordResponse();
            var lab = await _dBContext.labs
                .FirstOrDefaultAsync(x => x.LabCode.ToLower() == forgotPassword.EntityCode.ToLower());

            if (lab != null)
            {
                var user = await _dBContext.users
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == forgotPassword.Email.ToLower() && x.LabId == lab.LabId);

                if (user != null)
                {
                    result.Id = user.Id;
                    result.StaffId = user.StaffId;
                    result.Success = true;
                    result.Message = $"Request for forgot password with Lab Code {forgotPassword.EntityCode} found.";
                }
                else
                {
                    result.Success = false;
                    result.Message = $"Request for forgot password with Lab Code {forgotPassword.EntityCode} not found.";
                }
            }
            else
            {
                result.Success = false;
                result.Message = $"Request for forgot password with Lab Code {forgotPassword.EntityCode} not found.";
            }

            return result;
        }


        private async Task<ActivateOrInActivateUserResponse> ActivateOrInActivateHospitalStaffAsync(ActivateOrInActivateUser activateOrInActivateUser)
        {
            var result = new ActivateOrInActivateUserResponse();
            var hospitalStaff = await _dBContext.hospitalStaffs
                .FirstOrDefaultAsync(h => h.HospitalId == activateOrInActivateUser.HospitalId && h.StaffId == activateOrInActivateUser.StaffId);

            if (hospitalStaff == null)
            {
                result.Success = false;
                result.Message = "Hospital staff not found.";
                return result;
            }

            hospitalStaff.IsActive = activateOrInActivateUser.IsActive;
            hospitalStaff.ModifiedBy = activateOrInActivateUser.ModifiedBy;
            hospitalStaff.ModifiedOn = activateOrInActivateUser.ModifiedOn;

            var hospitalStaffUser = await _dBContext.users
                .FirstOrDefaultAsync(hs => hs.HospitalId == activateOrInActivateUser.HospitalId && hs.StaffId == activateOrInActivateUser.StaffId);

            if (hospitalStaffUser == null)
            {
                result.Success = false;
                result.Message = "Hospital staff user not found.";
                return result;
            }

            hospitalStaffUser.IsActive = activateOrInActivateUser.IsActive;
            hospitalStaffUser.ModifiedBy = activateOrInActivateUser.ModifiedBy;
            hospitalStaffUser.ModifiedOn = activateOrInActivateUser.ModifiedOn;

            result.Success = true;
            result.Message = "Hospital staff activated/deactivated successfully.";
            return result;
        }

        private async Task<ActivateOrInActivateUserResponse> ActivateOrInActivatePharmacyStaffAsync(ActivateOrInActivateUser activateOrInActivateUser)
        {
            var result = new ActivateOrInActivateUserResponse();
            var pharmacyStaff = await _dBContext.pharmacyStaff
                .FirstOrDefaultAsync(x => x.PharmacyId == activateOrInActivateUser.PharmacyId.Value);

            if (pharmacyStaff == null)
            {
                result.Success = false;
                result.Message = "Pharmacy staff not found.";
                return result;
            }

            pharmacyStaff.IsActive = activateOrInActivateUser.IsActive;
            pharmacyStaff.ModifiedBy = activateOrInActivateUser.ModifiedBy;
            pharmacyStaff.ModifiedOn = activateOrInActivateUser.ModifiedOn;

            var pharmacyUser = await _dBContext.users
                .FirstOrDefaultAsync(m => m.StaffId == activateOrInActivateUser.StaffId);

            if (pharmacyUser == null)
            {
                result.Success = false;
                result.Message = "Pharmacy staff user not found.";
                return result;
            }

            pharmacyUser.IsActive = activateOrInActivateUser.IsActive;
            pharmacyUser.ModifiedBy = activateOrInActivateUser.ModifiedBy;
            pharmacyUser.ModifiedOn = activateOrInActivateUser.ModifiedOn;

            result.Success = true;
            result.Message = "Pharmacy staff activated/deactivated successfully.";
            return result;
        }

        private async Task<ActivateOrInActivateUserResponse> ActivateOrInActivateLabStaffAsync(ActivateOrInActivateUser activateOrInActivateUser)
        {
            var result = new ActivateOrInActivateUserResponse();
            var labStaff = await _dBContext.labStaff
                .FirstOrDefaultAsync(x => x.LabId == activateOrInActivateUser.LabId.Value && x.StaffId == activateOrInActivateUser.StaffId.Value);

            if (labStaff == null)
            {
                result.Success = false;
                result.Message = "Lab staff not found.";
                return result;
            }

            labStaff.IsActive = activateOrInActivateUser.IsActive;
            labStaff.ModifiedBy = activateOrInActivateUser.ModifiedBy;
            labStaff.ModifiedOn = activateOrInActivateUser.ModifiedOn;

            var labUserStaff = await _dBContext.users
                .FirstOrDefaultAsync(m => m.LabId == activateOrInActivateUser.LabId.Value && m.StaffId == activateOrInActivateUser.StaffId.Value);

            if (labUserStaff == null)
            {
                result.Success = false;
                result.Message = "Lab staff user not found.";
                return result;
            }

            labUserStaff.IsActive = activateOrInActivateUser.IsActive;
            labUserStaff.ModifiedBy = activateOrInActivateUser.ModifiedBy;
            labUserStaff.ModifiedOn = activateOrInActivateUser.ModifiedOn;

            result.Success = true;
            result.Message = "Lab staff activated/deactivated successfully.";
            return result;
        }

    }
}
