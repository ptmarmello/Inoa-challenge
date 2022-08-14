using System;

namespace ConsoleApp
{
    class Program
    {  

        static async Task Main(string[] args)
        {
            // isso aqui depois tem que mudar pra receber tudo de uma só vez
            string StockValue = args[0].ToString();
            double priceOne = Convert.ToDouble( args[1].ToString() )/10;
            double priceTwo = Convert.ToDouble( args[2].ToString() )/10;

            double lowestPrice;
            double highestPrice;
            if( priceOne < priceTwo ){
                lowestPrice = priceOne;
                highestPrice = priceTwo;
            }
            else
            {
                lowestPrice = priceTwo;
                highestPrice = priceOne;
            }

            while(true){
                await StockController.StockCall(StockValue, lowestPrice, highestPrice );
                await Task.Delay(60000);
            }
        }
    }
}