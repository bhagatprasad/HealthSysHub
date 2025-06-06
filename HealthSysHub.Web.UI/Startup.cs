﻿using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using HealthSysHub.Web.UI.Factory;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using HealthSysHub.Web.UI.Reports;
using HealthSysHub.Web.UI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json.Serialization;
using QuestPDF.Infrastructure;

namespace HealthSysHub.Web.UI
{
    public class Startup
    {
        public readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            QuestPDF.Settings.License = LicenseType.Community;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddMvc();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddDirectoryBrowser();

            services.AddHttpContextAccessor();

            var healthSysHubConfig = configuration.GetSection("HealthSysHubConfig");

            services.Configure<HealthSysHubConfig>(healthSysHubConfig);

            services.AddHttpClient();

            services.AddScoped<HttpClientService>();

            services.AddTransient<TokenAuthorizationHttpClientHandler>();

            services.AddHttpClient("AuthorizedClient").AddHttpMessageHandler<TokenAuthorizationHttpClientHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRepositoryFactory, RepositoryFactory>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthenticateService, AuthenticateService>();

            services.AddScoped<IDoctorService, DoctorService>();

            services.AddScoped<IMedicineService, MedicineService>();

            services.AddScoped<ILabTestService, LabTestService>();

            services.AddScoped<IHospitalTypeService, HospitalTypeService>();

            services.AddScoped<IPatientTypeService, PatientTypeService>();

            services.AddScoped<IPaymentTypeService, PaymentTypeService>();

            services.AddScoped<IHospitalService, HospitalService>();

            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<ISpecializationService, SpecializationService>();

            services.AddScoped<IDoctorAppointmentService, DoctorAppointmentService>();

            services.AddScoped<IStaffService, StaffService>();

            services.AddScoped<ILabOrderRequestService, LabOrderRequestService>();

            services.AddScoped<IConsultationService, ConsultationService>();

            services.AddScoped<IPharmacyOrderRequestService, PharmacyOrderRequestService>();

            services.AddScoped<IPharmacyStaffService, PharmacyStaffService>();

            services.AddScoped<ILabStaffService, LabStaffService>();

            services.AddScoped<IPharmacyMedicineService, PharmacyMedicineService>();

            services.AddScoped<IPharmacyService, PharmacyService>();

            services.AddScoped<PdfLayoutService>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "allowCookies";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.SlidingExpiration = false;
                options.AccessDeniedPath = "/Error/NotAccessable";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("Patient", policy => policy.RequireRole("Patient"));
                options.AddPolicy("Doctor", policy => policy.RequireRole("Doctor"));
                options.AddPolicy("Pharmacist", policy => policy.RequireRole("Pharmacist"));
                options.AddPolicy("Nurse", policy => policy.RequireRole("Nurse"));
                options.AddPolicy("Executive", policy => policy.RequireRole("Executive"));
                options.AddPolicy("Lab technicians", policy => policy.RequireRole("Lab technicians"));
                options.AddPolicy("X-ray technicians", policy => policy.RequireRole("X-ray technicians"));
            });

            services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopCenter;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    ctx.Context.Response.Headers["Pragma"] = "no-cache";
                    ctx.Context.Response.Headers["Expires"] = "-1";
                }
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseNotyf();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
