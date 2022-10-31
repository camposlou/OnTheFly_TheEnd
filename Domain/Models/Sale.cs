using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    public class Sale
    {
        [Required(ErrorMessage = "Flight é um campo obrigatório")]
        public Flight Flight { get; set; }
        [Required(ErrorMessage = "Passenger é um campo obrigatório")]
        public List<Passenger> Passenger { get; set; }
        [Required(ErrorMessage = "Reserved é um campo obrigatório")]
        public bool Reserved { get; set; }
        [Required(ErrorMessage = "Sold é um campo obrigatório")]
        public bool Sold { get; set; }
    }
}
