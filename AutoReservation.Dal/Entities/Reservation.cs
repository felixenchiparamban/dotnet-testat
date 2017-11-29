using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key, Column("Id")]
        public int ReservationsNr { get; set; }

        public int AutoId { get; set; }

        public DateTime Bis { get; set; }

        public int KundeId { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTime Von { get; set; }

        //Navigation Properties
        [ForeignKey("AutoId")] // für InverseProperty
        public virtual Auto Auto { get; set; }

        [ForeignKey("KundeId")]
        public virtual Kunde Kunde { get; set; }
    }
}
