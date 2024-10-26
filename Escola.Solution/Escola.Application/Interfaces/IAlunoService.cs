using Escola.Application.DTOs;

namespace Escola.Application.Interfaces
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoDto>> ObterTodosAsync();
        Task<AlunoDto> ObterPorIdAsync(int id);
        Task AdicionarAsync(AlunoDto alunoDto);
        Task AtualizarAsync(AlunoDto alunoDto);
        Task RemoverAsync(int id);
    }
}
