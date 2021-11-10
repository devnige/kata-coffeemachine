namespace CoffeeMachine
{
    public class Drink
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Sugar { get; private set; }

        public int AddSugar(int numberOfSugars)
        {
            if (Sugar + numberOfSugars <= 2)
            {
                Sugar += numberOfSugars;
            }
            return Sugar;
        }
    }
}