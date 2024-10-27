using Escola.Application.DTOs;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Autenticação de usuário para geração de token de autorização")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _usuarioService.AutenticarAsync(loginDto.Email, loginDto.Senha);
            if (token == null) return Unauthorized("Usuário ou senha inválidos.");

            return Ok(new { Token = token });
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Adicionar um novo usuário")]
        public async Task<IActionResult> Adicionar([FromBody] Usuario usuario)
        {
            await _usuarioService.AdicionarAsync(usuario);
            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Obter usuário pelo Id")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation(Summary = "Atualiza os dados do usuário")]
        public async Task<IActionResult> Atualizar([FromBody] Usuario usuario)
        {
            await _usuarioService.AtualizarAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Remove usuário cadastrado")]
        public async Task<IActionResult> Remover(int id)
        {
            await _usuarioService.RemoverAsync(id);
            return NoContent();
        }

        [HttpGet("email/{email}")]
        [Authorize]
        [SwaggerOperation(Summary = "Obtem usuário através do e-mail")]
        public async Task<IActionResult> ObterPorEmail(string email)
        {
            var usuario = await _usuarioService.ObterPorEmailAsync(email);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
    }
}