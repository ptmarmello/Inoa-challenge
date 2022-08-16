using System;
using Newtonsoft.Json;

class APIController
{
    public static async Task<double> APICall(string stockSymbol)
    {
        //  ********* INICIALIZAÇÃO DAS CONFIGURAÇÕES HTTP-CLIENT *********
        var client = new HttpClient();
        double stockValue = 0;
        String jsonFile = File.ReadAllText("appsettings.json");
        dynamic infoInJson = JsonConvert.DeserializeObject(jsonFile);

        // aqui deve ser passado um secret ou variavel de ambiente para mais segurança
        string apiKey = infoInJson["TwelveDataAPI"]["Apikey"].Value;

        //  ********* FINALIZAÇÃO DAS CONFIGURAÇÕES HTTP-CLIENT *********


        //  ********* INÍCIO DA CONEXÃO E REQUEST HTTP PARA API *********
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.twelvedata.com/time_series?apikey={apiKey}&interval=1min&symbol={stockSymbol}&format=JSON&outputsize=1"),
            
        };
        //  ********* FINAL DA CONEXÃO E REQUEST HTTP PARA API *********

        //  ********* INÍCIO DA OBTENÇÃO DO VALOR DA COTAÇÃO *********
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            dynamic bodyInJson = JsonConvert.DeserializeObject(body);

            // verifica se vier com algum código diferente de 200 e retornar o erro antes de retornar o valor e mandar o email
            if( bodyInJson["status"].Value == "ok" ){
                // A divisão é uma forma de converter o valor que chega em formato string e é passado para double
                stockValue = Convert.ToDouble(bodyInJson["values"][0]["close"].Value)/100000;
            }
            else{

                // Caso alguém digite algo errado
                throw new ArgumentException($"Something Wrong Happened. CodeStatus:{bodyInJson["code"].Value}, {bodyInJson["message"].Value}.");
            }

        }
        //  ********* FINAL DA OBTENÇÃO DO VALOR DA COTAÇÃO *********
        return stockValue;
    }
}
