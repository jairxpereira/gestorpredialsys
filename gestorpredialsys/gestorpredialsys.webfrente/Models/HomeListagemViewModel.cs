using gestorpredialsys.entidades; // Morador, Condominio, Familia
namespace gestorpredialsys.webfrente.Models;
public record HomeListagemViewModel
(
IList<Morador> Moradores,
IList<Condominio> Condominios,
IList<Familia> Familias
);