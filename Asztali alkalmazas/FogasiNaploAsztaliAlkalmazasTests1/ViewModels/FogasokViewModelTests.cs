using Microsoft.VisualStudio.TestTools.UnitTesting;
using FogasiNaploAsztaliAlkalmazas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FogasiNaploAsztaliAlkalmazas.Stores;
using FogasiNaploAsztaliAlkalmazas.Models;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels.Tests
{
    [TestClass()]
    public class FogasokViewModelTests
    {
        [TestMethod()]
        public void FogasokViewModelTest_GetAllData()
        {
            RunEnvironment.ChangeToTestEnvironment();

            FogasokViewModel fogasokViewModel = new FogasokViewModel();

            int expected = 6;
            int actual = fogasokViewModel.Fogasok.Count;

            Assert.AreEqual(expected, actual, "Nem lett beolvasva az összes fogás!");
        }

        [TestMethod()]
        public void FogasokViewModelTest_DeleteInsertTest()
        {
            RunEnvironment.ChangeToTestEnvironment();
            FogasokViewModel fogasokViewModel = new FogasokViewModel();

            List<Fogasok> fogasok = fogasokViewModel.Fogasok.ToList();
            if (fogasok.Count > 0)
            {
                Fogasok torlendoFogas = fogasok.ElementAt(1);

                fogasokViewModel.Delete(torlendoFogas);

                int expected = 5;
                int actual = fogasokViewModel.Fogasok.Count;

                Assert.AreEqual(expected, actual, "Törlések után a fogások száma nem csökkent!");

                List<Fogasok> toroltLista = fogasokViewModel.Fogasok.ToList();

                expected = 0;
                actual = toroltLista.FindAll(fogasok => fogasok.fogasID == torlendoFogas.fogasID).Count;
                Assert.AreEqual(expected, actual, "Törlések után a törölt id-je megtalálható a listában!");

                fogasokViewModel.Save(torlendoFogas);
                expected = 6;
                actual = fogasokViewModel.Fogasok.Count;

                Assert.AreEqual(expected, actual, "Új fogás felvétele után a fogások száma nem nőtt!");
            }
        }
    }
}