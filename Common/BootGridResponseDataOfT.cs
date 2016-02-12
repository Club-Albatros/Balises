using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class BootGridResponseData<T>
    {
        public int current { get; set; }
        public int rowCount { get; set; }
        public IEnumerable<T> rows { get; set; }
        public int total { get; set; }
    }
}