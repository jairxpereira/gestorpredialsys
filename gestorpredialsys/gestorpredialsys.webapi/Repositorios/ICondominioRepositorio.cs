using gestorpredialsys.entidades; // Condominio
namespace gestorpredialsys.webapi.Repositorios;
public interface ICondominioRepositorio
{
    Task<Condominio?> criarCondominioAsync(Condominio m);
    Task<IEnumerable<Condominio>> obterCondominiosTodosAsync();
    Task<Condominio?> obterCondominioAsync(int id);
    Task<Condominio?> atualizarCondominioAsync(int id, Condominio cond);
    Task<bool?> deletarCondominioAsync(int id);
}
    
