namespace Albatros.DNN.Modules.Balises.ICG
{
    public class HRecord : Record
    {

        public HRecord(string rec)
            : base(rec)
        {
            Code = rec.Substring(2, 3);
            Value = rec.Substring(5);
        }

        public string Code { get; private set; }
        public string Value { get; private set; }
    }
}
