using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Domain.Models
{
    public class Address
    {
        [StringLength(9)]
        [JsonPropertyName("zipcode")]
        public string ZipCode { get; set; }

        [StringLength(100)]
        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [StringLength(10)]
        [JsonPropertyName("complement")]
        public string Complement { get; set; }

        [StringLength(30)]        
        [JsonPropertyName("city")]
        public string City { get; set; }

        [StringLength(2)]
        [JsonPropertyName("state")]
        public string State { get; set; }
    }
    public class AddressDTOViaCep: Address
    {
        [StringLength(9)]
        [JsonPropertyName("cep")]
        public string ZipCode { get; set; }

        [StringLength(100)]
        [JsonPropertyName("logradouro")]
        public string Street { get; set; }

        [JsonPropertyName("numero")]
        public int Number { get; set; }

        [StringLength(10)]
        [JsonPropertyName("complemento")]
        public string Complement { get; set; }

        [StringLength(30)]
        [JsonPropertyName("localidade")]
        public string City { get; set; }

        [StringLength(2)]
        [JsonPropertyName("uf")]
        public string State { get; set; }
    }
}
