using gestorpredialsys.entidades; // Familia
namespace gestorpredialsys.webapi.Repositorios;
public interface IFamiliaRepositorio
{
    Task<Familia?> criarFamiliaAsync(Familia fam);
    Task<IEnumerable<Familia>> obterFamiliasTodasAsync();
    Task<Familia?> obterFamiliaAsync(int id);
    Task<Familia?> atualizarFamiliaAsync(int id, Familia fam);
    Task<bool?> deletarFamiliaAsync(int id);
}
