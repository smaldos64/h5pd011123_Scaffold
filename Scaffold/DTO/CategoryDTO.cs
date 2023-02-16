using Scaffold.Models;

namespace Scaffold.DTO
{
    public class CategoryDTOForSave
    {
        public string ?CategoryName { get; set; }
    }

    public class CategoryDTOForUpdate : CategoryDTOForSave
    {
        public int CategoryID { get; set; }
    }

    public class CategoryDTO : CategoryDTOForUpdate
    {
        public virtual ICollection<ProductCategoryDTO> Products { get; set; }
               = new List<ProductCategoryDTO>();
    }
    public class CategoryProductDTO
    {
        public int CategoryID { get; set; }
        public string ?CategoryName { get; set; }
    }
}
