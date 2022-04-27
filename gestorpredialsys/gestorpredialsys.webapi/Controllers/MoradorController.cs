using Microsoft.AspNetCore.Mvc; // [Route], [ApiController], ControllerBase
using gestorpredialsys.entidades; // Morador
using gestorpredialsys.webapi.Repositorios; // IMoradorRepositorio

namespace gestorpredialsys.webapi.Controllers;

// endereço base: api/Morador
[Route("api/[controller]")]
[ApiController]
public class MoradorController : ControllerBase
{
    private readonly IMoradorRepositorio repo;

    
    // o construtor injeta o repositório registrado na inicialização
    public MoradorController(IMoradorRepositorio repo)
    {
        this.repo = repo;
    }

    // GET: api/Morador
    // Isso vai sempre retornar uma lista de Moradores (mas pode estar vazia)
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Morador>))]
    public async Task<IEnumerable<Morador>> GetMoradores()
    {
       
            return await repo.obterMoradoresTodosAsync();
        
    }

    // GET: api/Morador/[id]
    [HttpGet("{id}", Name = nameof(GetMorador))] // rota nomeada
    [ProducesResponseType(200, Type = typeof(Morador))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetMorador(int id)
    {
        Morador? m = await repo.obterMoradorAsync(id);
        if (m == null)
        {
            return NotFound(); // 404 Resource not found
        }
        return Ok(m); // 200 OK com Morador no corpo
    }

    // POST: api/Morador
    // BODY: Morador (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Morador))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Morador m)
    {
        if (m == null)
        {
            return BadRequest(); // 400 Bad request
        }

        Morador? moradorAdicionado = await repo.criarMoradorAsync(m);

        if (moradorAdicionado == null)
        {
            return BadRequest("Repositorio falhou em criar o Morador.");
        }
        else
        {
            return CreatedAtRoute( // 201 Created
              routeName: nameof(GetMorador),
              routeValues: new { id = moradorAdicionado.Id },
              value: moradorAdicionado);
        }
    }

    // PUT: api/Morador/[id]
    // BODY: Morador (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
      int id, [FromBody] Morador m)
    {
        
        if (m == null || m.Id != id)
        {
            return BadRequest(); // 400 Bad request
        }

        Morador? existente = await repo.obterMoradorAsync(id);
        if (existente == null)
        {
            return NotFound(); // 404 Resource not found
        }

        await repo.atualizarMoradorAsync(id, m);

        return new NoContentResult(); // 204 No content
    }

    // DELETE: api/Moradors/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        
        Morador? existente = await repo.obterMoradorAsync(id);
        if (existente == null)
        {
            return NotFound(); // 404 Resource not found
        }

        bool? deletado = await repo.deletarMoradorAsync(id);

        if (deletado.HasValue && deletado.Value) // Se tiver um valor é que deletou!
        {
            return new NoContentResult(); // 204 No content
        }
        else
        {
            return BadRequest( // 400 Bad request
              $"Morador {id} foi encontrado mas falhou em ser deletado.");
        }
    }
}
