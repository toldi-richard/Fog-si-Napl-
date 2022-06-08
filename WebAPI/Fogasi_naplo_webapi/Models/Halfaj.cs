using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fogasi_naplo_webapi.Models
{
    // Egység teszteknek létrehozott osztály
    public class Halfaj
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string LatinNev { get; set; }
        public int Elettartam { get; set; }
    }
}
