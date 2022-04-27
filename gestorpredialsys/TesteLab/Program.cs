// Este aplicativo de console é para testar componentes do Sistema Gestor predial (gestorpredialsys)
using gestorpredialsys.entidades;
using static System.Console;

WriteLine($"Provedor de banco de dados: {ConstantesProjeto.DatabaseProvider} ");

// consultar_moradores();
// consultar_familias();
// consultar_condominios();


Console.WriteLine( rateio_iptu(10));
Console.WriteLine(rateio_iptu(45));
Console.WriteLine(rateio_iptu(110));
Console.WriteLine(rateio_iptu(400));


static void consultar_moradores()
{
    using (DbgestaopredialContexto db = new())
    {

        // uma consulta para obter todos os moradores
        WriteLine("\n --- Moradores --- ");
        IQueryable<Morador>? moradores = db.Moradores;

        foreach (Morador morador in moradores.ToArray())
        {
            Console.WriteLine("{0} {1} id_familia({2}) idade({3})", 
                morador.Id, morador.Nome, morador.Id_familia, morador.Idade);
        }



    }
}

static void consultar_familias()
{
    using (DbgestaopredialContexto db = new())
    {

        // uma consulta para obter todas as familias
        WriteLine("\n --- Famílias --- ");
        IQueryable<Familia>? familias = db.Familias;

        foreach (Familia fam in familias.ToArray())
        {
            Console.WriteLine("{0} {1} - id_condominio({2}) - apto({3})" +
                "area_apto: {4} fracao_ideal:{5} valor_iptu_prop: {6}",
                fam.Id, fam.Nome, fam.Id_condominio, fam.Apto,
                fam.Area_apto, fam.Fracao_ideal, fam.Valor_iptu_prop);
        }

    }
}


static void consultar_condominios()
{
    using (DbgestaopredialContexto db = new())
    {

        // uma consulta para obter todos os condomínios
        WriteLine("\n --- Condomínios --- ");
        IQueryable<Condominio>? condominios = db.Condominios;

        foreach (Condominio cond in condominios.ToArray())
        {
            Console.WriteLine("{0} {1} - bairro ({2}) - area_total:{3} - valor_iptu: {4}",
                cond.Id, cond.Nome, cond.Bairro, cond.Area_total, cond.Valor_iptu);
        }

    }
}

static decimal? rateio_iptu( int id_apto)
{
    // Para cálculo da fração ideal
    float? fracao_ideal = 0.0F;
    float? area_apto = 0.0F;
    float? area_total_cond = 0.0F;
    
    
    // Para cáculo do iptu do apto
    decimal? valor_iptu_prop = 0M;
    decimal? valor_iptu_cond = 0M;

    // Vamos pegar dados do banco de dados
    try
    {

        using (DbgestaopredialContexto db = new())
        {

            IQueryable<Familia>? familias = db.Familias;
            IQueryable<Condominio>? condominios = db.Condominios;

            Familia? fam = familias.Where(f => f.Apto == id_apto).FirstOrDefault();
            Condominio? cond = condominios.Where(c => c.Id == fam.Id_condominio).FirstOrDefault();

            area_apto = fam.Area_apto;
            area_total_cond = cond.Area_total;
            valor_iptu_cond = cond.Valor_iptu;

        }

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return -1M;
    }
    
    fracao_ideal = (area_apto * 100) / area_total_cond;
    valor_iptu_prop = (valor_iptu_cond * (decimal?) fracao_ideal) / 100;
    return valor_iptu_prop;
    }