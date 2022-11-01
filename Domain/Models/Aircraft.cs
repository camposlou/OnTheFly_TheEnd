using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;


namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Aircraft
    {
        #region Property
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(6, ErrorMessage = "Registro Aeronáutico Brasileiro inválido!")]
        [JsonPropertyName("RAB")]
        public string RAB { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [JsonPropertyName("Capacity")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Formato de data inválido!")]
        [DataType(DataType.Date)]
        [JsonPropertyName("DtRegistry")]
        public DateTime DtRegistry { get; set; }

        [DataType(DataType.Date)]
        [JsonPropertyName("DtLastFlight")]
        public DateTime DtLastFlight { get; set; }

        
        [JsonPropertyName("Company")]
        public Company Company{ get; set; }
        //public string CNPJ { get; set; }
        #endregion


    }
}

