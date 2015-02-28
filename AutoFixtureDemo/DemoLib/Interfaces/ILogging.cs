namespace DemoLib.Interfaces
{
    public interface ILogging
    {
        void Info(string message);

        void Warning(string message);

        void Error(string message);
    }
}