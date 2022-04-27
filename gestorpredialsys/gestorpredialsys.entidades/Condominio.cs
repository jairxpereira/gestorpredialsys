using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace gestorpredialsys.entidades
{
    [Table("condominio")]
    public class Condominio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string? Nome { get; set; }
        [Column("bairro")]
        [StringLength(50)]
        public string? Bairro { get; set; }
           
        [Column("area_total", TypeName = "float")]
        public float? Area_total {get; set; }

        [Column("valor_iptu", TypeName = "money")]
        public decimal? Valor_iptu { get; set; }
    }
}
