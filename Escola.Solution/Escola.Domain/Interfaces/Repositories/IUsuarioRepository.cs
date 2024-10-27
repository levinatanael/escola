using Escola.Domain.Entities;

namespace Escola.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
        Task<Usuario> ObterPorIdAsync(int id);
        Task AtualizarAsync(Usuario usuario);
        Task RemoverAsync(int id);
        bool VerificarSenha(string senha, string hash);
        Task<Usuario> AutenticarAsync(string email, string senha);
    }
}