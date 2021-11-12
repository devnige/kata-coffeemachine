using System;
using CoffeeMachine.Builder;
using CoffeeMachine.Drinks;
using CoffeeMachine.Ingredients;

namespace CoffeeMachine
{
    public class InputHandler
    {
        private string _input;

        public InputHandler(string input)
        {
            _input = input;
        }

        public string ProcessInput()
        {
            var splitInput = SplitInput(_input);
            var sugarQuantity = GetSugarQuantity(splitInput[1]);
            var amountTendered = GetAmountTendered(splitInput[2]);
            var drinkLetter = GetDrinkLetter(splitInput[0]);
            var isExtraHot = IsExtraHotRequested(splitInput[0]);
            var drinkType = GetDrinkType(drinkLetter);
            IDrink drink;
            switch (drinkType)
                {
                    case ("coffee") :
                        drink = new Coffee(sugarQuantity, isExtraHot);
                        break;
                    case ("tea") :
                        drink = new Tea(sugarQuantity, isExtraHot);
                        break;
                    case ("chocolate") :
                        drink = new Chocolate(sugarQuantity, isExtraHot);
                        break;
                    case ("oj") :
                        drink = new OJ();
                        break;
                    default : throw new ArgumentOutOfRangeException();
                }
            string message;
                if (!IsEnoughMoneyTendered(amountTendered, drink.GetPrice()))
                {
                    message = MessageBuilder.CreateNotEnoughMoneyTenderedMessage(drinkType, drink.GetPrice(), amountTendered);
                    Console.WriteLine(message);
                    return message;
                }

                SalesTracker.IncrementDrinkCounter(drinkType);
                message = MessageBuilder.CreateOrderMessage(drinkType, sugarQuantity, isExtraHot);
                Console.WriteLine(message);

                Console.WriteLine("Do you want to show sales results for drinks? y/n");
                var response = Console.ReadLine();
                if (response == "y")
                {
                    MessageBuilder.CreateSalesReport(
                        SalesTracker.GetIndividualDrinkTypeSalesAmount(new Coffee(sugarQuantity, isExtraHot).GetName(), new Coffee(sugarQuantity, isExtraHot).GetPrice()),
                        SalesTracker.GetIndividualDrinkTypeSalesAmount(new Tea(sugarQuantity, isExtraHot).GetName(), new Tea(sugarQuantity, isExtraHot).GetPrice()),
                        SalesTracker.GetIndividualDrinkTypeSalesAmount(new Chocolate(sugarQuantity, isExtraHot).GetName(), new Chocolate(sugarQuantity, isExtraHot).GetPrice()),
                        SalesTracker.GetIndividualDrinkTypeSalesAmount(new OJ().GetName(), new OJ().GetPrice()),
                        SalesTracker.GetTotalSalesAmount(SalesTracker.GetIndividualDrinkTypeSalesAmount(new Coffee(sugarQuantity, isExtraHot).GetName(), new Coffee(sugarQuantity, isExtraHot).GetPrice()),
                            SalesTracker.GetIndividualDrinkTypeSalesAmount(new Tea(sugarQuantity, isExtraHot).GetName(), new Tea(sugarQuantity, isExtraHot).GetPrice()),
                            SalesTracker.GetIndividualDrinkTypeSalesAmount(new Chocolate(sugarQuantity, isExtraHot).GetName(), new Chocolate(sugarQuantity, isExtraHot).GetPrice()),
                            SalesTracker.GetIndividualDrinkTypeSalesAmount(new OJ().GetName(), new OJ().GetPrice())
                        )
                    );
                }
                return message;
        }

        public string[] SplitInput(string input)
        {
            return _input.Split(':');
        }

        public int GetSugarQuantity(string splitInput)
        {
            Int32.TryParse(splitInput, out int result);
            return result;
        }

        public decimal GetAmountTendered(string splitInput)
        {
            return Convert.ToDecimal(splitInput);
        }

        public bool IsExtraHotRequested(string splitInput)
        {
            return splitInput.Length > 1 && splitInput.ToCharArray()[1] == 'h' ? true : false;
        }

        public string GetDrinkLetter(string splitInput)
        {
            return splitInput.Length > 1 ? splitInput[0].ToString() : splitInput;
        }

        public string GetDrinkType(string drinkLetter) => drinkLetter switch
        {
            "C" => "coffee",
            "T" => "tea",
            "H" => "chocolate",
            "O" => "oj",
            _ => throw new ArgumentOutOfRangeException(nameof(drinkLetter), drinkLetter, null)
        };
        
        public bool IsEnoughMoneyTendered(decimal amountGiven, decimal drinkCost)
        {
            return amountGiven >= drinkCost;
        }
    }
}