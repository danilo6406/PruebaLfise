using Blazored.LocalStorage;
using EasySales.Server.Areas.Identity.Data;
using EasySales.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Syncfusion.Blazor.Inputs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasySales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<EasySalesServerUser> userManager;
        private readonly SignInManager<EasySalesServerUser> signInManager;

        public LoginController(IConfiguration configuration, UserManager<EasySalesServerUser> userManager, SignInManager<EasySalesServerUser> signInManager)
        {
            _configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var user = await userManager.FindByNameAsync(loginModel.Email);

                if (user == null || !await userManager.CheckPasswordAsync(user, loginModel.Password))
                    return Unauthorized(new LoginResult { ErrorMessage = "Autentificacion Invalida." });

                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
                var Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                await signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, lockoutOnFailure: false);

                return Ok(new LoginResult { IsAuthSuccessful = true, Token = Token });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> LoginCookie(string name)
        {
            try
            {
                var user = await userManager.FindByNameAsync(name);

                if (user != null)
                {
                    await signInManager.SignInAsync(user, true, "token");
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return creds;
        }

        private async Task<List<Claim>> GetClaims(EasySalesServerUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {

            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var tokenOptions = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }



    }
}
