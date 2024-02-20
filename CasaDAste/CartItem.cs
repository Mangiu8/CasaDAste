namespace CasaDAste
{
    public class CartItem
    {
        public string ProductName { get; private set; }
        public string ProductRace { get; private set; }
        public string ProductDescription { get; private set; }
        public decimal Price { get; private set; }

        public string Format()
        {
            return Price.ToString("C2");
        }

    }
}