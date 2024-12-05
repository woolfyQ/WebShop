using Application.Services;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Service;
using ShopAPI.Token;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly TokenProvider _tokenProvider;

        public AuthController(UserService userservice, AuthService authservice, TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
            _authService = authservice;
            _userService = userservice;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel loginModel, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Invalid model: {ModelState}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.ValidateUser(loginModel.Email, loginModel.Password, cancellationToken);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid password or Email" });
            }

            var token = _tokenProvider.Create(new UserDTO { Email = user.Email, Id = user.Id }); // Ensure token is created with DTO info
            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверка, существует ли пользователь с таким email
            var alreadyUser = await _userService.GetByEmailAsync(registerModel.Email, cancellationToken);
            if (alreadyUser != null)
            {
                return BadRequest(new { message = "User with this email already exists" });
            }

            // Хешируем пароль перед регистрацией
            registerModel.Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);

            var userDTO = new UserDTO
            {
                Email = registerModel.Email,
                Password = registerModel.Password,
            };

            // Создаем нового пользователя с переданным DTO
            var user = await _userService.Create(userDTO, cancellationToken);

            var token = _tokenProvider.Create(userDTO);

            return CreatedAtAction(nameof(Register), new { id = userDTO.Id }, new { Token = token });
        }
    }
}
