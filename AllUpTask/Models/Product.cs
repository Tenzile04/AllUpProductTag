using System.ComponentModel.DataAnnotations.Schema;

namespace AllUpTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Tax { get; set; }
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        
        public List<ProductTag>? ProductTags { get; set; } = new List<ProductTag>();
        [NotMapped]
        public List<int> TagIds { get; set; }
    }
}
