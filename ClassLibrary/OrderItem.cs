namespace ClassLibrary;

public class OrderItem
{
    public string ProductName { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice;

    public OrderItem(string productName, int quantity, decimal unitPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}