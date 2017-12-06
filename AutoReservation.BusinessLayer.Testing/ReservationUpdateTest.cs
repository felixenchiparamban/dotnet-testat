using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationUpdateTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var reservation = Target.GetReservationbyID(1);
            reservation.Von = new DateTime(2020, 01, 22);
            reservation.Bis = new DateTime(2020, 01, 25);
            Assert.IsTrue(Target.UpdateReservation(reservation));
        }
    }
}
