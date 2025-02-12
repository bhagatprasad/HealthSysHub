using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IUserManager
    {

        Task<IEnumerable<UserInfirmation>> FetchUsersAsync(Guid? hospitalId);

    }
}
