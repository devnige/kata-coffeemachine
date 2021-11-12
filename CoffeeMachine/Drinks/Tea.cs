namespace CoffeeMachine.Drinks
{
    public class Tea : IDrink
    {
        private string _name = "tea";
        private decimal _price = 0.4M;
        private int _sugarAmount;
        private bool _isExtraHot;

        public Tea(int sugarAmount, bool isExtraHot)
        {
            _sugarAmount = sugarAmount;
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
            return _sugarAmount;
        }
    }
}