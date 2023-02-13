using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Bookleus.Domain.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SKU { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = default!;

        [NotMapped]
        public bool IsAvailable => !BookReservations.Any(br => br.IsActive);

        [InverseProperty(nameof(CustomerBookReservation.Book))]
        public List<CustomerBookReservation> BookReservations { get; set; } = default!;
    }
}
