using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FogasiNaploAsztaliAlkalmazas.Models
{
    [Index(nameof(datum_idopont), nameof(azonosito), nameof(halfaj), nameof(suly), Name = "datum_idopont", IsUnique = true)]
    [Index(nameof(azonosito), Name = "fogasok_ibfk_1")]
    [Index(nameof(helyszinID), Name = "helyszinID")]
    public partial class Fogasok
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
        public float? suly { get; set; }

        [ForeignKey(nameof(azonosito))]
        [InverseProperty(nameof(Felhasznalok.fogasok))]
        public virtual Felhasznalok azonositoNavigation { get; set; }
        [ForeignKey(nameof(helyszinID))]
        [InverseProperty(nameof(Helyszinek.fogasok))]
        public virtual Helyszinek helyszin { get; set; }
    }
}
