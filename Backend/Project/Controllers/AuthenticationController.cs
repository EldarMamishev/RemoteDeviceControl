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
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApi.Helpers.Facades.Base;

namespace WebApi.Controllers
{
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Person> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenProvider _tokenProvider;
        private IPersonMappersFacade mappersFacade;

        public AuthenticationController(IConfiguration configuration, UserManager<Person> userManager, SignInManager<Person> signInManager,
            IMapper mapper, ITokenProvider tokenProvider, IPersonMappersFacade mappersFacade) 
            : base(userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
            this.mappersFacade = mappersFacade;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var user = this.mappersFacade.PersonMapper.MapPersonFromRegister(register);

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
            await this.Authenticate(login.Username);

            var user = await _userManager.FindByNameAsync(login.Username);
            var token = MakeToken(user);

            if (!Request.Headers.ContainsKey("www-authenticate"))
                Request.Headers.Append("www-authenticate", "Bearer " + token);


            Response.Cookies.Append("www-authenticate", token,
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(60)
            });
 
            Response.Cookies.Append(".AspNetCore.Application.Id", token,
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(60)
            });

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

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
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