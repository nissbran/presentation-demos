namespace SqlListener
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Security.Permissions;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        private static string ConnectionString =
            "Data Source=.;Database=EF7Context;Persist Security Info=false;Integrated Security = false;User Id=Listener;Password=listener";

        public static List<Item> ItemsFound { get; } = new List<Item>();

        public static ConcurrentQueue<Chunk> ChunkQueue = new ConcurrentQueue<Chunk>();

        private static DateTime Checkpoint;

        private static CancellationTokenSource timerTokenSource;

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            SqlClientPermission clientPermission = new SqlClientPermission(PermissionState.Unrestricted);
            clientPermission.Demand();

            SqlDependency.Start(ConnectionString);

            Checkpoint = new DateTime(2016, 03, 03);
            Checkpoint = DateTime.Now;

            RegisterForChange();

            Console.ReadLine();

            SqlDependency.Stop(ConnectionString);
        }

        private static void RegisterForChange()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            try
            {
                var syncCommand = new SqlCommand("SELECT MAX(UpdatedOn) FROM dbo.BankCustomer WHERE UpdatedOn >= @time", connection);
                syncCommand.Parameters.AddWithValue("time", Checkpoint);

                DateTime nextSyncPoint = syncCommand.ExecuteScalar() as DateTime? ?? Checkpoint;

                var subscriptionCommand = new SqlCommand("SELECT UpdatedOn FROM dbo.BankCustomer WHERE UpdatedOn >= @time", connection);
                subscriptionCommand.Parameters.AddWithValue("time", nextSyncPoint);

                var dependency = new SqlDependency(subscriptionCommand);
                dependency.OnChange += OnNotificationChange;

                subscriptionCommand.ExecuteNonQuery();

                var chunk = new Chunk(Checkpoint, nextSyncPoint);

                GetItems(chunk);

                timerTokenSource = new CancellationTokenSource();
                var token = timerTokenSource.Token;

                Task.Run(async () =>
                {
                    await Task.Delay(2000, token);

                    if (!token.IsCancellationRequested)
                    {
                        GetItems(new Chunk(chunk.From, chunk.To.AddSeconds(2)));
                    }

                }, token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private static void OnNotificationChange(object sender, SqlNotificationEventArgs e)
        {
            timerTokenSource.Cancel();

            Console.WriteLine($"Change received {e.Type}, {e.Info}");

            RegisterForChange();
        }

        private static void GetItems(Chunk chunk)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            try
            {
                var readChunchCommand =
                    new SqlCommand(
                        "SELECT CustomerId, RegistrationNumber, UpdatedOn FROM dbo.BankCustomer WHERE UpdatedOn >= @from and UpdatedOn <= @to",
                        connection);
                readChunchCommand.Parameters.AddWithValue("from", chunk.From);
                readChunchCommand.Parameters.AddWithValue("to", chunk.To);

                var reader = readChunchCommand.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);

                        if (!ItemsFound.Exists(item => item.CustomerId == id))
                            ItemsFound.Add(new Item
                            {
                                CustomerId = id,
                                RegistrationNumber = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                UpdatedOn = reader.GetDateTime(2)
                            });
                    }

                    if (ItemsFound.Any())
                        Checkpoint = ItemsFound.Max(item => item.UpdatedOn);
                }
                finally
                {
                    Console.WriteLine($"Number of items atm: {ItemsFound.Count}");
                    reader.Close();
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }

    public class Item
    {
        public long CustomerId { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime UpdatedOn { get; set; }
    }

    public class Chunk
    {
        public DateTime From { get; }

        public DateTime To { get; }

        public Chunk(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
