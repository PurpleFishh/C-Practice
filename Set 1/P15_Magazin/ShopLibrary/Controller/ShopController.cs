using System.Collections.Immutable;
using P15_Magazin.ShopLibrary.Domain;
using P15_Magazin.ShopLibrary.Service;

namespace P15_Magazin.ShopLibrary.Controller;

public class ShopController
{
    private readonly IShopService _shopService = new ShopService();

    public void AddProduct(string name, decimal price, int stock)
    {
        _shopService.AddProduct(name, price, stock);
    }

    public bool BuyProduct(string name, int quantity = 1)
    {
        return _shopService.BuyProduct(name, quantity) != null;
    }

    public void RemoveProduct(string name)
    {
        _shopService.RemoveProduct(name);
    }

    public IEnumerable<Product> GetProducts()
    {
        return _shopService.GetProducts();
    }

    public decimal GetShopStock()
    {
        return _shopService.GetShopStockValue();
    }
}