namespace CoffeeMachine.Ingredients
{
    public class Sugar
    {
        private int _quantity;

        public Sugar(int quantity)
        {
            _quantity = quantity;
        }

        public string GetSugarAttribute() => _quantity switch
        {
            0 => " with no sugar",
            1 => " with one sugar and a stick",
            2 => " with two sugars and a stick",
            _ => " with no sugar"
        };
    }
}