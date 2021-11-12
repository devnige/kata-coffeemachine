using System;
using System.Net;
using System.Text;
using Microsoft.VisualBasic;

namespace CoffeeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Acme Drinks Maker 2021\n");
            Console.WriteLine("Please enter the kind of drink you would like to make:");
            var drinkInput = Console.ReadLine();
            var inputBuilder = new StringBuilder();
            if (drinkInput is "coffee" or "tea" or "chocolate")
            {
                inputBuilder.Append(drinkInput.ToUpper().ToCharArray()[0]);
                Console.WriteLine("Do you want your drink extra hot? y/n");
                var extraHotRequested = Console.ReadLine();
                if (extraHotRequested == "y")
                {
                    inputBuilder.Append('h');
                }
                inputBuilder.Append(':');
                Console.WriteLine("Please enter how many sugars you want:");
                inputBuilder.Append(Console.ReadLine());
            }
            else if (drinkInput == "oj")
            {
                inputBuilder.Append(drinkInput.ToUpper().ToCharArray()[0]);
                inputBuilder.Append(':');
                inputBuilder.Append('0');
            }
            inputBuilder.Append(':');
            Console.WriteLine("Please enter your payment amount:");
            inputBuilder.Append(Console.ReadLine());
            var input = inputBuilder.ToString();
            
            
            var inputHandler = new InputHandler(input);
            inputHandler.ProcessInput();
        }
    }
}