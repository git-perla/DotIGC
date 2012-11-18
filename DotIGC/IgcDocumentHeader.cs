namespace DotIGC
{
    using System;
    
    public class IgcDocumentHeader
    {
        public DateTime Date { get; set; }
        
        public string PilotInCharge { get; set; }

        public string CrewMember { get; set; }

        public string GliderType { get; set; }

        public string GliderId { get; set; }

        public string CompetitionId { get; set; }

        public string CompetitionClass { get; set; }

        public string FlightRecorder { get; set; }

        public string GPS { get; set; }

        public string FirmwareVersion { get; set; }

        public string HardwareVersion { get; set; }

        public int GpsDatum { get; set; }

        public string PressureAltitudeSensor { get; set; }

        public int Accuracy { get; set; }
    }
}
