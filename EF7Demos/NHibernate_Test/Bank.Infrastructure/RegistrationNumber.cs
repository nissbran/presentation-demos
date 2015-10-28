namespace Bank.Infrastructure
{
    public class RegistrationNumber
    {
        public string Value { get; protected set; }

        public RegistrationNumber(string registrationNumber)
        {
            Value = registrationNumber;
        }

        protected RegistrationNumber()
        { 
        }

        public static implicit operator string (RegistrationNumber registrationNumber)
        {
            return registrationNumber?.Value;
        }

        public int CompareTo(RegistrationNumber other)
        {
            return string.CompareOrdinal(Value, other.Value);
        }

        public override string ToString()
        {
            return Value ?? string.Empty;
        }
    }
}
