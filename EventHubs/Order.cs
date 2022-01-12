using System.Text.Json;

namespace EventHubs
{
    public class Order
    {
        public string OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string DiscountCategory { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

