namespace DockerDemo.Consumer
{
    using System;
    using System.Runtime.Loader;

    public class Program
    {
        private static bool _programIsStopping = false;

        public static void Main(string[] args)
        {
            AssemblyLoadContext.Default.Unloading += MethodInvokedOnSigTerm;
            Console.CancelKeyPress += CancelKeyPressed;

            while (!_programIsStopping)
            {
                System.Threading.Thread.Sleep(5000);

                Console.WriteLine("The consumer is alive!");
            }
        }

        private static void CancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            CleanShutdown();
        }

        private static void MethodInvokedOnSigTerm(AssemblyLoadContext obj)
        {
            CleanShutdown();
        }

        private static void CleanShutdown()
        {
            Console.WriteLine("Exiting...");
            _programIsStopping = true;
        }
    }
}
