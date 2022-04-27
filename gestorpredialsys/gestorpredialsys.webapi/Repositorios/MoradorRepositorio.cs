using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using gestorpredialsys.entidades; // Morador


namespace gestorpredialsys.webapi.Repositorios;

public class MoradorRepositorio : IMoradorRepositorio
{

    // use uma instância do contexto de dados porque ele não deve ser
    // cacheado devido ao seu cacheamento interno.
        private DbgestaopredialContexto db;

    public MoradorRepositorio(DbgestaopredialContexto contextoInjetado)
    {
        db = contextoInjetado;
               
    }

    public async Task<Morador?> criarMoradorAsync(Morador m)
    {
   
        // Adiciona morador usando entity framework
        EntityEntry<Morador> moradorAdicionado = await db.Moradores.AddAsync(m);
        int afetado = await db.SaveChangesAsync();
        if (afetado == 1)
        {

            return m;
        }
        else
        {
            return null;
        }
    }

    public Task<IEnumerable<Morador>> obterMoradoresTodosAsync()
    {
        IEnumerable<Morador>? moradores = db.Moradores;
        return Task.FromResult(moradores);
          
    }

    public Task<Morador?> obterMoradorAsync(int id)
    {
       
        Morador morador = db.Moradores.Where (mor => mor.Id == id).FirstOrDefault();
        return Task.FromResult(morador);
    }

    public async Task<Morador?> atualizarMoradorAsync(int id, Morador m)
    {

        // Atualizar no banco de dados - *** essa função falhou ***
        db.Entry(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        db.Moradores.Update(m);       
        int afetado = await db.SaveChangesAsync();
        if (afetado == 1)
        {
             return m;
        }
        
        return null;
    }

    public async Task<bool?> deletarMoradorAsync(int id)
    {
        
        // remova do banco de dados
        Morador? m = db.Moradores.Find(id);
        if (m is null) return null;
        db.Moradores.Remove(m);
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
