using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            var auto = Target.GetAutos();
            Assert.IsNotNull(auto);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            var kunde = Target.GetKunden();
            Assert.IsNotNull(kunde);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            var reservation = Target.GetReservationen();
            Assert.IsNotNull(reservation);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            var auto = Target.GetAutoById(1);
            Assert.AreEqual(auto.Marke, "Fiat Punto");
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var kunde = Target.GetKundeById(1);
            Assert.AreEqual(kunde.Vorname, "Anna");
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            var reservation = Target.GetReservationById(1);
            Assert.AreEqual(reservation.Auto.Marke, "Fiat Punto");
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            var auto = Target.GetAutoById(9);
            Assert.IsNull(auto);
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            var kunde = Target.GetKundeById(9);
            Assert.IsNull(kunde);
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            var reservation = Target.GetReservationById(100);
            Assert.IsNull(reservation);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            AutoDto autodto = new AutoDto();
            autodto.Marke = "TATA Nano";
            autodto.AutoKlasse = AutoKlasse.Standard;
            autodto.Tagestarif = 20;

            Target.InsertAuto(autodto);

        }

        [TestMethod]
        public void InsertKundeTest()
        {
            KundeDto kundeDto = new KundeDto
            {
                Vorname = "Felix",
                Nachname = "Enchiparamban",
                Geburtsdatum = new System.DateTime(1992, 02, 09)
            };
            Target.InsertKunde(kundeDto);
            Assert.AreEqual(kundeDto.Vorname, "Felix");
        }

        [TestMethod]
        public void InsertReservationTest()
        {

            ReservationDto reservationDto = new ReservationDto
            {
                Von = new DateTime(2025, 01, 22),
                Bis = new DateTime(2025, 01, 28),
                Kunde = Target.GetKundeById(1),
                Auto = Target.GetAutoById(1)
            };
            var newReservation = Target.InsertReservation(reservationDto);
            Assert.IsTrue(newReservation);
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(Target.GetAutoById(1));
            Assert.IsNull(Target.GetAutoById(1));
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Target.DeleteKunde(Target.GetKundeById(1));
            Assert.IsNull(Target.GetKundeById(1));
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Target.DeleteReservation(Target.GetReservationById(1));
            Assert.IsNull(Target.GetReservationById(1));
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            var auto = Target.GetAutoById(1);
            auto.Marke = "Mahindra";
            Target.UpdateAuto(auto);
            Assert.AreEqual(auto.Marke, Target.GetAutoById(1).Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var kunde = Target.GetKundeById(1);
            kunde.Nachname = "Enchiparamban";
            Assert.IsTrue(Target.UpdateKunde(kunde));
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var reservation = Target.GetReservationById(1);
            reservation.Von = new DateTime(2029, 11, 11);
            reservation.Bis = new DateTime(2030, 11, 11);
            Assert.IsTrue(Target.UpdateReservation(reservation));
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [TestMethod]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}
