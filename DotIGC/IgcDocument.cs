namespace DotIGC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DotIGC.Records;

    public class IgcDocument
    {
        List<Record> records;

        IEnumerable<Record> Records
        {
            get { return this.records; }
        }

        public static IgcDocumentHeader LoadHeader(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
                return LoadHeader(stream);
        }

        public static IgcDocumentHeader LoadHeader(Stream stream)
        {
            var mapper = GetHeaderMapper();
            var header = new IgcDocumentHeader();
            var container = new Container<RecordType, IRecordReader>();
            container.Bind(RecordType.H).To<HeaderRecordReader>();

            using (var reader = new IgcReader(stream, new RecordReader(container)))
            {
                Record record;
                while (reader.Read(out record))
                {
                    Action<IgcDocumentHeader, Record> action;
                    if (mapper.TryGetValue(record.ThreeLetterCode, out action))
                    {
                        action(header, record);
                    }
                }
            }

            return header;
        }

        static Dictionary<ThreeLetterCode, Action<IgcDocumentHeader, Record>> GetHeaderMapper()
        {
            var mapper = new Dictionary<ThreeLetterCode, Action<IgcDocumentHeader, Record>>();

            mapper[ThreeLetterCode.DTE] = (header, record) =>
            {
                var day = record.Text.Substring(5, 2);
                var month = record.Text.Substring(7, 2);
                var year = record.Text.Substring(9, 2);

                header.Date = DateTime.Parse(year + "-" + month + "-" + day);
            };

            mapper[ThreeLetterCode.FXA] = (header, record) =>
            {
                var number = record.Text.Substring(5, 3);
                header.Accuracy = int.Parse(number);
            };

            mapper[ThreeLetterCode.PLT] = (header, record) =>
            {
                header.PilotInCharge = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.CM2] = (header, record) =>
            {
                header.CrewMember = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.GID] = (header, record) =>
            {
                header.GliderId = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.GTY] = (header, record) =>
            {
                header.GliderType = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.CID] = (header, record) =>
            {
                header.CompetitionId = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.CCL] = (header, record) =>
            {
                header.CompetitionClass = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.FTY] = (header, record) =>
            {
                header.FlightRecorder = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.GPS] = (header, record) =>
            {
                header.GPS = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.RFW] = (header, record) =>
            {
                header.FirmwareVersion = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.RHW] = (header, record) =>
            {
                header.HardwareVersion = ParseHeaderString(record.Text);
            };

            mapper[ThreeLetterCode.DTM] = (header, record) =>
            {
                var number = record.Text.Substring(5, 3);
                header.GpsDatum = int.Parse(number);
            };

            mapper[ThreeLetterCode.PRS] = (header, record) =>
            {
                header.PressureAltitudeSensor = ParseHeaderString(record.Text);
            };

            return mapper;
        }

        static string ParseHeaderString(string text)
        {
            var tokens = text.Split(new[] { ':' });
            return tokens.Length < 2 ? string.Empty : tokens[1].Trim();
        }
    }
}
