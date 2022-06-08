using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FogasiNaploAsztaliAlkalmazas.Models
{
    [Index(nameof(vizterkod), Name = "vizterkod", IsUnique = true)]
    [Index(nameof(vizterulet_neve), Name = "vizterulet_neve", IsUnique = true)]
    public partial class Helyszinek
    {
        public Helyszinek()
        {
            fogasok = new HashSet<Fogasok>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int helyszinID { get; set; }
        [Required]
        [StringLength(100)]
        public string vizterulet_neve { get; set; }
        [Column(TypeName = "int(11)")]
        public int vizterkod { get; set; }

        [InverseProperty("helyszin")]
        public virtual ICollection<Fogasok> fogasok { get; set; }
    }
}
