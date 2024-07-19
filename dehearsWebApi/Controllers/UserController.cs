using dehearsWebApi.Model.Auth;
using dehearsWebApi.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dehearsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(DataContext dataContext) : ControllerBase
    {
        private readonly UserServices _services = new UserServices(dataContext);

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel param)
        {
            var result = await _services.CreateUserAsync(param);

            var resultObject = result as dynamic;

            if (resultObject.IsInValid)
                return BadRequest(result);

            if (resultObject.IsExist)
                return Conflict(result); // Conflict status code (409) indicates resource username and email already exists

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserModel param)
        {
            var result = await _services.UpdateUserAsync(param);

            var resultObject = result as dynamic;

            if (resultObject.IsInValid)
                return BadRequest(result);

            if (resultObject.IsExist)
                return Conflict(result); // Conflict status code (409) indicates resource username and email already exists

            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _services.DeleteUserAsync(userId);

            var resultObject = result as dynamic;

            if (resultObject.IsInValid)
                return BadRequest(result);

            if (resultObject.IsExist)
                return Conflict(result); // Conflict status code (409) indicates resource username and email already exists

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            return Ok();
        }
    }
}
