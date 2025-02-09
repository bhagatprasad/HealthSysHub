using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using HealthSysHub.Web.DBConfiguration;
using HealthSysHub.Web.Managers;
using HealthSysHub.Web.DataManagers;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {

        services.AddApplicationInsightsTelemetryWorkerService();

        var sqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(sqlConnection));

        // Register your data managers as scoped
        services.AddScoped<IRoleManager, RoleDataManager>();
        services.AddScoped<IDepartmentManager, DepartmentDataManager>();
        services.AddScoped<IHospitalTypeManager, HospitalTypeDataManager>();
        services.AddScoped<ILabTestManager, LabTestDataManager>();
        services.AddScoped<IMedicineManager, MedicineDataManager>();
        services.AddScoped<IPatientTypeManager, PatientTypeDataManager>();
        services.AddScoped<IPaymentTypeManager, PaymentTypeDataManager>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
            );
        });
    })
    .Build();

host.Run();