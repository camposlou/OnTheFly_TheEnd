using Domain.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace APIPassenger.Services
{
    public class AddressPassengerServices
    {
        public AddressPassengerServices() { }
        public async Task<Address> GetAdress(string cep)
        {
            Address address;
            using (HttpClient _adressClient = new HttpClient())
            {
                HttpResponseMessage response = await _adressClient.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                var addressJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    AddressDTOViaCep addressDTO = JsonSerializer.Deserialize<AddressDTOViaCep>(addressJson);
                    address = new Address() { City = addressDTO.City, Complement = addressDTO.Complement, Number = addressDTO.Number,
                        State = addressDTO.State , Street = addressDTO.Street, ZipCode = addressDTO.ZipCode
                    };
                    return address;
                }
                    
                else
                    return null;
            }
        }
    }
}
