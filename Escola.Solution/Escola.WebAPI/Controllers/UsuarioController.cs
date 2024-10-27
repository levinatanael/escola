using Escola.Application.DTOs;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Escola.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _usuarioService.AutenticarAsync(loginDto);
            if (token == null) return Unauthorized("Usuário ou senha inválidos.");

            return Ok(new { Token = token });
        }
    }
}