using System;

class StockController
{
    public static async Task StockCall(string stockSymbol, double lowestPrice, double highestPrice )
    {
        stockSymbol.ToUpper(); // caso a string não esteja no formato correto
        // chama aqui o Controlador da API TwelveData e retorna o valor atual da cotação
        double onTimeValue = await APIController.APICall(stockSymbol);
        
        // Verifica se o valor atual é maior que o maior valor ou menor que o menor valor
        if(onTimeValue > highestPrice){
            MailController.MailCall("Sell");
        }
        else if(onTimeValue < lowestPrice){
            MailController.MailCall("Buy");
        }
        // else{

            // É possível colocar aqui mais opções do que fazer caso seja necessário
        // }
    }
}
