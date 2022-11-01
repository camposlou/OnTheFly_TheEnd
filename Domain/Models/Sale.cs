using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Sale
    {
        [Required(ErrorMessage = "Flight é um campo obrigatório")]
        [JsonPropertyName("Flight")]
        public Flight Flight { get; set; }
        [Required(ErrorMessage = "Passenger é um campo obrigatório")]
        [JsonPropertyName("Passenger")]
        public List<Passenger> Passenger { get; set; }
        [Required(ErrorMessage = "Reserved é um campo obrigatório")]
        [JsonPropertyName("Reserved")]
        public bool Reserved { get; set; }
        [Required(ErrorMessage = "Sold é um campo obrigatório")]
        [JsonPropertyName("Sold")]
        public bool Sold { get; set; }
    }
}
