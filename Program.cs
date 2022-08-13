using System;
// using System.Net.Mail;
// using MailKit.Net.Smtp;
// using MailKit;
// using MimeKit;

using Controller;

namespace ConsoleApp
{
    class Program
    {  
        static async Task Main(string[] args)
        {
            // isso aqui depois tem que mudar pra receber tudo de uma só vez
            string StockValue = "petr4";
            double priceOne = 30.2;
            double priceTwo = 35.6;

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

            await StockController.StockCall(StockValue, lowestPrice, highestPrice );
        }
    }
}