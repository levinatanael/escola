using Escola.Application.DTOs;
using Escola.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Escola.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém a lista de alunos cadastrado")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlunoDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<IEnumerable<AlunoDto>>> ObterTodos()
        {
            var alunos = await _alunoService.ObterTodosAsync();
            return Ok(alunos);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Obtém um aluno específico pelo Id")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AlunoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<AlunoDto>> ObterPorId(int id)
        {
            var aluno = await _alunoService.ObterPorIdAsync(id);
            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona um novo aluno")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AlunoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> Adicionar([FromBody] AlunoDto alunoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _alunoService.AdicionarAsync(alunoDto);
            return CreatedAtAction(nameof(ObterPorId), new { id = alunoDto.Id }, alunoDto);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Atualiza um aluno cadastrado")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AlunoDto alunoDto)
        {
            if (id != alunoDto.Id)
                return BadRequest("O ID do aluno não corresponde ao ID na URL.");

            await _alunoService.AtualizarAsync(alunoDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Remove um aluno cadastrado")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> Remover(int id)
        {
            var aluno = await _alunoService.ObterPorIdAsync(id);
            if (aluno == null)
                return NotFound();

            await _alunoService.RemoverAsync(id);
            return NoContent();
        }
    }
}