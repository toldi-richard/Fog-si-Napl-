using Fogasi_naplo_webapi.Controllers;
using Fogasi_naplo_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fogasi_naplo_WebAPI_Test
{
    [TestClass]
    public class NemAdatbazisAlapuUnitTeszt
    {
        //Nem adatbázis alapú egység tesztek
        private List<Halfaj> GetTestUsers()
        {
            var testFishes = new List<Halfaj>();

            testFishes.Add(new Halfaj { Id = 1, Nev = "Ponty", LatinNev = "Cyprinus carpio", Elettartam = 20 });
            testFishes.Add(new Halfaj { Id = 2, Nev = "Keszeg", LatinNev = "Abramis brama", Elettartam = 7 });
            testFishes.Add(new Halfaj { Id = 3, Nev = "Csuka", LatinNev = "Esox lucius", Elettartam = 10 });
            testFishes.Add(new Halfaj { Id = 4, Nev = "Harcsa", LatinNev = "Silurus glanis", Elettartam = 60 });

            return testFishes;
        }

        [TestMethod]
        public void GetAllFishes_VisszaadMindenHalfajt()
        {
            var testFishes = GetTestUsers();
            var controller = new HalfajController(testFishes);
            var result = controller.GetAllFishes() as List<Halfaj>;
            Assert.AreEqual(testFishes.Count, result.Count);
        }

    }
}
