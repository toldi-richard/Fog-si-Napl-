using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Fogasi_naplo_webapi.Models
{
    [Index(nameof(email_cim), Name = "email_cim", IsUnique = true)]
    [Index(nameof(szerepkorID), Name = "szerepkorID")]
    [Table("felhasznalok")]
    public partial class Felhasznalo 
    {
        public Felhasznalo()
        {
            fogasok = new HashSet<Fogas>();
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
        [InverseProperty(nameof(Szerepkor.felhasznalok))]
        public virtual Szerepkor szerepkor { get; set; }

        [InverseProperty("azonositoNavigation")]
        [JsonIgnore]
        public virtual ICollection<Fogas> fogasok { get; set; }
    }
}
