namespace RiverStream.Helper
{
    public class StreamCalculationHelper
    {
        private static readonly double velocityCorrectionFactor = 0.8;
        public static double CalculateVolume(Stream stream, bool useCorrectionFactor = false)
        {
            if (stream.Sections == null || !stream.Sections.Any())
                throw new ArgumentException("Stream must have at least one section.");

            double sectionWidth = stream.Width / stream.Sections.Count;
            double totalArea = 0;
            double totalFlow = 0;

            if (useCorrectionFactor)
                CorrectSectionsSettings(stream);

            for (int i = 0; i < stream.Sections.Count; i++)
            {
                double sectionDepth = stream.Sections[i].Depth;
                double sectionVelocity = stream.Sections[i].Velocity;
                
                double sectionArea = sectionDepth * sectionWidth;
                double sectionFlow = sectionArea * sectionVelocity;
                totalArea += sectionArea;
                totalFlow += sectionFlow;
            }

            double totalVolume = totalFlow;
            return totalVolume;
        }

        private static void CorrectSectionsSettings(Stream stream)
        {
            if (stream.Sections == null)
                return;

            var newSections = new List<StreamSection>();
            for (int i = 0; i < stream.Sections.Count; i++)
            {
                double prevDepth = i == 0 ? 0 : stream.Sections[i - 1].Depth;
                double nextDepth = i == stream.Sections.Count - 1 ? 0 : stream.Sections[i + 1].Depth;

                //The depth correction is made because in reality, the depth of the stream can vary within a section,
                //with some parts of the section being deeper or shallower than others.
                var correctedDepth = (prevDepth + stream.Sections[i].Depth + nextDepth) / 3;

                //The velocity correction factor has been added to adjust for the fact that water velocity at the surface is faster than water
                //velocity closer to the bottom of a stream.Use this factor to get a more accurate stream flow calculation.
                var correctedVelocity = stream.Sections[i].Velocity * velocityCorrectionFactor;

                newSections.Add(new StreamSection() { Depth = correctedDepth, Velocity = correctedVelocity });
            }

            stream.Sections = newSections;
        }
    }
}
