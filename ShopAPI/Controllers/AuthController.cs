using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Token;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly TokenProvider _tokenProvider;

        public AuthController(UserService userservice, TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
  
            _userService = userservice;
        }


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel loginModel, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetByEmailAsync(loginModel.Email, cancellationToken);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid password or Email" });
            }

            var token = _tokenProvider.CreateToken(new UserDTO { Email = user.Email, Id = user.Id }); // Ensure token is created with DTO info
            return Ok(new { Success = true, Token = token, UserId = user.Id });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var alreadyUser = await _userService.GetByEmailAsync(registerModel.Email, cancellationToken);
            if (alreadyUser != null)
            {
                return BadRequest(new { message = "User with this email already exists" });
            }

            var userDTO = new UserDTO
            {
                Email = registerModel.Email,
                Password = registerModel.Password,
            };

            var user = await _userService.Create(userDTO, cancellationToken);


            var token = _tokenProvider.CreateToken(userDTO);

            return CreatedAtAction(nameof(Register), new { Success = true, UserId = userDTO.Id }, new { Token = token, UserId = userDTO.Id });
        }
       

    }
}
