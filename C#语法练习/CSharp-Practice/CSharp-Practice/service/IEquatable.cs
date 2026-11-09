using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Practice.service
{
    internal interface IEquatable<T>
    {
        bool Equals(T obj);
    }
}
