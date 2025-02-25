﻿using HealthSysHub.Web.DBConfiguration.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace HealthSysHub.Web.DBConfiguration
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DoctorAppointment> doctorAppointments { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Nurse> nurses { get; set; }
        public DbSet<Pharmacist> pharmacists { get; set; }
        public DbSet<Hospital> hospitals { get; set; }
        public DbSet<HospitalContent> hospitalContents { get; set; }
        public DbSet<HospitalContact> hospitalContacts { get; set; }
        public DbSet<HospitalDepartment> hospitalDepartments { get; set; }
        public DbSet<HospitalSpecialty> hospitalSpecialties { get; set; }
        public DbSet<HospitalType> hospitalTypes { get; set; }
        public DbSet<HospitalStaff> hospitalStaffs { get; set; }
        public DbSet<PatientCare> patientCares { get; set; }
        public DbSet<LabTest> labTests { get; set; }
        public DbSet<Receptionist> receptionists { get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<PaymentType> paymentTypes { get; set; }
        public DbSet<PatientType> patientTypes { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<Specialization> specializations { get; set; }
        public DbSet<User> users { get; set; }
    }
}
