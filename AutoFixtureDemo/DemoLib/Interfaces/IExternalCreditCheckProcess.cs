namespace DemoLib.Interfaces
{
    using Model;

    public interface IExternalCreditCheckProcess
    {
        CreditCheckResult ScoreCustomer(string socialSecurityNumber);
    }
}