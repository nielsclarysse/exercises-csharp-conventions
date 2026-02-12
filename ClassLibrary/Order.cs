using System.Text;
using ClassLibrary;

public class Order
{
    private readonly int _orderId;
    private readonly DateTime _orderDate;
    private OrderStatus _status;
    private readonly List<OrderItem> _items;

    public int OrderId => _orderId;
    
    public DateTime OrderDate => _orderDate;

    public OrderStatus Status
    {
        get => _status;
        set => _status = value;
    }

    public decimal TotalAmount
    {
        get { return _items.Sum(item => item.Quantity * item.UnitPrice); }
    }

    public Order(int orderId)
    {
        _orderId = orderId;
        _orderDate = DateTime.Now;
        _status = OrderStatus.Pending;
        _items = new List<OrderItem>();
    }

    public void AddItem(string productName, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name cannot be empty", nameof(productName));
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be positive", nameof(quantity));
        }

        if (unitPrice < 0)
        {
            throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));
        }

        var orderItem = new OrderItem(productName, quantity, unitPrice);
        _items.Add(orderItem);
    }

    public void ProcessOrder()
    {
        if (_status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Only pending orders can be processed");
        }

        _status = OrderStatus.Processing;
    }

    public string GetSummary()
    {
        var summary = new StringBuilder();

        summary.AppendLine($"Order #{_orderId}");
        summary.AppendLine($"Date: {_orderDate:yyyy-MM-dd HH:mm}");
        summary.AppendLine($"Status: {_status}");
        summary.AppendLine($"Items: {_items.Count}");
        summary.AppendLine($"Total: ${TotalAmount:N2}");

        return summary.ToString();
    }
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}