namespace DotIGC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DotIGC.Records;
    
    public class GeoPositionCollection
    {
        public GeoPositionCollection(IEnumerable<Record> records)
        {
            var fixRecords = records.OfType<FixRecord>();
            GeoPositions = fixRecords.Zip(fixRecords.Skip(1), CreateGeoPosition)
                                     .ToList();
        }

        public List<GeoPosition> GeoPositions { get; private set; }

        GeoPosition CreateGeoPosition(FixRecord a, FixRecord b)
        {
            double seconds = (b.TimeUTC - a.TimeUTC).TotalSeconds;
            double distance = GeoPosition.Distance(a.Latitude, a.Longitude, b.Latitude, b.Longitude);
            double speed = (distance / seconds) * 3.6;

            return new GeoPosition(b.TimeUTC, b.Longitude, b.Latitude, b.PressureAltitude, b.GnssAltitude, double.NaN, speed);
        }
    }
}
