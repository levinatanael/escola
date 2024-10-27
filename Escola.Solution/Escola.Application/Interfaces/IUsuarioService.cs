using Escola.Domain.Entities;

namespace Escola.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> AutenticarAsync(string email, string senha);
        Task AdicionarAsync(Usuario usuario);
        Task<Usuario> ObterPorIdAsync(int id);
        Task AtualizarAsync(Usuario usuario);
        Task RemoverAsync(int id);
        Task<Usuario> ObterPorEmailAsync(string email);
    }
}