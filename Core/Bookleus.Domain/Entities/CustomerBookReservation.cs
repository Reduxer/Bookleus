using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookleus.Domain.Entities
{

    public class CustomerBookReservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerBookReservationId { get; set; }

        public Guid BookSKU { get; set; }

        public string CustomerId { get; set; } = default!;

        [ForeignKey(nameof(BookSKU))]
        public Book Book { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
