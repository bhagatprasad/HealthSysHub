using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.Utility.Helpers;
using HealthSysHub.Web.Utility.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DataManagers
{
    public class UserDataManager : IUserManager
    {
        private readonly ApplicationDBContext _dBContext;
        public UserDataManager(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
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
                                                  DepartmentId = staffJoinInfo != null ? staffJoinInfo.DepartmentId: null,
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
      
    }
}
