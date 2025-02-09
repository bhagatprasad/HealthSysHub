using HealthSysHub.Web.DBConfiguration.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Department> departments { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<LabTest> labTests { get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<PatientType> patientTypes { get; set; }
        public DbSet<PaymentType> paymentTypes { get; set; }
        public DbSet<Specialization> specializations { get; set; }
        public DbSet<HospitalType> hospitalTypes { get; set; }
    }
}
