using Microsoft.EntityFrameworkCore; // UseSqlite
using Microsoft.Extensions.DependencyInjection; // IServiceCollection
namespace gestorpredialsys.entidades;
public static class DbgestorpredialContextoExtensao
{
 /// <summary>
 /// Adiciona o DbgestorpredialContexto no especificado IServiceCollection. Usa o provedor SqlServer
 /// Isto é para o processo de dependency injection na aplicação web api
/// </summary>
/// <param name="services"></param>
/// <param name="connection">Configura para modificar o valor default de ".."</param>
/// <returns>Retorna um IServiceCollection que pode ser usado para adicionar mais serviços</returns>
public static IServiceCollection AddDbgestaopredialContexto(
this IServiceCollection services, string connection = "..")
    {
        // A intenção da string connection é permitir carregar a string de conexão de um
        // arquivo de configuração
         
        connection = "Data Source=.;" +
        "Initial Catalog=dbgestaopredial;" +
        "Integrated Security=true;Encrypt=false;" +
        "MultipleActiveResultSets=true;";
        services.AddDbContext<DbgestaopredialContexto>(options =>
        options.UseSqlServer(connection));
        
        return services;
    }
}