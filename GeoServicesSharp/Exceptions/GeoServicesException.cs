using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoServicesSharp.Exceptions
{
    [Serializable]
    public class GeoServicesException : Exception
    {
        public GeoServicesException(string message) : base(message)
        {       
        }

        public GeoServicesException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
