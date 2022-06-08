using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Fogasi_naplo_webapi.Models
{
    [Table("szerepkorok")]
    public partial class Szerepkor
    {
        public Szerepkor()
        {
            felhasznalok = new HashSet<Felhasznalo>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int szerepkorID { get; set; }
        [Required]
        [StringLength(20)]
        public string szerepkor_megnev { get; set; }

        [InverseProperty("szerepkor")]
        [JsonIgnore]
        public virtual ICollection<Felhasznalo> felhasznalok { get; set; }
    }
}
