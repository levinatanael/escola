using Escola.Domain.Entities;

namespace Escola.Domain.Interfaces.Repositories
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> ObterTodosAsync();
        Task<Aluno> ObterPorIdAsync(int id);
        Task AdicionarAsync(Aluno aluno);
        Task AtualizarAsync(Aluno aluno);
        Task RemoverAsync(int id);
    }
}