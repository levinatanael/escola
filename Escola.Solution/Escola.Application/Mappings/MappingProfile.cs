using AutoMapper;
using Escola.Application.DTOs;
using Escola.Domain.Entities;

namespace Escola.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Aluno, AlunoDto>().ReverseMap();
        }
    }
}