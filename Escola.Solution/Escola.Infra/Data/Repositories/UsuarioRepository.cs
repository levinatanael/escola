using Escola.Domain.Entities;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EscolaDbContext _context;

        public UsuarioRepository(EscolaDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            // Criptografa a senha antes de salvar
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var usuario = await ObterPorIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public bool VerificarSenha(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

        public async Task<Usuario> AutenticarAsync(string email, string senha)
        {
            var usuario = await ObterPorEmailAsync(email);
            if (usuario == null || !VerificarSenha(senha, usuario.Senha))
            {
                return null;
            }
            return usuario;
        }
    }
}