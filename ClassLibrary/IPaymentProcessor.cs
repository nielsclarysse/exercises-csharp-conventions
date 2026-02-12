namespace ClassLibrary;

public interface IPaymentProcessor
{
    decimal ProcessPayment(decimal amount, string currency);
    bool ValidatePaymentMethod(string paymentMethod);
}