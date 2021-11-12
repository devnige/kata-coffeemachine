using System;
using CoffeeMachine.Drinks;
using CoffeeMachine.Ingredients;

namespace CoffeeMachine.Builder
{
    public static class MessageBuilder
    {
        public static string GetExtraHotAttribute(bool isExtraHotRequested)
        {
            return isExtraHotRequested ? "extra hot " : "";
        }
        
        public static string CreateNotEnoughMoneyTenderedMessage(string drinkType, decimal drinkPrice, decimal amountTendered)
        {
            return $"You have not tendered enough money. You are short {drinkPrice} - {amountTendered}.\nPlease enter Euro {drinkPrice} for a {drinkType}";
        }
        
        public static string CreateOrderMessage(string drinkType, int sugarQuantity, bool isExtraHot) => drinkType switch
        {
            "coffee" or "tea" or "chocolate" =>
                $"The drink maker will make one {GetExtraHotAttribute(isExtraHot)}{drinkType}{new Sugar(sugarQuantity).GetSugarAttribute()}",
            "oj" => $"The drink maker will make one oj",
            _ => throw new ArgumentOutOfRangeException(nameof(drinkType), drinkType, null)
        };

        public static void CreateSalesReport(decimal coffeeSales, decimal teaSales, decimal chocolateSales, decimal ojSales, decimal totalSales)
        {

            Console.WriteLine($"Total coffee sales is {coffeeSales}");
            Console.WriteLine($"Total tea sales is {teaSales}");
            Console.WriteLine($"Total chocolate sales is {chocolateSales}");
            Console.WriteLine($"Total oj sales is {ojSales}");
            Console.WriteLine($"All sales total is {totalSales}");

        }
    }
}