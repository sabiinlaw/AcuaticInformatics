using Microsoft.Extensions.DependencyInjection;
using RiverStream.Helper;
using RiverStream.Service;
using Stream = RiverStream.Helper.Stream;

static double ReadDouble(string message)
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

var services = new ServiceCollection();
services.AddSingleton<IStreamService, StreamService>();
var serviceProvider = services.BuildServiceProvider();
var streamService = serviceProvider.GetService<IStreamService>();

var width = ReadDouble($"Enter the stream width (in feet): ");
var sectionsQuantity = ReadDouble($"Enter the stream cross-section sections quantity: ");

var stream = new Stream()
{
    Width = width,
    Sections = new List<StreamSection>()
};

for (int i = 0; i < sectionsQuantity; i++)
{
    var depth = ReadDouble($"Enter the depth measurement at section {i + 1} (in feet): ");
    var velocity = ReadDouble($"Enter the velocity measurement at section {i + 1} (in feet per second): ");
    stream.Sections.Add(new StreamSection() { Depth = depth, Velocity = velocity });
}

var volume = streamService?.CalculateVolume(stream);
Console.WriteLine("The total volume of water flowing through the stream is {0} cubic feets per second.", volume);