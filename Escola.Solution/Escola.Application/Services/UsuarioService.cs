using Escola.Application.DTOs;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Escola.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<string> AutenticarAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.AutenticarAsync(email, senha);
            if (usuario == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _usuarioRepository.AdicionarAsync(usuario);
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return await _usuarioRepository.ObterPorIdAsync(id);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            await _usuarioRepository.AtualizarAsync(usuario);
        }

        public async Task RemoverAsync(int id)
        {
            await _usuarioRepository.RemoverAsync(id);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _usuarioRepository.ObterPorEmailAsync(email);
        }
    }
}