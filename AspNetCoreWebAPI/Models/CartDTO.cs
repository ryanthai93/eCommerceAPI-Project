﻿namespace AspNetCoreWebAPI.Models
{
    public class CartDTO
    {
        public int id { get; set; }
        public string CartID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }

    }
}
