using System;
using System.ComponentModel.DataAnnotations;

namespace LetishteNet5.Data.Entities
{
    public class Reservation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter Valid SSN!")]
        public string SSN { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nationality { get; set; }

        [Required]
        public string TicketType { get; set; }

        [Required]
        [Range(1, 100)]
        public int TicketsCount { get; set; }

        public bool IsConfirmed { get; set; } = false;

        public string FlightId { get; set; }

        public Flight Flight { get; set; }
    }
}
