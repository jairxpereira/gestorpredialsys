using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using gestorpredialsys.entidades; // Condominio


namespace gestorpredialsys.webapi.Repositorios;

public class CondominioRepositorio : ICondominioRepositorio
{

    // use uma instância do contexto de dados porque ele não deve ser
    // cacheado devido ao seu cacheamento interno.
    private DbgestaopredialContexto db;

    public CondominioRepositorio(DbgestaopredialContexto contextoInjetado)
    {
        db = contextoInjetado;

    }

    public async Task<Condominio?> criarCondominioAsync(Condominio cond)
    {

        // Adiciona morador usando entity framework
        EntityEntry<Condominio> condAdicionado = await db.Condominios.AddAsync(cond);
        int afetado = await db.SaveChangesAsync();
        if (afetado == 1)
        {

            return cond;
        }
        else
        {
            return null;
        }
    }

    public Task<IEnumerable<Condominio>> obterCondominiosTodosAsync()
    {
        IEnumerable<Condominio>? condominios = db.Condominios;
        return Task.FromResult(condominios);

    }

    public Task<Condominio?> obterCondominioAsync(int id)
    {

        Condominio condominio = db.Condominios.Where(cond => cond.Id == id).FirstOrDefault();
        return Task.FromResult(condominio);
    }

    public async Task<Condominio?> atualizarCondominioAsync(int id, Condominio cond)
    {

        // Atualizar no banco de dados - *** essa função falhou no repositório de moradores***
        Condominio existente = await obterCondominioAsync(1);
        existente.Area_total = cond.Area_total;
        existente.Bairro = cond.Bairro;
        existente.Valor_iptu = cond.Valor_iptu;
        existente.Nome = cond.Nome;        

        db.Entry(existente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        
        int afetado = await db.SaveChangesAsync();
        if (afetado == 1)
        {
            return cond;
        }

        return null;
    }

    public async Task<bool?> deletarCondominioAsync(int id)
    {

        // remova do banco de dados
        Condominio? cond = db.Condominios.Find(id);
        if (cond is null) return null;
        db.Condominios.Remove(cond);
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
