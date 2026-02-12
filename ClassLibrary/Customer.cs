namespace ClassLibrary;

public class Customer
{
    public const int MaxOrdersPerDay = 10;
    public const decimal DefaultDiscountRate = 0.05m;

    private string _firstName;
    private string _lastName;
    private string _email;
    private readonly DateTime _registrationDate;
    private MembershipLevel _membershipLevel;
    private readonly List<Order> _orders;
        
    public string FirstName
    {
        get {
            return _firstName;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("First name cannot be empty");
            }
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Last name cannot be empty");
            }
            _lastName = value;
        }
    }
        
    public string Email
    {
        get => _email;
        set
        {
            if (!IsValidEmail(value))
            {
                throw new ArgumentException("Invalid email format");
            }
            _email = value;
        }
    }

    public DateTime RegistrationDate
    {
        get
        {
            return _registrationDate;
        }
    }
        
    public MembershipLevel MembershipLevel
    {
        get => _membershipLevel;
        set => _membershipLevel = value;
    }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public int YearsSinceRegistration => DateTime.Now.Year - _registrationDate.Year;

    public bool IsPremiumMember => _membershipLevel == MembershipLevel.Gold || 
                                   _membershipLevel == MembershipLevel.Platinum;
        
    public Customer(string firstName, string lastName, string email)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _registrationDate = DateTime.Now;
        _membershipLevel = MembershipLevel.Bronze;
        _orders = new List<Order>();
    }

    public Customer(string firstName, string lastName, string email, MembershipLevel membershipLevel)
        : this(firstName, lastName, email)
    {
        _membershipLevel = membershipLevel;
    }
        
    public void AddOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        if (_orders.Count >= MaxOrdersPerDay)
        {
            throw new InvalidOperationException($"Maximum {MaxOrdersPerDay} orders per day exceeded");
        }

        _orders.Add(order);
    }

    public decimal GetTotalSpent()
    {
        decimal sum = 0;
        foreach (Order order in _orders)
        {
            sum += order.TotalAmount;
        }
        return sum;
    }

    public List<Order> GetOrdersByStatus(OrderStatus status)
    {
        return _orders
            .Where(order => order.Status == status)
            .OrderByDescending(order => order.OrderDate)
            .ToList();
    }

    public decimal GetDiscountRate()
    {
        return _membershipLevel switch
        {
            MembershipLevel.Bronze => 0.05m,
            MembershipLevel.Silver => 0.10m,
            MembershipLevel.Gold => 0.15m,
            MembershipLevel.Platinum => 0.20m,
            _ => 0.00m
        };
    }

    public string GenerateReport()
    {
        string report = "";

        report += "=== Customer Report ===" + Environment.NewLine;
        report += "Name: " + FullName + Environment.NewLine;
        report += "Email: " + Email + Environment.NewLine;
        report += "Membership: " + _membershipLevel + Environment.NewLine;
        report += "Member Since: " + _registrationDate.ToString("yyyy-MM-dd") + Environment.NewLine;
        report += "Years Active: " + YearsSinceRegistration.ToString() + Environment.NewLine;
        report += "Total Orders: " + _orders.Count.ToString() + Environment.NewLine;
        report += "Total Spent: $" + GetTotalSpent().ToString("N2") + Environment.NewLine;
        report += "Discount Rate: " + GetDiscountRate().ToString("P0") + Environment.NewLine;

        return report;
    }
        
    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return email.Contains("@") && email.Contains(".");
    }

    public void UpgradeMembership()
    {
        switch (_membershipLevel)
        {
            case MembershipLevel.Bronze:
                _membershipLevel = MembershipLevel.Silver;
                break;
            case MembershipLevel.Silver:
                _membershipLevel = MembershipLevel.Gold;
                break;
            case MembershipLevel.Gold:
                _membershipLevel = MembershipLevel.Platinum;
                break;
            default:
                break;
        }
    }

    public override string ToString()
    {
        return $"Customer: {FullName} ({Email})";
    }
}

public enum MembershipLevel
{
    Bronze,
    Silver,
    Gold,
    Platinum
}