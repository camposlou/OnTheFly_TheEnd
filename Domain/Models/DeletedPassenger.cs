using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{

    [BsonIgnoreExtraElements]
    public class DeletedPassenger
    {
        [MaxLength(14)]
        [Required]
        [JsonPropertyName("CPF")]
        public string CPF { get; set; }
        [MaxLength(30)]
        [Required]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Gender")]
        public char Gender { get; set; }
        [MaxLength(14)]
        [JsonPropertyName("Phone")]
        public string Phone { get; set; }
        [JsonPropertyName("DtBirth")]
        public DateTime DtBirth { get; set; }
        [JsonPropertyName("DtRegister")]
        public DateTime DtRegister { get; set; }
        [JsonPropertyName("Status")]
        public bool Status { get; set; }
        [JsonPropertyName("Address")]
        public Address Address { get; set; }
    }
}
