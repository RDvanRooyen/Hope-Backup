using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Data;
using WebUI.Data.Models;
using WebUI.Services;

namespace WebUI.Factory
{
    public class ServiceFactory
    {

        /// <summary>
        /// configuration object used to fetch configuration values
        /// when instantiating DB Context, Services
        /// </summary>
        private IConfiguration _configuration;

        public ServiceFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TService> CreateService<TService>(AuthenticationStateProvider authStateProvider) where TService : ServiceBase
        {
            AuthenticationState authState = await authStateProvider.GetAuthenticationStateAsync();
            return (TService)Activator.CreateInstance(typeof(TService), CreateApplicationDbContext(), authState.GetUserId());
        }

        public ApplicationDbContext<ApplicationUser> CreateApplicationDbContext()
        {
            return new ApplicationDbContext<ApplicationUser>(
                    new DbContextOptionsBuilder<ApplicationDbContext<ApplicationUser>>()
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                    .Options
                );
        }

        public async Task<ServiceBase> CreateServiceBaseAsync(AuthenticationStateProvider authStateProvider)
        {
            return await CreateService<ServiceBase>(authStateProvider);
        }

        public ReportService CreateReportService()
        {
            return new ReportService(CreateApplicationDbContext());
        }

    }
}
