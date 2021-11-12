using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using CoffeeMachine;
using CoffeeMachine.Builder;
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
        [InlineData("coffee", 0, false, "The drink maker will make one coffee with no sugar")] 
        [InlineData("tea", 0, false, "The drink maker will make one tea with no sugar")]
        [InlineData("chocolate", 0, false, "The drink maker will make one chocolate with no sugar")]

        public void GivenCreateOrderMessage_WhenOrderStringReceived_ThenOrderDetailsAreReturned(string drinkType, int sugarQuantity, bool isExtraHot, string result)
        {
            //Arrange

            //Act
            var actual = MessageBuilder.CreateOrderMessage(drinkType, sugarQuantity, isExtraHot); 
            
            //Assert
            Assert.Equal(result, actual);
        }

        [Theory] // check different sugar quantities
        [InlineData("coffee", 2, false, "The drink maker will make one coffee with two sugars and a stick")] 
        [InlineData("coffee", 1, false, "The drink maker will make one coffee with one sugar and a stick")]
        [InlineData("coffee", 0, false, "The drink maker will make one coffee with no sugar")]
        [InlineData("tea", 0, false, "The drink maker will make one tea with no sugar")]
        [InlineData("tea", 1, false, "The drink maker will make one tea with one sugar and a stick")]
        [InlineData("tea", 2, false, "The drink maker will make one tea with two sugars and a stick")]
        [InlineData("chocolate", 0, false, "The drink maker will make one chocolate with no sugar")]
        [InlineData("chocolate", 2, false, "The drink maker will make one chocolate with two sugars and a stick")]
        [InlineData("chocolate", 1, false, "The drink maker will make one chocolate with one sugar and a stick")]
        [InlineData("oj", 0, false, "The drink maker will make one oj")]

        public void GivenCreateOrderMessage_WhenOrderStringHasDifferentQuantitiesOfSugar_ThenOrderDetailsAreReturned
            (string input, int sugarQuantity, bool isExtraHot, string result)
        {
            //Arrange
            
            
            //Act
            var actual =  MessageBuilder.CreateOrderMessage(input, sugarQuantity, isExtraHot);
            
            //Assert
            Assert.Equal(result, actual);

        }
        
        
        [Theory] // checking when correct money has been given
        [InlineData("C:2:0.7", "The drink maker will make one coffee with two sugars and a stick")]
        [InlineData("T:1:0.5", "The drink maker will make one tea with one sugar and a stick")]
        [InlineData("H:0:0.6", "The drink maker will make one chocolate with no sugar")]

        public void GivenCheckMoneyGiven_WhenCorrectMoneyIsTendered_ThenDrinkIsMade(string input, string result)
        {
            var inputHandler = new InputHandler(input);

            string actual = inputHandler.ProcessInput();
            
            Assert.Equal(result, actual);
        }
        
        [Theory]
        [InlineData("C:1:0", "You have not tendered enough money. You are short 0.6.\nPlease enter Euro 0.6 for a coffee")]
        [InlineData("T:0:0.3", "You have not tendered enough money. You are short 0.1.\nPlease enter Euro 0.4 for a tea")]
        [InlineData("H:2:0.2", "You have not tendered enough money. You are short 0.3.\nPlease enter Euro 0.5 for a chocolate")]
        
        public void GivenCheckMoneyGiven_WhenIncorrectAmountIsTendered_ThenMessageStatingNotEnoughMoneyForDrinkTypeIsReturned(string input, string result)
        {
            var inputHandler = new InputHandler(input);

            var actual = inputHandler.ProcessInput();
            
            Assert.Equal(result, actual);
        }
        
        [Theory] // test case for OJ in the chilled section
        [InlineData("O:0:0.6", "Your oj is being prepared")]

        public void GivenCheckMoneyGiven_WhenCorrectMoneyIsGiven_ThenOJIsMade(string input, string result)
        {
            var inputHandler = new InputHandler(input);

            var actual = inputHandler.ProcessInput();
            
            Assert.Equal(result, actual);
        }
        
        [Theory] // add extra hot option to coffee, tea and chocolate
        
        [InlineData("Ch:0:0.6", "The drink maker will make one extra hot coffee with no sugar")]
        [InlineData("Ch:1:0.6", "The drink maker will make one extra hot coffee with one sugar and a stick")]
        [InlineData("Ch:2:0.6", "The drink maker will make one extra hot coffee with two sugars and a stick")]
        [InlineData("Th:0:0.6", "The drink maker will make one extra hot tea with no sugar")]
        [InlineData("Th:1:0.6", "The drink maker will make one extra hot tea with one sugar and a stick")]
        [InlineData("Th:2:0.6", "The drink maker will make one extra hot tea with two sugars and a stick")] 
        [InlineData("Hh:0:0.6", "The drink maker will make one extra hot chocolate with no sugar")]
        [InlineData("Hh:1:0.6", "The drink maker will make one extra hot chocolate with one sugar and a stick")]
        [InlineData("Hh:2:0.6", "The drink maker will make one extra hot chocolate with two sugars and a stick")] 
        
        
        public void GivenCheckMoneyGiven_WhenCustomerSelectsExtraHot_ThenExtraHotDrinkIsMade(string input, string result)
        {
            InputHandler inputHandler = new InputHandler(input);
            var actual = inputHandler.ProcessInput();

            Assert.Equal(result, actual);
        }
        
        [Theory] // check counter after drink ordered
        
        [InlineData("C:0:0.6", 1)]
        [InlineData("C:1:0.6", 2)]
        [InlineData("C:2:0.6", 3)]    
        [InlineData("T:1:0.4", 1)]
        [InlineData("H:2:0.5", 1)]
        [InlineData("O:0:0.6", 1)]
        
        public void GivenProcessOrder_WhenAnOrderIsProcessed_ThenDrinkCounterIncrements(string input, int result)
        {
            InputHandler inputHandler = new InputHandler(input);
            var drinkLetter = inputHandler.GetDrinkLetter(input.Split(':')[0]);
            var drinkType = inputHandler.GetDrinkType(drinkLetter);
            
            var actual = SalesTracker.GetIndividualDrinkTypeCounterValue(drinkType);
                
            Assert.Equal(result, actual);
        }
    }
}