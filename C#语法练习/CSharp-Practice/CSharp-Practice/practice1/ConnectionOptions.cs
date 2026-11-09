using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Practice.practice1
{
    internal class ConnectionOptions
    {
        public string Host { get; init; } = "localhost";
        public int Port { get; init; } = 80;
        public bool UseSsl {  get; init; }
    }
}
