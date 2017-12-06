using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationAvailabilityTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void ScenarioOkay01Test()
        {
            //No Overlap
            // |----| Date Range A
            //      |-------| Date Range B

            var rangeB = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 20), Bis = new DateTime(2020, 01, 30) };
            Assert.IsTrue(Target.AvailabilityCheck(rangeB));
        }

        [TestMethod]
        public void ScenarioOkay02Test()
        {
            //No Overlap
            //                 |----| Date Range A
            //      |-------| Date Range B

            var rangeA = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 22), Bis = new DateTime(2020, 01, 25) };

            Assert.IsTrue(Target.AvailabilityCheck(rangeA));
        }

        [TestMethod]
        [ExpectedException(typeof(AutoUnavailableException))]
        public void ScenarioNotOkay01Test()
        {
            //Overlapping
            // |----| Date Range A
            //   |-------| Date Range B

            var rangeB = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 15), Bis = new DateTime(2020, 01, 25) };
            Target.AvailabilityCheck(rangeB);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(AutoUnavailableException))]
        public void ScenarioNotOkay02Test()
        {
            //Overlapping
            // |-----------------------| Date Range A
            //   |-------| Date Range B

            var rangeB = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 12), Bis = new DateTime(2020, 01, 16) };
            Target.AvailabilityCheck(rangeB);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(AutoUnavailableException))]
        public void ScenarioNotOkay03Test()
        {
            //Overlapping
            //     |--------------| Date Range A
            //|-------| Date Range B

            var rangeB = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 5), Bis = new DateTime(2020, 01, 15) };
            Target.AvailabilityCheck(rangeB);
            Assert.Fail();
        }
        [TestMethod]
        [ExpectedException(typeof(AutoUnavailableException))]
        public void ScenarioNotOkay04Test()
        {
            //Overlapping
            //     |--------------| Date Range A
            //|------------------------| Date Range B

            var rangeB = new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 5), Bis = new DateTime(2020, 01, 25) };
            Target.AvailabilityCheck(rangeB);
            Assert.Fail();
        }
    }
}
