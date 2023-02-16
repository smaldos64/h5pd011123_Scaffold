using Scaffold.Models;

namespace Scaffold.DTO
{
    public class ProductDTOForSave
    {
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
    }
    public class ProductDTOForUpdate : ProductDTOForSave
    {
        public int ProductID { get; set; }
    }

    //public class ProductDTO : ProductDTOForUpdate
    //{

    //}

    public class ProductDTOPlusCategory : ProductDTOForUpdate
    {
        public virtual CategoryProductDTO? Category { get; set; }
    }

    public class ProductCategoryDTO
    {
        public int ProductID { get; set; }
        public string ?ProductName { get; set; }
    }
    
}
