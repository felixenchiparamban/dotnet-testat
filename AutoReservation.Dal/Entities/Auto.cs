using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    [Table("Auto")]
    public abstract class Auto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20), Required]
        public string Marke { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int Tagestarif { get; set; }

        //Navigation Property
        public ICollection<Reservation> Reservationen { get; set; }
    }

    public class StandardAuto : Auto {}

    public class LuxusklasseAuto : Auto
    {
        [Required]
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto {}
}
