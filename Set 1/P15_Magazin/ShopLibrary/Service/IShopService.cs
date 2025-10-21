using P15_Magazin.ShopLibrary.Domain;

namespace P15_Magazin.ShopLibrary.Service;

public interface IShopService
{
    public Product AddProduct(string name, decimal price, int stock);
    public Product? BuyProduct(string name, int quantity);
    public bool RemoveProduct(string name);
    public IEnumerable<Product> GetProducts();
    public decimal GetShopStockValue();
}