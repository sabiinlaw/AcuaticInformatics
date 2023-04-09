using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RiverFlow.Helper;
using RiverFlow.Service;
using Stream = RiverFlow.Helper.Stream;

namespace RiverFlowExecutor
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var service = host.Services.GetRequiredService<IStreamService>();
            var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

            var stream = InitStream();
            var volume = service.CalculateVolume(stream);

            logger.LogInformation("The total volume of water flowing through the stream is {0} cubic feets per second.", volume);
            return host.RunAsync();

        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<IStreamService, StreamService>();
            })
            .ConfigureLogging((_, logging) =>
            {
                logging.AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter(nameof(Program), LogLevel.Information)
                    .AddFilter(nameof(StreamService), LogLevel.Information)
                    .AddConsole();
            });

        private static Stream InitStream()
        {
            var streamWidth = ReadDouble($"Enter the stream width (in feet): ");
            var sectionsQuantity = ReadDouble($"Enter the stream cross-section sections quantity: ");

            var stream = new Stream()
            {
                Width = streamWidth,
                Sections = new List<StreamSection>()
            };

            for (int i = 0; i < sectionsQuantity; i++)
            {
                var depth = ReadDouble($"Enter the depth measurement at section {i + 1} (in feet): ");
                var velocity = ReadDouble($"Enter the velocity measurement at section {i + 1} (in feet per second): ");
                stream.Sections.Add(new StreamSection() { Depth = depth, Velocity = velocity });
            }

            return stream;
        }

        private static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out double result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }
    }
}