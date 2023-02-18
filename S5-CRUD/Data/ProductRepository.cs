using S5_CRUD.Model;

namespace S5_CRUD.Data
{
    public static class ProductRepository
    {
        public static List<Product>? Products { get; set; }

        public static void Add(Product product)
        {
            if(Products == null)
                Products = new List<Product>();
            Products.Add(product);
        }

        public static void Remove(Product product)
        {
            if (Products != null)
            {
                Products.Remove(product);
            }
        }

        public static Product GetBy(string code)
        {
            return Products.FirstOrDefault(p => p.Code == code);
        }
    }
}
