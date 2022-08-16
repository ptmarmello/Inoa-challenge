using System;

namespace ConsoleApp
{
    class Program
    {  

        static async Task Main(string[] args)
        {
            //  ********* ENTRADA DOS ARGS *********
            // recebe os argumentos na ordem determinada String Double Double. 
            string StockValue = args[0].ToString();
            double priceOne = Convert.ToDouble( args[1].ToString() )/10;
            double priceTwo = Convert.ToDouble( args[2].ToString() )/10;
            //  ********* FINAL DOS ARGS *********

            //  ********* INÍCIO DA VERIFICAÇÃO DE VALORES *********
            // Por via das dúvidas, criei essa mini função só pra identificar se os valores passados estão corretamente na ordem "menor - maior"
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
            //  ********* FINAL DA VERIFICAÇÃO DE VALORES *********

            //  ********* INÍCIO DA FUNÇÃO LOOP *********
            // Aqui fica a chamada do controlador geral das cotações e do envio de e-mail
            while(true){
                await StockController.StockCall(StockValue, lowestPrice, highestPrice );
                await Task.Delay(60000); // Delay de 1min para garantir o invervalo de 1min entre as avaliações da cotação. 
            }
            //  ********* FINAL DA FUNÇÃO LOOP *********
        }
    }
}