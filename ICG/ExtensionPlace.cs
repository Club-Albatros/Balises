using System;

namespace Albatros.DNN.Modules.Balises.ICG
{
    public class ExtensionPlace
    {
        public int StartByte;
        public int EndByte;
        public ExtensionPlace()
        {
        }
        public ExtensionPlace(string def)
        {
            StartByte = Convert.ToInt32(def.Substring(0, 2));
            EndByte = Convert.ToInt32(def.Substring(def.Length - 2));
        }
        public int Length()
        {
            return EndByte - StartByte + 1;
        }
    }
}
