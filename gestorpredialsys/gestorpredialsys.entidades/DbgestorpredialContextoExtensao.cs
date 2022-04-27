using Microsoft.EntityFrameworkCore; // UseSqlite
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
namespace gestorpredialsys.entidades;
public static class DbgestorpredialContextoExtensao
{
 /// <summary>
 /// Adiciona o DbgestorpredialContexto no especificado IServiceCollection. Usa o provedor SqlServer
 /// Isto é para o processo de dependency injection na aplicação frontal
/// </summary>
/// <param name="services"></param>
/// <param name="relativePath">Set to override the default of ".."</param>
/// <returns>An IServiceCollection that can be used to add more services.</returns>
public static IServiceCollection AddDbgestaopredialContexto(
this IServiceCollection services, string connection = "..")
    {
        
         connection = "Data Source=.;" +
        "Initial Catalog=dbgestaopredial;" +
        "Integrated Security=true;Encrypt=false;" +
        "MultipleActiveResultSets=true;";
        services.AddDbContext<DbgestaopredialContexto>(options =>
        options.UseSqlServer(connection)
        );
        return services;
    }
}