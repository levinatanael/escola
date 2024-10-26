using Escola.Domain.Entities;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Escola.Infra.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly EscolaDbContext _context;

        public AlunoRepository(EscolaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<Aluno> ObterPorIdAsync(int id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        public async Task AdicionarAsync(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var aluno = await ObterPorIdAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
        }
    }
}