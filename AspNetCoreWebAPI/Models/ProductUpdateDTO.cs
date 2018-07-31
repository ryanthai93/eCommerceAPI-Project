using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebAPI.Models
{
    public class ProductUpdateDTO
    {
        //public int ProductID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public int SubCategoryID { get; set; }
        public char Featured { get; set; }
        public int MainCategoryID { get; set; }

    }
}
