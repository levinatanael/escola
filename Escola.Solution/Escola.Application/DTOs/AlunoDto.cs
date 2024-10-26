using Swashbuckle.AspNetCore.Annotations;

namespace Escola.Application.DTOs
{
    public class AlunoDto
    {
        [SwaggerSchema(Description = "Código único do aluno")]
        public int Id { get; set; }

        [SwaggerSchema(Description = "Nome completo do aluno")]
        public string Nome { get; set; }

        [SwaggerSchema(Description = "Data de nascimento do aluno")]
        public DateTime DataNascimento { get; set; }

        [SwaggerSchema(Description = "Número do registro acadêmico do aluno")]
        public string RA { get; set; }
    }
}