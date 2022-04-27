using Microsoft.AspNetCore.Mvc; // [Route], [ApiController], ControllerBase
using gestorpredialsys.entidades; // Familia
using gestorpredialsys.webapi.Repositorios; // IMoradorRepositorio
namespace gestorpredialsys.webapi.Controllers;

// endereço base: api/Familia
[Route("api/[controller]")]
[ApiController]
public class FamiliaController : ControllerBase
{
    private readonly IFamiliaRepositorio repo;


    // o construtor injeta o repositório registrado na inicialização
    public FamiliaController(IFamiliaRepositorio repo)
    {
        this.repo = repo;
    }

    // GET: api/Familia
    // Isso vai sempre retornar uma lista de Familias (mas pode estar vazia)
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Morador>))]
    public async Task<IEnumerable<Familia>> GetFamilias()
    {

        return await repo.obterFamiliasTodasAsync();

    }

    // GET: api/Familia/[id]
    [HttpGet("{id}", Name = nameof(GetFamilia))] // rota nomeada
    [ProducesResponseType(200, Type = typeof(Familia))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFamilia(int id)
    {
        Familia? fam = await repo.obterFamiliaAsync(id);
        if (fam == null)
        {
            return NotFound(); // 404 Resource not found
        }
        return Ok(fam); // 200 OK com Morador no corpo
    }

    // POST: api/Familia
    // BODY: Familia (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Familia))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Familia fam)
    {
        if (fam == null)
        {
            return BadRequest(); // 400 Bad request
        }

        Familia? familiaAdicionada = await repo.criarFamiliaAsync(fam);

        if (familiaAdicionada == null)
        {
            return BadRequest("Repositorio falhou em criar a Familia.");
        }
        else
        {
            return CreatedAtRoute( // 201 Created
              routeName: nameof(GetFamilia),
              routeValues: new { id = familiaAdicionada.Id },
              value: familiaAdicionada);
        }
    }

    // PUT: api/Familia/[id]
    // BODY: Familia (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
      int id, [FromBody] Familia fam)
    {

        if (fam == null || fam.Id != id)
        {
            return BadRequest(); // 400 Bad request
        }

        Familia? existente = await repo.obterFamiliaAsync(id);
        if (existente == null)
        {
            return NotFound(); // 404 Resource not found
        }

        await repo.atualizarFamiliaAsync(id, fam);

        return new NoContentResult(); // 204 No content
    }

    // DELETE: api/Familia/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {

        Familia? existente = await repo.obterFamiliaAsync(id);
        if (existente == null)
        {
            return NotFound(); // 404 Resource not found
        }

        bool? deletado = await repo.deletarFamiliaAsync(id);

        if (deletado.HasValue && deletado.Value) // Se tiver um valor é que deletou!
        {
            return new NoContentResult(); // 204 No content
        }
        else
        {
            return BadRequest( // 400 Bad request
              $"Familia {id} foi encontrada mas falhou em ser deletada.");
        }
    }
}
