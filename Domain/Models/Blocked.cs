using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    public class Blocked
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Cnpj é um campo obrigatório")]

        [StringLength(19)]
        [JsonPropertyName("Cnpj")]
        public string Cnpj { get; set; } //mascara
        [Required(ErrorMessage = "Nome é um campo obrigatório")]

        [StringLength(30)]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [StringLength(30)]
        [JsonPropertyName("NameOpt")]
        public string NameOpt { get; set; }
        [JsonPropertyName("DtOpen")]
        public DateTime DtOpen { get; set; }
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("Address")]
        public Address Adress { get; set; }
        

    }
}
