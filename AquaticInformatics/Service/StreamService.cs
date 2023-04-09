using Microsoft.Extensions.Logging;
using RiverFlow.Helper;
using Stream = RiverFlow.Helper.Stream;

namespace RiverFlow.Service
{
    public interface IStreamService
    {
        public double CalculateVolume(Stream stream, bool useCorrectionFactor = false);
    }

    public class StreamService : IStreamService
    {
        private readonly ILogger<StreamService> _logger;
        public StreamService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<StreamService>();
        }

        public double CalculateVolume(Stream stream, bool useCorrectionFactor = false)
        {
            var result = StreamCalculationHelper.CalculateVolume(stream, useCorrectionFactor);
            _logger.LogInformation($"Calculated volume: {result}");

            return result;
        }
    }
}
