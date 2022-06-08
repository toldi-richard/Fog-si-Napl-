using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FogasiNaploAsztaliAlkalmazas.Models
{
    public partial class Szerepkorok
    {
        public Szerepkorok()
        {
            felhasznalok = new HashSet<Felhasznalok>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int szerepkorID { get; set; }
        [Required]
        [StringLength(20)]
        public string szerepkor_megnev { get; set; }

        [InverseProperty("szerepkor")]
        public virtual ICollection<Felhasznalok> felhasznalok { get; set; }
    }
}
