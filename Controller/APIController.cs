using System;
using Newtonsoft.Json;

namespace Controller
{
    class APIController
    {
        public static async Task<double> APICall(string stockSymbol)
        {
            
            var client = new HttpClient();
            double stockValue = 0;
            String jsonFile = File.ReadAllText("appsettings.json");
            dynamic infoInJson = JsonConvert.DeserializeObject(jsonFile);

            string apiKey = infoInJson["Smtp"]["Apikey"].Value;

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.twelvedata.com/time_series?apikey={apiKey}&interval=1min&symbol={stockSymbol}&format=JSON&outputsize=1"),
                // RequestUri = new Uri($"https://www.google.com/search?q={stockSymbol}&oq={stockSymbol}&ie=UTF-8"),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                dynamic bodyInJson = JsonConvert.DeserializeObject(body);

                // verificar se vier com algum código diferente de 200 e retornar o erro antes de mandar o email
                if( bodyInJson["status"].Value == "ok" ){
                    // A divisão é uma forma de converter o valor que chega em formato string e é passado para double
                    stockValue = Convert.ToDouble(bodyInJson["values"][0]["close"].Value)/100000;
                }
                else{

                    // Caso alguém digite algo errado
                    throw new ArgumentException($"Something Wrong Happened. CodeStatus:{bodyInJson["code"].Value}, {bodyInJson["message"].Value}.");
                }

            }
            return stockValue;
        }
    }
}