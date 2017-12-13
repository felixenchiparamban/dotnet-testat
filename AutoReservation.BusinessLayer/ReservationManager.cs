using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;
using System.Data.Entity.Core;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager : ManagerBase
    {
        // Alle Entities lesen
        public List<Reservation> List
        {
            get
            {
                using(var context = new AutoReservationContext())
                {
                    return context.Reservationen
                        .Include(r => r.Auto)
                        .Include(k => k.Kunde)
                        .ToList();
                }
            }
        }

        //Eine Entity anhand des Primaerschluessels lesen
        public Reservation GetReservationbyID(int id)
        {
            using(var context = new AutoReservationContext())
            {
                return context.Reservationen
                    .Include(r => r.Auto)
                    .Include(k => k.Kunde)
                    .SingleOrDefault(r => r.ReservationsNr == id);
            }
        }

        // Einfuegen
        public Reservation InsertReservation(Reservation reservation)
        {
            using(var context = new AutoReservationContext())
            {
                CheckDateRange(reservation);
                AvailabilityCheck(context, reservation);
                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
                return reservation; 
            }
        }

        // Update
        public bool UpdateReservation(Reservation reservation)
        {
            using(var context = new AutoReservationContext())
            {
                try
                {
                    CheckDateRange(reservation);
                    AvailabilityCheck(context, reservation);
                    context.Entry(reservation).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }

        // loeschen
        public void DeleteReservation(Reservation reservation)
        {
            using(var context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public bool CheckDateRange(Reservation reservation)
        {
            if ((reservation.Bis - reservation.Von).TotalHours < 24)
            {
                throw new InvalidDateRangeException("reservation error in checkDateRange");
            }
            return true;
        }

        private bool AvailabilityCheck(AutoReservationContext context, Reservation reservationA)
        {
            if (!(context.Reservationen.Any(r => r.AutoId == reservationA.AutoId)))
            {
                throw new AutoUnavailableException("reservation error in AvailabilityCheck");
            }

            List<Reservation> reserv = context.Reservationen
                .AsNoTracking()
                .Where(re => re.AutoId == reservationA.AutoId)
                .ToList();

            foreach(Reservation reservationB in reserv)
            {
                if(!(reservationA.Bis <= reservationB.Von || reservationA.Von >= reservationB.Bis))
                {
                    throw new AutoUnavailableException("reservation error in AvailabilityCheck");
                }
            }
            return true;
        }

        public bool AvailabilityCheck(Reservation reservation)
        {
            using(var context = new AutoReservationContext())
            {
                return AvailabilityCheck(context, reservation);
            }
        }
    }
}