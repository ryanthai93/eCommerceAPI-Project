using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebAPI.Models
{
    public class CartUpdateDTO
    {
        //public int id { get; set; }
        public string CartID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }

    }
}
