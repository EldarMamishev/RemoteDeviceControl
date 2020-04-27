using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly UserManager<User> _userManager;

        public BaseController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<User> CurrentUser => _userManager.FindByEmailAsync(
            User.Identity.Name ??
            User.Claims.Where(c => c.Properties.ContainsKey("unique_name"))
                .Select(c => c.Value)
                .FirstOrDefault()
        );
    }
}