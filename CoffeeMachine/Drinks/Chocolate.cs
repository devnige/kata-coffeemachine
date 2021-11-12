namespace CoffeeMachine.Drinks
{
    public class Chocolate : IDrink
    {
        private string _name = "chocolate";
        private decimal _price = 0.5M;
        private int _sugarAmount;
        private bool _isExtraHot;

        public Chocolate(int sugarAmount, bool isExtraHot)
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