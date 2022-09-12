using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using System.Threading.Tasks;
using WebUI.Data.Models;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext<ApplicationUser> _dbContext;

        public CompanyController(Data.ApplicationDbContext<ApplicationUser> dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<int>> CreateCompany([FromBody]Company company)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Companies.Add(company);
                await _dbContext.SaveChangesAsync();
                return Ok(company.CompanyId);
            }

            return BadRequest();
        }
    }
}
