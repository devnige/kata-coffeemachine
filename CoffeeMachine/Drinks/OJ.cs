namespace CoffeeMachine
{
    public class OJ : IDrink
    {
        private string _name = "oj";
        private decimal _price = 0.6M;
        
        public string GetName()
        {
            return _name;
        }

        public decimal GetPrice()
        {
                return _price;
        }
    }
}