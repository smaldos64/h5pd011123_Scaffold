using Scaffold.Models;

namespace Scaffold.DTO
{
    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public virtual ICollection<ProductCategoryDTO> Products { get; set; }
               = new List<ProductCategoryDTO>();
    }
    public class CategoryProductDTO
    {
        public int CategoryID { get; set; }
        public string ?CategoryName { get; set; }
    }
}
