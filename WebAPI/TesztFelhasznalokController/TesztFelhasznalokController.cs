using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesztFelhasznalokController
{
    [TestClass]
    public class TesztFelhasznalokController
    {
          private FelhasznalokController controller = new FelhasznalokController(new FogasiNaploContext());

        private Felhasznalo GetPeldaFelhasznalo()
        {
            return new Felhasznalo()
            {
                azonosito = 111111,
                szerepkorID = 3,
                email_cim = "Agoston1@gmail.com",
                jelszo = "Teszt"
            };
        }

        private Felhasznalo CreateTesztFelhasznalo()
        {
            return new Felhasznalo()
            {
                azonosito = 999999,
                szerepkorID = 3,
                email_cim = "Teszt@gmail.com",
                jelszo = "Teszt"
            };
        }

        [TestMethod]
        // GET: api/Felhasznalok, Darabsz�m ellen�rz�se
        public async Task GetFelhasznalok_DarabszamMegfelel()
        {
            // Http Response �zenet
            var response = await controller.GetUsers();

            // List�v� alak�tjuk
            var result = response.Value as List<Felhasznalo>;

            Assert.IsNotNull(result);
            int szamlalo = 13; // Ennyi felhaszn�l� van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);

        }

        // GET: api/Felhasznalok/111111, V�lasz email ellen�rz�se
        [TestMethod]
        public async Task GetFelhasznalo_ValaszUgyanazonEmaillel()
        {
            // Ez lesz az �sszehasonl�t�si alap
            var felhasznalo = GetPeldaFelhasznalo();
            var response = await controller.GetUser(111111);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.email_cim, felhasznalo.email_cim);
        }

        // POST: api/Felhasznalok, Konfliktus ellen�rz�se
        [TestMethod]
        public async Task PostFelhasznalo_KonfliktusValasszal()
        {
            var felhasznalo = GetPeldaFelhasznalo();
            var response = await controller.PostUser(felhasznalo);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // T�pus ellen�rz�s a p�ld�nynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict st�tuszk�d �sszehasonl�t�s
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Felhasznalok, Ugyanazt a v�lasz�rt�ket kapjuk vissza
        [TestMethod]
        public async Task PostFelhasznalo_UgyanazonErtekkel()
        {
            var felhasznalo = CreateTesztFelhasznalo();

            // El�sz�r l�trehozzuk a felhasznalot
            var response = await controller.PostUser(felhasznalo);
            var result = response.Result as CreatedAtActionResult;

            // A v�lasz �ltal visszaadott felhaszn�l�
            var valaszFelhasznalo = result.Value as Felhasznalo;
            Assert.IsNotNull(result);

            // Melyik �tvonalon lehet majd lek�rdezni
            Assert.AreEqual(result.ActionName, "GetFelhasznalo");

            // A GetUser/{id} �tvonalon lesz el�rhet�
            Assert.AreEqual(result.RouteValues["id"], valaszFelhasznalo.azonosito);

            // A l�trehozott felhaszn�l� email c�me megegyezik, az elk�ld�ttel
            Assert.AreEqual(valaszFelhasznalo.email_cim, felhasznalo.email_cim);

            // T�r�lj�k az adatb�zisb�l
            await controller.DeleteUser(valaszFelhasznalo.azonosito);
        }

        //PUT: api/Felhasznalok/999999, Hiba elt�r� azonos�t� eset�n
        [TestMethod]
        public async Task PutFelhasznalok_HibaElteroIDnal()
        {
            // 999999-es azonos�t�-ra k�ld�m a 111111-es azonos�t�-j� tan�r adatait
            var response = await controller.PutUser(999999, GetPeldaFelhasznalo());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvert�l�s is t�rt�nik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Felhasznalok/id, St�tuszk�d v�lasszal
        [TestMethod]
        public async Task DeletFelhasznalo_StatuszKodValasszal()
        {
            var felhasznalo = CreateTesztFelhasznalo();
            var postResponse = await controller.PostUser(felhasznalo);
            var postResult = postResponse.Result as CreatedAtActionResult;
            var valaszFelhasznalo = postResult.Value as Felhasznalo;

            var response = await controller.DeleteUser(valaszFelhasznalo.azonosito);
            var result = response as StatusCodeResult;

            Assert.IsNotNull(result);
            // 204 NoContent st�tuszk�d ellen�rz�se 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
