using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Flight
    {
        public Airport Destiny { get; set; }
        public Aircraft Plane { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int Sale { get; set; }


        [Required(ErrorMessage = "Formato de data Inválido!")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HHmm")]
        public DateTime Departure { get; set; }


        [Required]
        public bool Status { get; set; }

    }
}
