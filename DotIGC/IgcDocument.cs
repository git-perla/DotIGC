namespace DotIGC
{
    using DotIGC.Records;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IgcDocument
    {
        List<IRecord> records;

        public IgcDocument(IEnumerable<IRecord> records)
        {
            this.records = records.ToList();
        }

        IEnumerable<IRecord> Records
        {
            get { return this.records; }
        }
    }
}
