namespace Workshop.Web
{
    using System.Reflection;

    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Workshop.Data;
    using Workshop.Data.Common;
    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Data.Repositories;
    using Workshop.Data.Seeding;
    using Workshop.Services.Cloudinary;
    using Workshop.Services.Data.Courses;
    using Workshop.Services.Data.HashProvider;
    using Workshop.Services.Data.Messages;
    using Workshop.Services.Data.Notifications;
    using Workshop.Services.Data.NotificationsUsersStatusCollection;
    using Workshop.Services.Data.Roles;
    using Workshop.Services.Data.Search;
    using Workshop.Services.Data.Topics;
    using Workshop.Services.Data.Users;
    using Workshop.Services.Mapping;
    using Workshop.Services.Messaging;
    using Workshop.Web.Hubs;
    using Workshop.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            var cloudinaryCredentials = new CloudinaryDotNet.Account(
            this.configuration["Cloudinary:CloudName"],
            this.configuration["Cloudinary:ApiKey"],
            this.configuration["Cloudinary:ApiSecret"]);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddSignalR();

            services.AddSingleton(cloudinaryUtility);
            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddSingleton<INotificationUsersStatusCollection, NotificationUsersStatusCollection>();

            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IHashProvider, HashProvider>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<INotificationsService, NotificationsService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IHashProvider, HashProvider>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<ITopicsService, TopicsService>();
            services.AddTransient<ISearchService, SearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithRedirects("/Home/Error{0}");

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/chat");
                        endpoints.MapHub<NotificationHub>("/notifications");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
