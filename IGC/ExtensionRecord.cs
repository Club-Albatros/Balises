using System;
using System.Collections.Generic;

namespace Albatros.DNN.Modules.Balises.IGC
{
    public class ExtensionRecord : Record
    {

        public ExtensionRecord(string rec)
            : base(rec)
        {
            int cnt = Convert.ToInt32(rec.Substring(1, 2));
            for (int i = 1; i <= cnt; i++)
            {
                _extensions.Add(rec.Substring((i - 1) * 7 + 7, 3), new ExtensionPlace(rec.Substring((i - 1) * 7 + 3, 4)));
            }
        }

        private Dictionary<string, ExtensionPlace> _extensions = new Dictionary<string, ExtensionPlace>();
        public Dictionary<string, ExtensionPlace> Extensions
        {
            get { return _extensions; }
        }

    }
}
