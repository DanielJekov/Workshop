namespace Sandbox
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Workshop.Data;
    using Workshop.Data.Common;
    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Data.Repositories;
    using Workshop.Data.Seeding;
    using Workshop.Services.Data;
    using Workshop.Services.Messaging;

    using CommandLine;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.Text;
    using System.Security.Cryptography;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var guid1 = Guid.NewGuid().ToString().ToCharArray();
            var guid2 = Guid.NewGuid().ToString().ToCharArray();
            Console.WriteLine(new string('-', 60));

            //First Try 7 miliseconds!
            var stopWatch = new Stopwatch();
            //stopWatch.Start();
            //Console.WriteLine(GetHashFromTwoStringsNoMatterWhosFirst(guid1, guid2));
            //stopWatch.Stop();
            //Console.WriteLine(stopWatch.ElapsedMilliseconds);
            //stopWatch.Reset();

            //Second Try
            stopWatch.Start();
            Console.WriteLine(GetHashFromTwoStringsNoMatterWhosFirst(guid1, guid2));
            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds);

            //Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            //var serviceCollection = new ServiceCollection();
            //ConfigureServices(serviceCollection);
            //IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            //// Seed data on application startup
            //using (var serviceScope = serviceProvider.CreateScope())
            //{
            //    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    dbContext.Database.Migrate();
            //    new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            //}

            //using (var serviceScope = serviceProvider.CreateScope())
            //{
            //    serviceProvider = serviceScope.ServiceProvider;

            //    return Parser.Default.ParseArguments<SandboxOptions>(args).MapResult(
            //        opts => SandboxCode(opts, serviceProvider).GetAwaiter().GetResult(),
            //        _ => 255);
            //}
        }

        private static string GetHashFromTwoStringsNoMatterWhosFirst(char[] first, char[] second)
        {
            return GetHashString(ConcatTwoStrings(first, second));
        }

        private static string ConcatTwoStrings(char[] first, char[] second)
        {
            string result = string.Empty;
            for (int i = 0; i < first.Length; i++)
            {
                if ((int)first[i] == (int)second[i])
                {
                    continue;
                }

                if ((int)first[i] < (int)second[i])
                {
                    result = new string(first) + new string(second);
                }
                else
                {
                    result = new string(second) + new string(first);
                }

                break;
            }

            return result;
        }

        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private static async Task<int> SandboxCode(SandboxOptions options, IServiceProvider serviceProvider)
        {
            var sw = Stopwatch.StartNew();

            var settingsService = serviceProvider.GetService<ISettingsService>();
            Console.WriteLine($"Count of settings: {settingsService.GetCount()}");

            Console.WriteLine(sw.Elapsed);
            return await Task.FromResult(0);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .UseLoggerFactory(new LoggerFactory()));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
        }
    }
}
