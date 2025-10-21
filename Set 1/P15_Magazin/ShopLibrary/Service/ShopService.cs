using P15_Magazin.ShopLibrary.Domain;
using P15_Magazin.ShopLibrary.Repository;

namespace P15_Magazin.ShopLibrary.Service;

public class ShopService : IShopService
{
    private readonly IRepository<Product> _repository = new InMemoryShopRepository();

    public Product AddProduct(string name, decimal price, int stock)
    {
        var product = new Product(name, price, stock);
        _repository.Add(product);
        return product;
    }

    public Product? BuyProduct(string name, int quantity)
    {
        var product = _repository.GetByName(name);
        if (product == null || product.Stock < quantity)
            return product;

        product.Stock -= quantity;
        product = _repository.Upsert(product);

        return product;
    }

    public bool RemoveProduct(string name) => _repository.DeleteByName(name);

    public IEnumerable<Product> GetProducts() => _repository.GetAll();

    public decimal GetShopStockValue() => _repository.GetAll().Sum(p => p.Price * p.Stock);
}