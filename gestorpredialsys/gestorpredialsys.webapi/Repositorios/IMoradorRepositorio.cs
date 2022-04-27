using gestorpredialsys.entidades; // Morador
namespace gestorpredialsys.webapi.Repositorios;
public interface IMoradorRepositorio
{
    Task<Morador?> criarMoradorAsync(Morador m);
    Task<IEnumerable<Morador>> obterMoradoresTodosAsync();
    Task<Morador?> obterMoradorAsync(int id);
    Task<Morador?> atualizarMoradorAsync(int id, Morador m);
    Task<bool?> deletarMoradorAsync(int id);
}
