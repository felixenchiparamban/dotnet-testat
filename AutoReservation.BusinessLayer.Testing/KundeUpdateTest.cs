using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class KundeUpdateTest
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var kundeUpdate = Target.GetKundeById(1);
            kundeUpdate.Nachname = "Tata";
            kundeUpdate.Vorname = "Ratan";
            Assert.IsTrue(Target.UpdateKunde(kundeUpdate));
        }
    }
}
