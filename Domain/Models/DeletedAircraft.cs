using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class DeletedAircraft
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
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        [JsonPropertyName("DtRegistry")]
        public DateTime DtRegistry { get; set; }

        [Required(ErrorMessage = "Formato de data inválido!")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        [JsonPropertyName("DtLastFlight")]
        public DateTime? DtLastFlight { get; set; }

        //  [Required(ErrorMessage = "Este campo é obrigatório!"), StringLength(19, ErrorMessage = "CNPJ inválido!")]
        // public Company Company{ get; set; }
        //public string CNPJ { get; set; }
        #endregion

        #region Method
        //public AirCraft()
        //{
        //    DtRegistry = DateTime.Now;
        //}
        #endregion
    }
}
