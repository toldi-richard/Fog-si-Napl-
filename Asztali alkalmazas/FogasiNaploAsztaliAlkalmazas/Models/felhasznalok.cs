using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FogasiNaploAsztaliAlkalmazas.Models
{
    [Index(nameof(email_cim), Name = "email_cim", IsUnique = true)]
    [Index(nameof(szerepkorID), Name = "szerepkorID")]
    public partial class Felhasznalok
    {
        public Felhasznalok()
        {
            fogasok = new HashSet<Fogasok>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int azonosito { get; set; }
        [Column(TypeName = "int(11)")]
        public int szerepkorID { get; set; }
        [Required]
        [StringLength(70)]
        public string jelszo { get; set; }
        [Required]
        [StringLength(50)]
        public string email_cim { get; set; }

        [ForeignKey(nameof(szerepkorID))]
        [InverseProperty(nameof(Szerepkorok.felhasznalok))]
        public virtual Szerepkorok szerepkor { get; set; }
        [InverseProperty("azonositoNavigation")]
        public virtual ICollection<Fogasok> fogasok { get; set; }
    }
}
