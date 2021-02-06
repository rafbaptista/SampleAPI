using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UserAPI.Domain.Interfaces.Services;
using UserAPI.Domain.ViewModels;
using System;
using System.Threading.Tasks;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Interfaces.Transactions;
using UserAPI.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace UserAPI.Services.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Return a list of all users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">successful request</response>
        /// <response code="500">If any error occurred while fetching database</response>
        [HttpGet]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 60)]
        //will cache endpoint for 60s. hit F5 to refresh, test only in browser advancing and retreating page    
        public IActionResult Get(
            [FromServices] IUserService userService,
            [FromServices] IMapper mapper,
            [FromServices] IUserRepository userRepository
            )
        {
            try
            {
                var users = userService.QueryAll(bringExcluded: false).ToList();
                var usersViewModel = mapper.Map<List<UserViewModel>>(users);
                var result = ResultViewModel.From(usersViewModel, true, "Active users successfully displayed");

                return Ok(result);
            }
            catch (Exception)
            {
                var result = ResultViewModel.From(null, false, "There was an error trying to retrieve the requested data");
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }
        }

        /// <summary>
        /// Return user by id or null if not found
        /// </summary>
        /// <param name="id">id of user (guid)</param>
        /// <returns></returns>
        /// <response code="200">successful request</response>
        /// <response code="400">user not found</response>
        /// <response code="500">internal server error</response>
        [HttpGet]
        [Route("{id:guid}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 60)]
        public IActionResult GetById(
            Guid id,
            [FromServices] IUserService userService,
            [FromServices] IMapper mapper
            )
        {
            try
            {
                var user = userService.GetById(id, bringExcluded: false);

                if (user == null)
                    return BadRequest(ResultViewModel.From(id, true, "User not found"));

                var userViewModel = mapper.Map<UserViewModel>(user);

                return Ok(ResultViewModel.From(userViewModel, true, "User successfully displayed"));
            }
            catch (Exception)
            {
                var result = ResultViewModel.From(null, false, "There was an error trying to retrieve the requested data");
                return StatusCode(StatusCodes.Status500InternalServerError, result);
                 
            }
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model">Object containg user information to be registered</param>
        /// <response code="200">successful request</response>
        /// <response code="400">invalid payload format or missing required fields</response>
        /// <response code="500">internal server error</response>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ResultViewModel>> Post(
            [FromBody] User model,
            [FromServices] IUserService userService,
            [FromServices] IMapper mapper,
            [FromServices] IUnitOfWork uow
            )
        {
            var validation = model.Validate();

            if (!validation.IsValid)
                return BadRequest(ResultViewModel.From(validation.Errors, false, "Data sent was not on a valid format"));

            try
            {
                userService.Add(model);
                await uow.Commit();
                return Ok(ResultViewModel.From(mapper.Map<UserViewModel>(model), true, "User successfully created"));
            }
            catch (Exception)
            {
                var result = ResultViewModel.From(null, false, "There was an error trying to create user");
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="id">Id (guid) of user</param>
        /// <param name="model">user object</param>
        /// <response code="200">successful request</response>
        /// <response code="400">invalid payload format or missing required fields</response>
        /// <response code="500">internal server error</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ResultViewModel>> Put(
            [FromRoute] Guid id,    
            [FromBody] User model,
            [FromServices] IUserService userService,
            [FromServices] IMapper mapper,
            [FromServices] IUnitOfWork uow
            )
        {
            var validation = model.Validate();

            if (!validation.IsValid)
                return BadRequest(ResultViewModel.From(validation.Errors, false, "Data sent was not on a valid format"));

            if (id != model.Id)
                return BadRequest(ResultViewModel.From(null, false, "User not foud"));
            
            try
            {
                userService.Update(model);
                await uow.Commit();
                return Ok(ResultViewModel.From(mapper.Map<UserViewModel>(model), true, "User successfully updated"));
            }
            catch (Exception)
            {
                var result = ResultViewModel.From(null, false, "There was an error trying to update user");
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">id (guid) of user</param>
        /// <response code="200">successful request</response>
        /// <response code="400">user not found</response>
        /// <response code="500">internal server error</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "manager")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultViewModel>> Delete(
            [FromRoute] Guid id,
            [FromServices] IUserService userService,
            [FromServices] IMapper mapper,
            [FromServices] IUnitOfWork uow
            )
        {
            try
            {
                var user = userService.GetById(id);

                if (user == null)
                    return BadRequest(ResultViewModel.From(id, false, "User not found"));

                userService.Delete(user);
                await uow.Commit();
                return Ok(ResultViewModel.From(null, true, "User successfully deleted"));
            }
            catch (Exception)
            {
                var result = ResultViewModel.From(null, false, "There was an error trying to update user");
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
    }
}