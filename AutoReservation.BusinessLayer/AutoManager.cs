using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;
using System.Data.Entity.Core;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager : ManagerBase
    {
        // Example
        //public List<Auto> List
        //{
        //    get
        //    {
        //        using (AutoReservationContext context = new AutoReservationContext())
        //        {
        //            return context.Autos.ToList();
        //        }
        //    }
        //}

        // Alle Entities lesen
        public List<Auto> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Autos.ToList();
                }
            }
        }

        //Eine Entity anhand des Primaerschluessels lesen
        public Auto GetAutoById(int id)
        {
            var result = from l in List
                         where l.Id == id
                         select l;
            return result as Auto;
        }

        // Einfuegen
        public void InsertAuto(Auto auto)
        {
            // Wieso funktioniet die folgende Code hiern icht?
            //List.Add(auto);
            //List.saveChanges();
            using (var context = new AutoReservationContext())
            {
                // Unterschied zwischen dem nachfolgenden codes?
                //context.Autos.Add(auto);
                context.Entry(auto).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        // Update
        public void UpdateAuto(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(auto).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new OptimisticConcurrencyException();
                }
            }
        }

        // Loeschen
        public void DeleteAuto(Auto auto)
        {
            using(var context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}