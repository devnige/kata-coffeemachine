using System;

namespace CoffeeMachine
{
    public static class DrinkReport
    {
        private static int CoffeeCounter { get; set; }
        private static int TeaCounter { get; set; }
        private static int ChocolateCounter { get; set; }
        private static int OJCounter { get; set; }

        public static int IncrementDrinkCounter(string drinkType) => drinkType switch
        {
            "C" => CoffeeCounter++,
            "T" => TeaCounter++,
            "H" => ChocolateCounter++,
            "O" => OJCounter++,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static int Counters(string drinkType) => drinkType switch
        {
            "C" => CoffeeCounter,
            "T" => TeaCounter,
            "H" => ChocolateCounter,
            "O" => OJCounter,
            _ => throw new ArgumentOutOfRangeException()
        };

        public static void ClearCounters()
        {
            CoffeeCounter = 0;
            TeaCounter = 0;
            ChocolateCounter = 0;
            OJCounter = 0;
        }
        
        public static decimal DrinkSalesAmount()
        {
            var coffeeSales = CoffeeCounter * (decimal) DrinkSelection.Coffee / 100;
            var teaSales = TeaCounter * (decimal) DrinkSelection.Tea / 100;
            var chocolateSales = ChocolateCounter * (decimal) DrinkSelection.Chocolate / 100;
            var ojSales = OJCounter * (decimal) DrinkSelection.OJ / 100;
            var totalSales = coffeeSales + teaSales + chocolateSales + ojSales;
            return totalSales;
        }
    }
}