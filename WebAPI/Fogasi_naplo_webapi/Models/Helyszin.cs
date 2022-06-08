using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Fogasi_naplo_webapi.Models
{
    [Index(nameof(vizterkod), Name = "vizterkod", IsUnique = true)]
    [Index(nameof(vizterulet_neve), Name = "vizterulet_neve", IsUnique = true)]
    [Table("helyszinek")]
    public partial class Helyszin
    {
        public Helyszin()
        {
            fogasok = new HashSet<Fogas>();
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
        [JsonIgnore]
        public virtual ICollection<Fogas> fogasok { get; set; }
    }
}
