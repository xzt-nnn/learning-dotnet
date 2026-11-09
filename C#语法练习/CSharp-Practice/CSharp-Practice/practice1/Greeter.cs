using System;
using System.Globalization;

namespace CSharp_Practice.practice1
{
    internal class Greeter
    {
        public string Greet(string name)
        {
            int count = 10;
            double temperature = 36.6;
            var CodeName = "C#";
            var items = new List<string> { "one", "two", "three" };
            Console.WriteLine($"count={count}, temperature={temperature}, CodeName={CodeName}, items={string.Join(",", items)}");
            var culture = CultureInfo.CurrentCulture;
            return $"Hello ,{name} ! Culture:{culture.Name}";
        }
    }
}
