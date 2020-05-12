using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models
{
    public class ProductsSelectForReservation
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public virtual Reservations Reservations { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }


    }
}
