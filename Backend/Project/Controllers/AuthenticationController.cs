using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Helpers.Token;
using ViewModel.Auth;

namespace WebApi.Controllers
{
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenProvider _tokenProvider;

        public AuthenticationController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IMapper mapper, ITokenProvider tokenProvider) 
            : base(userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var user = _mapper.Map<ApplicationUser>(register);

            var identityResult = await _userManager.CreateAsync(user, register.Password);
            if (!identityResult.Succeeded)
                return BadRequest(identityResult.Errors);

            await _signInManager.SignInAsync(user, false);
            var token = MakeToken(user);

            return Ok(token);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            if (!loginResult.Succeeded)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(login.Username);
            var token = MakeToken(user);

            return Ok(token);
        }

        [HttpPost]
        [Route("refreshtoken")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await CurrentUser;
            var token = MakeToken(user);

            return Ok(token);
        }

        private string MakeToken(IdentityUser<int> user)
        {
            var config = GetTokenConfiguration();
            return _tokenProvider.MakeToken(user, config);
        }

        private TokenConfiguration GetTokenConfiguration()
        {
            var lifeTime = _configuration.GetValue<int>("Tokens:Lifetime");
            var audience = _configuration.GetValue<string>("Tokens:Audience");
            var issuer = _configuration.GetValue<string>("Tokens:Issuer");
            var tokenKey = _configuration.GetValue<string>("Tokens:Key");

            return new TokenConfiguration(lifeTime, audience, issuer, tokenKey);
        }
    }
}