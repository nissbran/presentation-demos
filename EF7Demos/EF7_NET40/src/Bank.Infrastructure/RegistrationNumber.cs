namespace Bank.Infrastructure
{
    public class RegistrationNumber
    {
        public string Value { get; protected set; }

        public RegistrationNumber(string registrationNumber)
        {
            Value = registrationNumber;
        }

        public static implicit operator string (RegistrationNumber registrationNumber)
        {
            return registrationNumber?.Value;
        }

        public int CompareTo(RegistrationNumber other)
        {
            return string.CompareOrdinal(Value, other.Value);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents this instance.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that represents this instance.
        /// 
        /// </returns>
        public override string ToString()
        {
            return Value ?? string.Empty;
        }
    }
}
