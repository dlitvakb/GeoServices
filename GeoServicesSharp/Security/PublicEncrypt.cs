using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoServicesSharp.Security
{
    class Encrypt
    {
        public Encrypt() { }

        public string encrypt(string input)
        {
            return new PrivateEncrypt().Encrypt(input);
        }
    }
}