using gestorpredialsys.webfrente.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using gestorpredialsys.entidades; // DbgestaopredialContexto
using Microsoft.EntityFrameworkCore;

namespace gestorpredialsys.webfrente.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbgestaopredialContexto db;
        private readonly IHttpClientFactory clientFactory;
        public HomeController(ILogger<HomeController> logger,
          DbgestaopredialContexto injectedContext,
          IHttpClientFactory httpClientFactory)
        {

            _logger = logger;
            db = injectedContext;
            clientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
          
            _logger.LogInformation("*** gestorpredialsys.webfrente aplicação web mvc ***");

           return View();        

        }

        public async Task<IActionResult> Listagem()
        {
      
            HomeListagemViewModel model = new(
            Moradores: await db.Moradores.ToListAsync(),
            Condominios: await db.Condominios.ToListAsync(),
            Familias: await db.Familias.ToListAsync());
            return View(model); // pass model to view        

        }


        public async Task<IActionResult> Moradores()
        {
            string uri;
                ViewData["Title"] = "Moradores via webapi";
                uri = "api/morador/";
            
            HttpClient client = clientFactory.CreateClient(
            name: "gestorpredialsys.webapi");
            
            HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: uri);
            
            HttpResponseMessage response = await client.SendAsync(request);
            IEnumerable<Morador>? model = await response.Content.ReadFromJsonAsync<IEnumerable<Morador>>();
            return View(model);
        }


        public IActionResult Rateio_iptu( int apto )
        {
            HomeRateioIptuViewModel model = new();

            model.mensagem = "Iptu proporcional calculado com sucesso!";

            Decimal? valor_iptu_prop = fun_rateio_iptu(apto, model);

            if (valor_iptu_prop == -1)
            {
                model.mensagem = "Falha no cálculo do iptu. Verifique se o número do apartamento é válido.";
            }
                    
            
            return View(model);
        }
        public IActionResult Contato()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public decimal? fun_rateio_iptu(int id_apto, HomeRateioIptuViewModel? model)
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

                    model.familia = fam;
                    model.condominio = cond;
                                     
                    area_apto = fam.Area_apto;
                    area_total_cond = cond.Area_total;
                    valor_iptu_cond = cond.Valor_iptu;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                model.Valor_iptu_prop = -1M;
                return -1M;
            }

            fracao_ideal = (area_apto * 100) / area_total_cond;
            valor_iptu_prop = (valor_iptu_cond * (decimal?)fracao_ideal) / 100;
            model.Valor_iptu_prop = valor_iptu_prop;
            return valor_iptu_prop;
        }
    }
}