namespace CoffeeMachine.Drinks
{
    public class Coffee : IDrink
    {
        private string _name = "coffee";
        private decimal _price = 0.6M;
        private readonly int _sugarQuantity;
        private bool _isExtraHot;

        public Coffee(int sugarQuantity, bool isExtraHot)
        {
            _sugarQuantity = sugarQuantity;
            _isExtraHot = isExtraHot;
        }

        public string GetName()
        {
            return _name;
        }
        public decimal GetPrice()
        {
            return _price;
        }

        public int GetSugar()
        {
            return _sugarQuantity;
        }
    }
}