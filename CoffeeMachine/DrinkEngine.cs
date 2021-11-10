using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CoffeeMachine
{
    public class DrinkEngine
    {
        private bool _isCoffeeAvailable;
        private bool _isTeaAvailable;
        private bool _isChocolateAvailable;
        private bool _isOJAvailable;

        public DrinkEngine(bool isDrinkAvailable)
        {
            _isCoffeeAvailable = isDrinkAvailable;
            _isTeaAvailable = isDrinkAvailable;
            _isChocolateAvailable = isDrinkAvailable;
            _isOJAvailable = isDrinkAvailable;
            var drinkSales = DrinkReport.DrinkSalesAmount();
        }

        public string ProcessOrder(string input) // method to handle string input from machine
        {
            var splitInput = input.Split(':');
            var numSugars = Int32.TryParse(splitInput[1], out int result);
            var sugarQuantity = result;
            var amountGiven = System.Convert.ToDecimal(splitInput[2]);
            var drinkLetter = splitInput[0];
            var isExtraHotRequested = false;
            if (splitInput[0].Length > 1)
            {
                var subs = splitInput[0].ToCharArray();
                drinkLetter = subs[0].ToString();
                isExtraHotRequested = subs[1].ToString() == "h";
            }
            
            var drinkType = BuildFullHotDrinkName(drinkLetter);

            if (IsSelectedDrinkAvailable(drinkType))
            {
                switch (drinkType)
                {
                    case ("coffee") :
                        IsEnoughMoneyTendered(amountGiven, DrinkSelection.Coffee);
                        break;
                    case ("tea") :
                        IsEnoughMoneyTendered(amountGiven, DrinkSelection.Tea);
                        break;
                    case ("chocolate") :
                        IsEnoughMoneyTendered(amountGiven, DrinkSelection.Chocolate);
                        break;
                    case ("oj") :
                        IsEnoughMoneyTendered(amountGiven, DrinkSelection.OJ);
                        break;
                    default : throw new 
                }
                
                var systemStatusMessage = ProcessStatus(drinkType, amountGiven);
                
                var orderMessage = CreateOrderMessage(drinkType, sugarQuantity, isExtraHotRequested);
                IncrementDrinkCounter(drinkLetter);
                return orderMessage;
            }

            return $"Sorry, your choice of {drinkType} is not available";
        }

        public bool IsSelectedDrinkAvailable(string drinkType) => drinkType switch
        {
            "coffee" => _isCoffeeAvailable,
            "tea" => _isTeaAvailable,
            "chocolate" => _isChocolateAvailable,
            "oj" => _isOJAvailable,
            _ => throw new ArgumentException()
        };
        
        public string ProcessStatus(string drinkType, decimal amountGiven) => drinkType switch
        {
            "coffee" when IsEnoughMoneyTendered(amountGiven, DrinkSelection.Coffee) => $"Your {drinkType} is being prepared",
            "tea" when IsEnoughMoneyTendered (amountGiven, DrinkSelection.Tea) => $"Your {drinkType} is being prepared",
            "chocolate" when IsEnoughMoneyTendered(amountGiven, DrinkSelection.Chocolate) => $"Your {drinkType} is being prepared",
            "oj" when IsEnoughMoneyTendered(amountGiven, DrinkSelection.OJ) => $"Your {drinkType} is being prepared",
            _ => $"You haven't inserted enough money for {drinkType}"
        };
        
        private bool IsEnoughMoneyTendered(decimal amountGiven, DrinkSelection drinkSelected)
        {
            return amountGiven >= (decimal) drinkSelected / 100;
        }

        private void IncrementDrinkCounter(string drinkLetter)
        {
            DrinkReport.IncrementDrinkCounter(drinkLetter);
        }

        private string BuildFullHotDrinkName(string drinkLetter) => drinkLetter switch
        {
            "C" => "coffee",
            "T" => "tea",
            "H" => "chocolate",
            "O" => "oj",
            _ => throw new ArgumentOutOfRangeException(nameof(drinkLetter), drinkLetter, null)
        };

        public string GetSugarAttribute(int sugarQuantity) => sugarQuantity switch
        {
            0 => " with no sugar",
            1 => " with one sugar and a stick",
            2 => " with two sugars and a stick",
            _ => " with no sugar"
        };

        public static string GetExtraHotAttribute(bool isExtraHotRequested)
        {
            return isExtraHotRequested ? "extra hot " : "";
        }
        
        public string CreateOrderMessage(string drinkType, int sugarQuantity, bool isExtraHot) => drinkType switch
        {
            "coffee" or "tea" or "chocolate" =>
                $"The drink maker will make one {GetExtraHotAttribute(isExtraHot)}{drinkType}{GetSugarAttribute(sugarQuantity)}",
            "oj" => $"The drink maker will make one oj",
            _ => throw new ArgumentOutOfRangeException(nameof(drinkType), drinkType, null)
        };
    }
}