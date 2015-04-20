namespace DemoLib.Interfaces
{
    using Model.Commands;

    interface IMyServiceBus
    {
        void Transmit(Command command);
    }

}
