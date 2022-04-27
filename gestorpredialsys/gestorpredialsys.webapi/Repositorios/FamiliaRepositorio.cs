using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using gestorpredialsys.entidades; // Morador


namespace gestorpredialsys.webapi.Repositorios;

public class FamiliaRepositorio : IFamiliaRepositorio
{

    // use uma instância do contexto de dados porque ele não deve ser
    // cacheado devido ao seu cacheamento interno.
    private DbgestaopredialContexto db;

    public FamiliaRepositorio(DbgestaopredialContexto contextoInjetado)
    {
        db = contextoInjetado;

    }

    public async Task<Familia?> criarFamiliaAsync(Familia fam)
    {

        // Adiciona família usando entity framework
        EntityEntry<Familia> familiaAdicionada = await db.Familias.AddAsync(fam);
        int afetado = await db.SaveChangesAsync();
        if (afetado == 1)
        {

            return fam;
        }
        else
        {
            return null;
        }
    }

    public Task<IEnumerable<Familia>> obterFamiliasTodasAsync()
    {
        IEnumerable<Familia>? familias = db.Familias;
        return Task.FromResult(familias);

    }

    public Task<Familia?> obterFamiliaAsync(int id)
    {

        Familia familia = db.Familias.Where(fam => fam.Id == id).FirstOrDefault();
        return Task.FromResult(familia);
    }

    public async Task<Familia?> atualizarFamiliaAsync(int id, Familia fam)
    {

        // Atualizar no banco de dados - *** essa função falhou ***
        fam.Id = id;
        db.Entry(fam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        db.Familias.Update(fam);
        int afetado = await db.SaveChangesAsync();
        if (afetado == 1)
        {
            return fam;
        }

        return null;
    }

    public async Task<bool?> deletarFamiliaAsync(int id)
    {

        // remova do banco de dados
        Familia? fam = db.Familias.Find(id);
        if (fam is null) return null;
        db.Familias.Remove(fam);
        int afetado = await db.SaveChangesAsync();
        if (afetado == 1)
        {
            return true;
        }
        else
        {
            return null;
        }
    }
}
