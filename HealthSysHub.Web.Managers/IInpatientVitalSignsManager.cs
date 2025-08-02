using HealthSysHub.Web.DBConfiguration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Managers
{
    public interface IInpatientVitalSignsManager
    {
        Task<List<InpatientVitalSigns>> GetAllVitalSignsAsync(); // Retrieve all vital signs
        Task<List<InpatientVitalSigns>> GetVitalSignsByInpatientIdAsync(Guid inpatientId); // Retrieve vital signs by inpatient ID
        Task<InpatientVitalSigns> GetVitalSignByIdAsync(Guid vitalSignId); // Retrieve a specific vital sign by ID
        Task<InpatientVitalSigns> InsertOrUpdateVitalSignAsync(InpatientVitalSigns vitalSign); // Insert or update a vital sign
        Task<bool> DeleteVitalSignAsync(Guid vitalSignId); // Delete a vital sign by ID (soft delete)
        Task<int> GetVitalSignCountAsync(); // Get the total count of vital signs
    }

}
