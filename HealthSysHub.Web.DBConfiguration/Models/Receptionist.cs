﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.DBConfiguration.Models
{
    [Table("Receptionist")]
    public class Receptionist
    {
        [Key]
        public Guid ReceptionistId { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? StaffId { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public string? Experience { get; set; }
        public string? Education { get; set; }
        public string? Awards { get; set; }
        public string? ProfileUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public DateTimeOffset? JoiningDate { get; set; }
        public string? Status { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
