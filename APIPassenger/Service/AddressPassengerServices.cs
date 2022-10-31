using Domain.Models;
using Newtonsoft.Json;
using System.Net.Http;
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
                    return address = JsonConvert.DeserializeObject<Address>(addressJson);
                else
                    return null;
            }
        }
    }
}
