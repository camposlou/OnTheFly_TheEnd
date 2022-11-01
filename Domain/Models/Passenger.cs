using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Passenger
    {
        [Required]
        [MaxLength(14)]
        [JsonPropertyName("CPF")]
        public string CPF { get; set; }
        [Required]
        [MaxLength(30)]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("Gender")]
        public char Gender { get; set; }
        [MaxLength(14)]
        [JsonPropertyName("Phone")]
        public string? Phone { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [JsonPropertyName("DtBirth")]
        public DateTime DtBirth { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [JsonPropertyName("DtRegister")]
        public DateTime DtRegister { get; set; }
        [Required]
        [JsonPropertyName("Status")]
        public bool Status { get; set; }
        [Required]
        [JsonPropertyName("Address")]
        public Address Address { get; set; }

    }
}
