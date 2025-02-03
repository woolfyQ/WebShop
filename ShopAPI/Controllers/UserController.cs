using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController (UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO, CancellationToken cancellationToken)
        {
            var user = await _userService.Create(userDTO, cancellationToken);
            return Ok(user);
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteUser([FromBody] UserDTO userDTO, CancellationToken cancellationToken )
        {
            var user = await _userService.Delete(userDTO.Id, cancellationToken);
            return Ok(user);
        
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUser(Guid id,UserDTO userDTO, CancellationToken cancellationToken)
        {
            var user = await _userService.Update(id, userDTO, cancellationToken);
            return Ok(user);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(id,cancellationToken);
            return Ok(user);
        }






    }
}
