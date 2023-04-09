using RiverStream.Helper;
using Stream = RiverStream.Helper.Stream;

namespace RiverStream.Service
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
