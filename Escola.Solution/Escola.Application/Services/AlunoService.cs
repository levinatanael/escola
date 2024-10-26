using AutoMapper;
using Escola.Application.DTOs;
using Escola.Application.Interfaces;
using Escola.Domain.Entities;
using Escola.Domain.Interfaces.Repositories;

namespace Escola.Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;

        public AlunoService(IAlunoRepository alunoRepository, IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlunoDto>> ObterTodosAsync()
        {
            var alunos = await _alunoRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<AlunoDto>>(alunos);
        }

        public async Task<AlunoDto> ObterPorIdAsync(int id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id);
            return _mapper.Map<AlunoDto>(aluno);
        }

        public async Task AdicionarAsync(AlunoDto alunoDto)
        {
            var aluno = _mapper.Map<Aluno>(alunoDto);
            await _alunoRepository.AdicionarAsync(aluno);
        }

        public async Task AtualizarAsync(AlunoDto alunoDto)
        {
            var aluno = _mapper.Map<Aluno>(alunoDto);
            await _alunoRepository.AtualizarAsync(aluno);
        }

        public async Task RemoverAsync(int id)
        {
            await _alunoRepository.RemoverAsync(id);
        }
    }
}