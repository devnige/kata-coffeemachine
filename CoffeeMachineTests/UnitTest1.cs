using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using CoffeeMachine;
using Xunit;
using Xunit.Sdk;

namespace CoffeeMachineTests
{
    public class UnitTest1
    {
        public UnitTest1()
        {
        }

        [Theory] // check different drink types, all with no sugar
        [InlineData("coffee", 0, true, "The drink maker will make one coffee with no sugar")] 
        [InlineData("tea", 0, true, "The drink maker will make one tea with no sugar")]
        [InlineData("chocolate", 0, true, "The drink maker will make one chocolate with no sugar")]

        public void GivenCreateOrderMessage_WhenOrderStringReceived_ThenOrderDetailsAreReturned(string drinkType, int sugarQuantity, bool isDrinkAvailable, string result)
        {
            //Arrange
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);
            
            //Act
            var actual =  coffeeMachine.CreateOrderMessage(drinkType, sugarQuantity, false);
            
            //Assert
            Assert.Equal(result, actual);
        }

        [Theory] // check different sugar quantities
        [InlineData("coffee", 2, true, "The drink maker will make one coffee with two sugars and a stick")] 
        [InlineData("coffee", 1, true, "The drink maker will make one coffee with one sugar and a stick")]
        [InlineData("coffee", 0, true, "The drink maker will make one coffee with no sugar")]
        [InlineData("tea", 0, true, "The drink maker will make one tea with no sugar")]
        [InlineData("tea", 1, true, "The drink maker will make one tea with one sugar and a stick")]
        [InlineData("tea", 2, true, "The drink maker will make one tea with two sugars and a stick")]
        [InlineData("chocolate", 0, true, "The drink maker will make one chocolate with no sugar")]
        [InlineData("chocolate", 2, true, "The drink maker will make one chocolate with two sugars and a stick")]
        [InlineData("chocolate", 1, true, "The drink maker will make one chocolate with one sugar and a stick")]
        [InlineData("oj", 0, true, "The drink maker will make one oj")]

        public void GivenCreateOrderMessage_WhenOrderStringHasDifferentQuantitiesOfSugar_ThenOrderDetailsAreReturned
            (string input, int sugarQuantity, bool isDrinkAvailable, string result)
        {
            //Arrange
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);
            
            //Act
            var actual =  coffeeMachine.CreateOrderMessage(input, sugarQuantity, false);
            
            //Assert
            Assert.Equal(result, actual);

        }
        
        [Theory] // check when _isDrinkAvailable = false
        [InlineData("C:2:0", false, "Sorry, your choice of coffee is not available")] 
        [InlineData("T:1:0", false, "Sorry, your choice of tea is not available")]
        [InlineData("H:0:0", false, "Sorry, your choice of chocolate is not available")]
        [InlineData("O:0:0", false, "Sorry, your choice of oj is not available")]

        public void GivenCheckMoneyGiven_WhenDrinkIsUnavailable_ThenMessageThatWeAreOutOfThatDrinkIsGiven(string input, bool isDrinkAvailable, string result)
        {
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);
            var actual = coffeeMachine.ProcessOrder(input);

            Assert.Equal(result, actual);
        }
        
        [Theory] // checking when correct money has been given
        [InlineData("coffee", 0.7, true, "Your coffee is being prepared")]
        [InlineData("tea", 0.4, true, "Your tea is being prepared")]
        [InlineData("chocolate", 0.5, true, "Your chocolate is being prepared")]
        
        public void GivenCheckMoneyGiven_WhenCorrectMoneyIsTendered_ThenDrinkIsMade(string drinkType, decimal moneyGiven, bool isDrinkAvailable, string result)
        {
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);

            var actual = coffeeMachine.ProcessStatus(drinkType, moneyGiven);
            
            Assert.Equal(result, actual);
        }
        
        [Theory]
        [InlineData("coffee", 0.1, true, "You haven't inserted enough money for coffee")]
        [InlineData("tea", 0.3, true, "You haven't inserted enough money for tea")]
        [InlineData("chocolate", 0.4, true, "You haven't inserted enough money for chocolate")]
        
        public void GivenCheckMoneyGiven_WhenIncorrectAmountIsTendered_ThenMessageStatingNotEnoughMoneyForDrinkTypeIsReturned(string drinkType, decimal moneyGiven, bool isDrinkAvailable, string result)
        {
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);

            var actual = coffeeMachine.ProcessStatus(drinkType, moneyGiven);
            
            Assert.Equal(result, actual);
        }
        
        [Theory]
        [InlineData("oj", 0.6, true, "Your oj is being prepared")] // add oj to the drinks available

        public void GivenCheckMoneyGiven_WhenCorrectMoneyIsGiven_ThenOJIsMade(string drinkType, decimal moneyGiven, bool isDrinkAvailable, string result)
        {
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);

            var actual = coffeeMachine.ProcessStatus(drinkType, moneyGiven);
            
            Assert.Equal(result, actual);
        }
        
        [Theory] // add extra hot option to coffee, tea and chocolate
        
        [InlineData("Ch:0:0", true, "The drink maker will make one extra hot coffee with no sugar")]
        [InlineData("Ch:1:0", true, "The drink maker will make one extra hot coffee with one sugar and a stick")]
        [InlineData("Ch:2:0", true, "The drink maker will make one extra hot coffee with two sugars and a stick")]
        [InlineData("Th:0:0", true, "The drink maker will make one extra hot tea with no sugar")]
        [InlineData("Th:1:0", true, "The drink maker will make one extra hot tea with one sugar and a stick")]
        [InlineData("Th:2:0", true, "The drink maker will make one extra hot tea with two sugars and a stick")] 
        [InlineData("Hh:0:0", true, "The drink maker will make one extra hot chocolate with no sugar")]
        [InlineData("Hh:1:0", true, "The drink maker will make one extra hot chocolate with one sugar and a stick")]
        [InlineData("Hh:2:0", true, "The drink maker will make one extra hot chocolate with two sugars and a stick")] 
        
        
        public void GivenCheckMoneyGiven_WhenCustomerSelectsExtraHot_ThenExtraHotDrinkIsMade(string input, bool isDrinkAvailable, string result)
        {
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);
            var actual = coffeeMachine.ProcessOrder(input);

            Assert.Equal(result, actual);
        }
        
        [Theory] // check counter after drink ordered
        
        [InlineData("C:0:0", true, 1)]
        [InlineData("T:1:0", true, 1)]
        [InlineData("H:2:0", true, 4)]
        [InlineData("O:0:0", true, 1)]
        
        public void GivenProcessOrder_WhenAnOrderIsProcessed_ThenDrinkCounterIncrements(string input, bool isDrinkAvailable, int result)
        {
            var coffeeMachine = new DrinkEngine(isDrinkAvailable);
            coffeeMachine.ProcessOrder(input);
            
            var actual = DrinkReport.Counters(input[0].ToString());
            Assert.Equal(result, actual);
            DrinkReport.ClearCounters();
        }
    }
}