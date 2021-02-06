using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces.Services;
using UserAPI.Domain.ViewModels;

namespace UserAPI.Services.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Route("api")]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Authenticate and generates token for the given credentials
        /// </summary>
        /// <param name="user">user information</param>
        /// <response code="200">successful request</response>
        /// <response code="400">incorrect data, user not found or excluded</response>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Authenticate(
            [FromBody] LoginViewModel user,
            [FromServices] ITokenService tokenService,
            [FromServices] IUserService userService,
            [FromServices] IMapper mapper
            )
        {
            if (!user.IsValid)
                return BadRequest(ResultViewModel.From(data: null, success: false, message: "Please inform your email and password"));

            User appUser = userService.QueryAll(u => 
                u.Email.ToLower() == user.Email.ToLower() && 
                u.Password == user.Password, 
                bringExcluded: true)
                .FirstOrDefault();

            if (appUser == null)
                return BadRequest(ResultViewModel.From(data: user.Email, success: false, message: "Email or password incorrect")); ;

            if (appUser.Excluded)
                return BadRequest(ResultViewModel.From(data: user.Email, success: false, message: "User found, but inactive"));

            var token = tokenService.GenerateToken(appUser);

            return Ok(ResultViewModel.From(data: token, success: true, message: "User successfully authenticated"));
        }
    }
}
