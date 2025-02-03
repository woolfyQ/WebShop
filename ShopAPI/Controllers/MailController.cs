using Application.Services;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly EmailService _emailService;
        public MailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMessage(UserMessage userMessage)
        {
            // Логируем данные, которые были получены на сервере
            Console.WriteLine($"Received message: {userMessage.Name}, {userMessage.Email}, {userMessage.Message}");

            // Проверим корректность данных
            if (string.IsNullOrEmpty(userMessage.Name) || string.IsNullOrEmpty(userMessage.Email) || string.IsNullOrEmpty(userMessage.Message))
            {
                return BadRequest("Все поля формы должны быть заполнены.");
            }

            try
            {
                // Пытаемся отправить письмо
                await _emailService.SendEmail(userMessage);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                // Логируем ошибку отправки email
                Console.WriteLine($"Error sending email: {ex.Message}");
                return StatusCode(500, $"Error sending email: {ex.Message}");
            }
        }
    }
}
