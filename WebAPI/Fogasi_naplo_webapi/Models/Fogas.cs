using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Fogasi_naplo_webapi.Models
{
    [Index(nameof(datum_idopont), nameof(azonosito), nameof(halfaj), nameof(suly), Name = "datum_idopont", IsUnique = true)]
    [Index(nameof(helyszinID), Name = "helyszinID")]
    [Table("fogasok")]
    public partial class Fogas
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int fogasID { get; set; }
        [Column(TypeName = "int(11)")]
        public int azonosito { get; set; }
        [Column(TypeName = "int(11)")]
        public int helyszinID { get; set; }
        [StringLength(10)]
        public string horgaszhely { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime datum_idopont { get; set; }
        [StringLength(15)]
        public string halfaj { get; set; }
        public float suly { get; set; }

        [ForeignKey(nameof(azonosito))]
        [InverseProperty(nameof(Felhasznalo.fogasok))]
        public virtual Felhasznalo azonositoNavigation { get; set; }
        [ForeignKey(nameof(helyszinID))]
        [InverseProperty(nameof(Helyszin.fogasok))]
        [JsonIgnore]
        public virtual Helyszin helyszin { get; set; }
    }
}
