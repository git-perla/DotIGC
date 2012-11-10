namespace DotIGC
{
    using DotIGC.Records;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IgcDocument
    {
        List<Record> records;

        IEnumerable<Record> Records
        {
            get { return this.records; }
        }
    }
}
