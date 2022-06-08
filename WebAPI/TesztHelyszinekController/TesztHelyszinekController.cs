using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesztFelhasznalokController
{
    [TestClass]
    public class TesztFogasokController
    {
        private HelyszinekController controller = new HelyszinekController(new FogasiNaploContext());

        private Helyszin GetPeldaHelyszin()
        {
            return new Helyszin()
            {
                helyszinID = 1,
                vizterulet_neve = "K�r�gyi csatorna",
                vizterkod = 600111,    
            };
        }

        private Helyszin CreateTesztHelyszin()
        {
            return new Helyszin()
            {
                helyszinID = 999,
                vizterulet_neve = "Teszt",
                vizterkod = 999999,
            };
        }

        [TestMethod]
        // GET: api/Helyszinek, Darabsz�m ellen�rz�se
        public async Task GetHelyszinek_DarabszamMegfelel()
        {
            // Http Response �zenet
            var response = await controller.GetPlaces();

            // List�v� alak�tjuk
            var result = response.Value as List<Helyszin>;

            Assert.IsNotNull(result);
            int szamlalo = 13; // Ennyi helyszin van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);
        }

        // GET: api/Helszinek/1, V�lasz vizterkod ellen�rz�se
        [TestMethod]
        public async Task GetHelyszin_ValaszUgyanazonAzonositoval()
        {
            // Ez lesz az �sszehasonl�t�si alap
            var fogas = GetPeldaHelyszin();
            var response = await controller.GetPlace(1);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.vizterkod, fogas.vizterkod);
        }


        // POST: api/Helszinek, Konfliktus ellen�rz�se
        [TestMethod]
        public async Task PostHelyszin_KonfliktusValasszal()
        {
            var helyszin = GetPeldaHelyszin();
            var response = await controller.PostPlace(helyszin);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // T�pus ellen�rz�s a p�ld�nynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict st�tuszk�d �sszehasonl�t�s
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Helyszinek, Ugyanazt a v�lasz�rt�ket kapjuk vissza
        [TestMethod]
        public async Task PostHelyszin_UgyanazonErtekkel()
        {
            var helyszin = CreateTesztHelyszin();

            // El�sz�r l�trehozzuk a helyszint
            var response = await controller.PostPlace(helyszin);
            var result = response.Result as CreatedAtActionResult;

            // A v�lasz �ltal visszaadott fog�s
            var valaszHelyszin = result.Value as Helyszin;
            Assert.IsNotNull(result);

            // Melyik �tvonalon lehet majd lek�rdezni
            Assert.AreEqual(result.ActionName, "GetHelyszin");

            // A GetHelyszin/{id} �tvonalon lesz el�rhet�
            Assert.AreEqual(result.RouteValues["id"], valaszHelyszin.helyszinID);

            // A l�trehozott helyszin vizterkodja megegyezik, az elk�ld�ttel
            Assert.AreEqual(valaszHelyszin.vizterkod, helyszin.vizterkod);

            // T�r�lj�k az adatb�zisb�l
            await controller.DeletePlace(valaszHelyszin.helyszinID);
        }

        //PUT: api/Helyszinek/111, Hiba elt�r� azonos�t� eset�n
        [TestMethod]
        public async Task PutHelyszin_HibaElteroIDnal()
        {
            // 111-es id-ra k�ld�m a 1-es id-j� helysz�n adatait
            var response = await controller.PutPlace(111, GetPeldaHelyszin());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvert�l�s is t�rt�nik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Helyszinek/id, St�tuszk�d v�lasszal
        [TestMethod]
        public async Task DeletHelyszin_StatuszKodValasszal()
        {
            var helyszin = CreateTesztHelyszin();
            var postResponse = await controller.PostPlace(helyszin);
            var postResult = postResponse.Result as CreatedAtActionResult;
            var valaszHelyszin = postResult.Value as Helyszin;

            var response = await controller.DeletePlace(valaszHelyszin.helyszinID);
            var result = response as StatusCodeResult;

            Assert.IsNotNull(result);
            // 204 NoContent st�tuszk�d ellen�rz�se 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
