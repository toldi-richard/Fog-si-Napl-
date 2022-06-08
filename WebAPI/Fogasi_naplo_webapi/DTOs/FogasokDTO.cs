using Fogasi_naplo_webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fogasi_naplo_webapi.DTOs
{
    public class FogasokDTO
    {
        public int fogasID { get; set; }
        public int azonosito { get; set; }
        public string helyszin { get; set; }
        public string horgaszhely { get; set; }
        public string datum_idopont { get; set; }
        public string halfaj { get; set; }
        public float suly { get; set; }

        public FogasokDTO(int fogasID, int azonosito, string vizterulet_neve,
            string horgaszhely, string datum_idopont, string halfaj,
            float suly)
        {
            this.fogasID = fogasID;
            this.azonosito = azonosito;
            this.helyszin = vizterulet_neve;
            this.horgaszhely = horgaszhely;
            this.datum_idopont = datum_idopont;
            this.halfaj = halfaj;
            this.suly = suly;
        }
    }
}
