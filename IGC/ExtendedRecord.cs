using System;
using System.Collections.Generic;

namespace Albatros.DNN.Modules.Balises.IGC
{
    /// <summary>
    /// Base for B and K records
    /// </summary>
    /// <remarks></remarks>
    public class ExtendedRecord : TimeStampRecord
    {

        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        public Dictionary<string, string> Attributes
        {
            get { return _attributes; }
        }

        public ExtendedRecord(string rec, DateTime flightdate, ExtensionRecord extensions)
            : base(rec, flightdate)
        {
            foreach (string k in extensions.Extensions.Keys)
            {
                _attributes.Add(k, rec.Substring(extensions.Extensions[k].StartByte - 1, extensions.Extensions[k].Length()));
            }
        }

    }
}
