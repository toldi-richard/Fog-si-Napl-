using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.DTOs;
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
        private FogasokController controller = new FogasokController(new FogasiNaploContext());

        private Fogas GetPeldaFogas()
        {
            return new Fogas()
            {
                fogasID = 1,
                azonosito = 111111,
                helyszinID = 9,
                datum_idopont = new DateTime(2022,03,17),
                halfaj = "Teszt",
                suly = 6
            };
        }

        private Fogas CreateTesztFogas()
        {
            return new Fogas()
            {
                fogasID = 9999,
                azonosito = 111111,
                helyszinID = 9,
                datum_idopont = new DateTime(2022, 03, 17),
                halfaj = "Teszt",
                suly = 6
            };
        }

        [TestMethod]
        // GET: api/Fogasok, Darabsz�m ellen�rz�se
        public async Task GetFogasok_DarabszamMegfelel()
        {
            // Http Response �zenet
            var response = await controller.GetCatches(0);

            // List�v� alak�tjuk
            var result = response.Value as List<FogasokDTO>;

            Assert.IsNotNull(result);
            int szamlalo = 67; // Ennyi fog�s van a DB-ben

            Assert.AreEqual(result.Count, szamlalo);
        }

        // GET: api/Fogasok/9999, V�lasz azonos�t� ellen�rz�se
        [TestMethod]
        public async Task GetFogas_ValaszUgyanazonAzonositoval()
        {
            // Ez lesz az �sszehasonl�t�si alap
            var fogas = GetPeldaFogas();
            var response = await controller.GetCatch(1);
            var result = response.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.azonosito, fogas.azonosito);
        }


        // POST: api/Fogasok, Konfliktus ellen�rz�se
        [TestMethod]
        public async Task PostFogas_KonfliktusValasszal()
        {
            var fogas = GetPeldaFogas();
            var response = await controller.PostCatch(fogas);
            var result = response.Result as ConflictObjectResult;

            Assert.IsNotNull(result);
            // T�pus ellen�rz�s a p�ld�nynak
            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            // 409-es, Conflict st�tuszk�d �sszehasonl�t�s
            Assert.AreEqual(result.StatusCode, 409);
        }

        // POST: api/Fogasok, Ugyanazt a v�lasz�rt�ket kapjuk vissza
        [TestMethod]
        public async Task PostFogas_UgyanazonErtekkel()
        {
            var fogas = CreateTesztFogas();

            // El�sz�r l�trehozzuk a fog�st
            var response = await controller.PostCatch(fogas);
            var result = response.Result as CreatedAtActionResult;

            // A v�lasz �ltal visszaadott fog�s
            var valaszFogas = result.Value as Fogas;
            Assert.IsNotNull(result);

            // Melyik �tvonalon lehet majd lek�rdezni
            Assert.AreEqual(result.ActionName, "GetFogas");

            // A GetCatch/{id} �tvonalon lesz el�rhet�
            Assert.AreEqual(result.RouteValues["id"], valaszFogas.fogasID);

            // A l�trehozott fogas halfaja megegyezik, az elk�ld�ttel
            Assert.AreEqual(valaszFogas.halfaj, fogas.halfaj);

            // T�r�lj�k az adatb�zisb�l
            await controller.DeleteCatch(valaszFogas.fogasID);
        }

        //PUT: api/Fogasok/111, Hiba elt�r� azonos�t� eset�n
        [TestMethod]
        public async Task PutFogasok_HibaElteroIDnal()
        {
            // 111-es id-ra k�ld�m a 1-es id-j� fog�s adatait
            var response = await controller.PutCatch(111, GetPeldaFogas());
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            // Ez ugyanaz mint a fenti, csak itt konvert�l�s is t�rt�nik
            var result = response as BadRequestResult;
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        //DELETE: api/Fogasok/id, St�tuszk�d v�lasszal
        [TestMethod]
        public async Task DeletFogas_StatuszKodValasszal()
        {
            var fogas = CreateTesztFogas();
            var postResponse = await controller.PostCatch(fogas);
            var postResult = postResponse.Result as CreatedAtActionResult;
            var valaszFogas = postResult.Value as Fogas;

            var response = await controller.DeleteCatch(valaszFogas.fogasID);
            var result = response as StatusCodeResult;

            Assert.IsNotNull(result);
            // 204 NoContent st�tuszk�d ellen�rz�se 
            Assert.AreEqual(result.StatusCode, 204);
        }
    }
}
