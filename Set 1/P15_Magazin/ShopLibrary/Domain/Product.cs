namespace P15_Magazin.ShopLibrary.Domain;

public class Product(string name, decimal price, int stock)
{
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int Stock { get; set; } = stock;
    
    public override string ToString()
    {
        return $"Product: {Name}, Price: {Price:C}, Stock: {Stock}";
    }
}