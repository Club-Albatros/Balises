using System;

namespace Albatros.DNN.Modules.Balises.IGC
{
    /// <summary>
    /// Event rec
    /// </summary>
    /// <remarks></remarks>
    public class ERecord : TimeStampRecord
    {

        public ERecord(string rec, DateTime flightdate)
            : base(rec, flightdate)
        {
            _code = rec.Substring(7, 3);
            if (rec.Length > 10)
            {
                _value = rec.Substring(10);
            }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
        }

    }
}
