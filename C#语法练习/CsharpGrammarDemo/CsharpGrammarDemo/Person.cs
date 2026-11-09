using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpGrammarDemo
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public void SayHello()
        {
            Console.WriteLine($"我是{Name}，年龄{Age}");
        }
    }
}
