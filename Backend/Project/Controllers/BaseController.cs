using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly UserManager<Person> _userManager;

        public BaseController(UserManager<Person> userManager)
        {
            _userManager = userManager;
        }

        public Task<Person> CurrentUser => _userManager.FindByNameAsync(
            User.Identity.Name ??
            User.Claims.Where(c => c.Properties.ContainsKey("unique_name"))
                .Select(c => c.Value)
                .FirstOrDefault()
        );
    }
}