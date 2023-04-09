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
        public double CalculateVolume(Stream stream, bool useCorrectionFactor = false)
        {
            return StreamCalculationHelper.CalculateVolume(stream, useCorrectionFactor);
        }
    }
}
