using ConsoleApp;

namespace ClassLibrary;

public class CreditCardPaymentProcessor : IPaymentProcessor
{
    private readonly ILogger _logger;

    public CreditCardPaymentProcessor(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public decimal ProcessPayment(decimal amount, string currency)
    {
        try
        {
            _logger.LogInfo($"Processing payment of {amount} {currency}");

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be positive", nameof(amount));
            }

            return amount;
        }
        catch (Exception ex)
        {
            _logger.LogError("Payment processing failed", ex);
            throw;
        }
    }

    public bool ValidatePaymentMethod(string? paymentMethod)
    {
        string method = string.Empty;
        
        if (paymentMethod != null)
        {
            method = paymentMethod;
        }

        return method.Equals("CreditCard", StringComparison.OrdinalIgnoreCase) ||
               method.Equals("DebitCard", StringComparison.OrdinalIgnoreCase);
    }
}