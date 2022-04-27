using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // [Required], [StringLength]
using System.ComponentModel.DataAnnotations.Schema; // [Column]

namespace gestorpredialsys.entidades
{
    [Table("morador")]
    public class Morador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        
        [Column("id_familia")]
        public int Id_familia { get; set;}

        [Column("nome")]
        [StringLength(50)]
        public string? Nome { get; set; }

        [Column("idade")]

        public int Idade { get; set; }


    }
}
