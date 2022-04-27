using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace gestorpredialsys.entidades
{
    [Table("familia")]
    public class Familia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("nome")]
        [StringLength(50)]
        public string? Nome { get; set; }
        
        [Column("id_condominio")]
        public int Id_condominio { get; set; }
        
        [Column("apto")]
        public int Apto { get; set; }

        [Column("area_apto", TypeName = "float")]
        public float? Area_apto { get; set; }

        [Column("fracao_ideal", TypeName = "float")]
        public float? Fracao_ideal { get; set; }

        [Column("valor_iptu_prop", TypeName = "money")]
        public decimal? Valor_iptu_prop { get; set; }

    }
}
