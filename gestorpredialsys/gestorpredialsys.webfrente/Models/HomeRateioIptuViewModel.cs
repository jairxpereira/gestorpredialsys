using gestorpredialsys.entidades;
namespace gestorpredialsys.webfrente.Models
{
    public record HomeRateioIptuViewModel
    {
        public Decimal? Valor_iptu_prop;
        public Familia? familia;
        public Condominio? condominio;
        public string? mensagem;
    }
}
