using System;
using CoffeeMachine.Drinks;

namespace CoffeeMachine.Builder
{
    public static class SalesTracker
    {
        private static int CoffeeCounter { get; set; }
        private static int TeaCounter { get; set; }
        private static int ChocolateCounter { get; set; }
        private static int OJCounter { get; set; }
        
        public static int IncrementDrinkCounter(string drinkType) => drinkType switch
        {
            "coffee" => ++CoffeeCounter,
            "tea" => ++TeaCounter,
            "chocolate" => ++ChocolateCounter,
            "oj" => ++OJCounter,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static int GetIndividualDrinkTypeCounterValue(string drinkType) => drinkType switch
        {
            "coffee" => CoffeeCounter,
            "tea" => TeaCounter,
            "chocolate" => ChocolateCounter,
            "oj" => OJCounter,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static void ClearCounters()
        {
            CoffeeCounter = 0;
            TeaCounter = 0;
            ChocolateCounter = 0;
            OJCounter = 0;
        }

        public static decimal GetIndividualDrinkTypeSalesAmount(string drinkType, decimal drinkPrice) => drinkType switch
        {
            "coffee" => CoffeeCounter * drinkPrice,
            "tea" => TeaCounter * drinkPrice,
            "chocolate" => ChocolateCounter * drinkPrice,
            "oj" => OJCounter * drinkPrice,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static decimal GetTotalSalesAmount(decimal coffeeSales, decimal teaSales, decimal chocolateSales, decimal ojSales)
        {
            return coffeeSales + teaSales + chocolateSales + ojSales;
        }
    }
}