using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [Required]
        [JsonPropertyName("Destiny")]
        public Airport Destiny { get; set; }

        [Required]
        [JsonPropertyName("Plane")]
        public Aircraft Plane { get; set; }


        [Required]
        [JsonPropertyName("Sale")]
        public int Sale { get; set; }


        [Required]
        [JsonPropertyName("Departure")]
        public DateTime Departure { get; set; }


        [Required]
        [JsonPropertyName("Status")]
        public bool Status { get; set; }

    }
}
