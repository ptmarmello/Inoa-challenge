using System;

class StockController
{
    public static async Task StockCall(string stockSymbol, double lowestPrice, double highestPrice )
    {
        stockSymbol.ToUpper(); // caso a string não esteja no formato correto
        // chama aqui o API Controller
        double onTimeValue = await APIController.APICall(stockSymbol);
        
        if(onTimeValue > highestPrice){
            MailController.MailCall("Sell");
        }
        else if(onTimeValue < lowestPrice){
            MailController.MailCall("Buy");
        }
        else{
            Console.WriteLine("Vamo manter mais um pouco");

            // Insira aqui
        }
    }
}
