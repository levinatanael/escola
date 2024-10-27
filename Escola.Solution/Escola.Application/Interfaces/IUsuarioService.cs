using Escola.Application.DTOs;

namespace Escola.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> AutenticarAsync(LoginDto loginDto);
    }
}