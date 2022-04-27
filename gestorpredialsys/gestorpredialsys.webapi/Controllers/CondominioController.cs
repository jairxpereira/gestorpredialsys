using Microsoft.AspNetCore.Mvc; // [Route], [ApiController], ControllerBase
using gestorpredialsys.entidades; // Morador
using gestorpredialsys.webapi.Repositorios; // IMoradorRepositorio

namespace gestorpredialsys.webapi.Controllers;

// endereço base: api/Condominio
[Route("api/[controller]")]
[ApiController]
public class CondominioController : ControllerBase
{
    private readonly ICondominioRepositorio repo;


    // o construtor injeta o repositório registrado na inicialização
    public CondominioController(ICondominioRepositorio repo)
    {
        this.repo = repo;
    }

    // GET: api/Condominio
    // Isso vai sempre retornar uma lista de Condominios (mas pode estar vazia)
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Condominio>))]
    public async Task<IEnumerable<Condominio>> GetCondominios()
    {

        return await repo.obterCondominiosTodosAsync();

    }

    // GET: api/Condominio/[id]
    [HttpGet("{id}", Name = nameof(GetCondominio))] // rota nomeada
    [ProducesResponseType(200, Type = typeof(Condominio))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCondominio(int id)
    {
        Condominio? cond = await repo.obterCondominioAsync(id);
        if (cond == null)
        {
            return NotFound(); // 404 Resource not found
        }
        return Ok(cond); // 200 OK com Condominio no corpo
    }

    // POST: api/Condominio
    // BODY: Condominio (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Condominio))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Condominio cond)
    {
        if (cond == null)
        {
            return BadRequest(); // 400 Bad request
        }

        Condominio? condAdicionado = await repo.criarCondominioAsync(cond);

        if (condAdicionado == null)
        {
            return BadRequest("Repositorio falhou em criar o Condominio.");
        }
        else
        {
            return CreatedAtRoute( // 201 Created
              routeName: nameof(GetCondominio),
              routeValues: new { id = condAdicionado.Id },
              value: condAdicionado);
        }
    }

    // PUT: api/Condominio/[id]
    // BODY: Condominio (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
      int id, [FromBody] Condominio cond)
    {

        if (cond == null || cond.Id != id)
        {
            return BadRequest(); // 400 Bad request
        }

        Condominio? existente = await repo.obterCondominioAsync(id);
        if (existente == null)
        {
            return NotFound(); // 404 Resource not found
        }

        await repo.atualizarCondominioAsync(id, cond);

        return new NoContentResult(); // 204 No content
    }

    // DELETE: api/Condominio/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {

        Condominio? existente = await repo.obterCondominioAsync(id);
        if (existente == null)
        {
            return NotFound(); // 404 Resource not found
        }

        bool? deletado = await repo.deletarCondominioAsync(id);

        if (deletado.HasValue && deletado.Value) // Se tiver um valor é que deletou!
        {
            return new NoContentResult(); // 204 No content
        }
        else
        {
            return BadRequest( // 400 Bad request
              $"Condominio {id} foi encontrado mas falhou em ser deletado.");
        }
    }
}
