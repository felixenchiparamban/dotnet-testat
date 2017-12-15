using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;

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


        //WIP
        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            AutoDto autoOne = Target.GetAutoById(1);
            AutoDto autoTwo = Target.GetAutoById(1);
            autoOne.Marke = "TATA";
            autoOne.Marke = "auto";
            Target.UpdateAuto(autoOne);
            Target.UpdateAuto(autoTwo);
            Assert.Fail();
        }

        [ExpectedException(typeof(FaultException<KundeDto>))]
        [TestMethod]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            KundeDto kundeOne = Target.GetKundeById(1);
            KundeDto kundeTwo = Target.GetKundeById(1);
            kundeOne.Nachname = "muster";
            kundeTwo.Nachname = "hello";
            Target.UpdateKunde(kundeOne);
            Target.UpdateKunde(kundeTwo);
            Assert.Fail();
        }

        //WIP
        [ExpectedException(typeof(FaultException<ReservationDto>))]
        [TestMethod]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto reservationOne = Target.GetReservationById(1);
            ReservationDto reservationTwo = Target.GetReservationById(1);
            reservationOne.Von = reservationOne.Von.AddYears(1);
            reservationOne.Bis = reservationOne.Bis.AddYears(1);
            reservationTwo.Von = reservationTwo.Von.AddYears(10);
            reservationTwo.Bis = reservationTwo.Bis.AddYears(10);


            Target.UpdateReservation(reservationOne);
            try
            {
                Target.UpdateReservation(reservationTwo);

            }
            catch (Exception)
            {

                throw;
            }
            Assert.Fail();
        }

        #endregion

        #region Insert / update invalid time range

        [ExpectedException(typeof(FaultException<FaultInvalidDateRange>))]
        [TestMethod]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            ReservationDto reservationDto = new ReservationDto
            {
                Von = new DateTime(2025, 11, 11),
                Bis = new DateTime(2025, 11, 5),
                Kunde = Target.GetKundeById(1),
                Auto = Target.GetAutoById(1)
            };
            Target.InsertReservation(reservationDto);            
            Assert.Fail();
        }

        [ExpectedException(typeof(FaultException<FaultAutoUnavailable>))]
        [TestMethod]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            ReservationDto reservation = new ReservationDto
            {
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20),
                Kunde = Target.GetKundeById(1),
                Auto = Target.GetAutoById(1)
            };
            Target.InsertReservation(reservation);
        }

        [ExpectedException(typeof(FaultException<FaultInvalidDateRange>))]
        [TestMethod]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            var reservation = Target.GetReservationById(1);
            reservation.Von = new DateTime(2020, 01, 20);
            reservation.Bis = new DateTime(2020, 01, 10);
            Target.UpdateReservation(reservation);
            Assert.Fail();
        }

        [ExpectedException(typeof(FaultException<FaultAutoUnavailable>))]
        [TestMethod]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            var reservation = Target.GetReservationById(1);
            reservation.Bis = new DateTime(2020, 01, 15);
            Target.UpdateReservation(reservation);
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            var from = new DateTime(2030, 11, 11);
            var to = new DateTime(2030, 12, 12);
            int kundenId = 1;
            int reservationNr = 1;
            Assert.IsTrue(Target.IsAutoAvailable(from, to, kundenId, reservationNr));
        }

        [ExpectedException(typeof(FaultException<FaultAutoUnavailable>))]
        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            var from = new DateTime(2020, 01, 10);
            var to = new DateTime(2020, 01, 20);
            int kundenId = 1;
            int reservationNr = 1;

            Target.IsAutoAvailable(from, to, kundenId, reservationNr);
            Assert.Fail();
        }

        #endregion
    }
}
