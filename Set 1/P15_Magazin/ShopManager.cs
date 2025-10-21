using System.Collections.Immutable;
using P15_Magazin.ShopLibrary;
using P15_Magazin.ShopLibrary.Controller;

namespace P15_Magazin;

public class ShopManager
{
    private bool _running = true;
    private readonly ShopController _controller = new();

    public ShopManager()
    {
        Main();
    }

    private void Main()
    {
        Console.WriteLine("Shop Manager is running(- to stop it)");
        while (_running)
        {
            Console.WriteLine(
                "Operations: a - add new product, b - buy product, r - remove product, s - show products, v - show stock value");
            if (!TakeInput("operation", out var operation))
                continue;
            var bankAction = ParseOperation(operation);
            if (!bankAction.HasValue)
            {
                Console.WriteLine("Invalid operation");
                continue;
            }

            bool isSuccess;
            string? msg;
            switch (bankAction)
            {
                case ShopActions.AddProduct:
                    (isSuccess, msg) = AddProduct();
                    if (!isSuccess)
                        Console.WriteLine($"Operation failed: {msg}");
                    else
                        Console.WriteLine("Product added");
                    break;
                case ShopActions.BuyProduct:
                    (isSuccess, msg) = BuyProduct();
                    if (!isSuccess)
                        Console.WriteLine($"Operation failed: {msg}");
                    else
                        Console.WriteLine("Product bought");
                    break;
                case ShopActions.RemoveProduct:
                    (isSuccess, msg) = RemoveProduct();
                    if (!isSuccess)
                        Console.WriteLine($"Operation failed: {msg}");
                    else
                        Console.WriteLine("Product removed");
                    break;
                case ShopActions.ShowProducts:
                    (_, msg) = ShowProducts();
                    Console.WriteLine("Products:\n" + msg);
                    break;
                case ShopActions.ShowStockValue:
                    (_, msg) = ShowStockValue();
                    Console.WriteLine("Stock value: " + msg);
                    break;
                default:
                    Console.WriteLine("Invalid shop action");
                    break;
            }
        }
    }

    private ShopActions? ParseOperation(string operation)
    {
        return operation.ToLower() switch
        {
            "a" => ShopActions.AddProduct,
            "b" => ShopActions.BuyProduct,
            "r" => ShopActions.RemoveProduct,
            "s" => ShopActions.ShowProducts,
            "v" => ShopActions.ShowStockValue,
            _ => null
        };
    }

    private (bool isSuccess, string? msg) AddProduct()
    {
        if (!TakeInput("product name", out var name))
            return (false, "Invalid product name");
        if (!TakeInput("product price", out var priceInput))
            return (false, "Invalid product price");
        if (!decimal.TryParse(priceInput, out var price))
            return (false, "Invalid price number format");
        if (!TakeInput("product stock", out var stockInput))
            return (false, "Invalid product stock number");
        if (!int.TryParse(stockInput, out var stock))
            return (false, "Invalid stock number format");
        _controller.AddProduct(name, price, stock);

        return (true, null);
    }

    private (bool isSuccess, string? msg) BuyProduct()
    {
        if (!TakeInput("product name", out var name))
            return (false, "Invalid product name");
        if (!TakeInput("product quantity", out var quantityInput))
            return (false, "Invalid product quantity number");
        if (!int.TryParse(quantityInput, out var quantity))
            return (false, "Invalid quantity number format");

        var isSuccess = _controller.BuyProduct(name, quantity);
        if (!isSuccess)
            return (false, "Product not found or insufficient stock");

        return (true, null);
    }

    private (bool isSuccess, string? msg) RemoveProduct()
    {
        if (!TakeInput("product name", out var name))
            return (false, "Invalid product name");

        _controller.RemoveProduct(name);

        return (true, null);
    }

    private (bool isSuccess, string? msg) ShowProducts()
    {
        var products = _controller.GetProducts().ToList();
        if (products.Count == 0)
            return (true, "No products found");

        var str = string.Join('\n', products);
        return (true, str);
    }

    private (bool isSuccess, string? msg) ShowStockValue()
    {
        var price = _controller.GetShopStock();
        return (true, $"{price}");
    }

    private bool TakeInput(string variable, out string input)
    {
        if (!InputHelper.TakeInput(variable, out input))
            return false;

        if (CheckProgramEnd(input))
            return false;
        return true;
    }

    private bool CheckProgramEnd(string input)
    {
        if (input != "-") return false;
        _running = false;
        return true;
    }
}