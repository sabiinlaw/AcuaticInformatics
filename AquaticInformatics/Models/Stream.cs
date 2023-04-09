using System.ComponentModel;

namespace RiverFlow.Helper
{
    public class Stream
    {
        [Description("Stream width in feets")]
        public double Width { get; set; }

        [Description("Stream cross-section sections")]
        public List<StreamSection>? Sections { get; set; }
    }

    public class StreamSection
    {
        [Description("Stream cross-section section depth in feets")]
        public double Depth { get; set; }

        [Description("Stream cross-section section velocity in feet/s")]
        public double Velocity { get; set; }
    }
}
