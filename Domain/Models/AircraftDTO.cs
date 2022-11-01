using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AircraftDTO
    {
        [Required]
        [JsonPropertyName("RAB")]
        public string RAB { get; set; }
        [JsonPropertyName("Capacity")]
        public int Capacity { get; set; }
    }
}
