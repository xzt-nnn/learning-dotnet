using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpGrammarDemo
{
    internal class LINQ
    {

        public delegate int CalcDelegate(int a, int b);
        static void Main(string[] args)
        {
            List<Student> stuList = new List<Student>()
            {
                new Student { Id = 1, Name = "小明", Score = 85 },
                new Student { Id = 2, Name = "小红", Score = 92 },
                new Student { Id = 3, Name = "小刚", Score = 58 },
                new Student { Id = 4, Name = "小宇", Score = 76 },
                new Student { Id = 5, Name = "小琪", Score = 92 }
            };
            var result = from s in stuList
                         where s.Score > 90
                         select s;
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name} {item.Score}");
            }

            var query = stuList.Where(s => s.Score == 92)
                .Select(s => new { s.Name });
            //var query = stuList
            //    .Where(s => s.Score >= 70)
            //    .OrderByDescending(s => s.Score)
            //    .Select(s => new { s.Name, s.Score });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name}");
            }

            // 普通方法
            static int Add(int x, int y)
            {
                return x + y;
            }
            static int Sub(int x, int y)
            {
                return x - y;
            }

            // 使用委托变量指向方法
            CalcDelegate func = Add;
            int res1 = func(10, 3);
            Console.WriteLine(res1);

            func = Sub;
            int res2 = func(10, 3);
            Console.WriteLine(res2);
            
            // Func：两个int入参，返回int
            Func<int, int, int> calc = (a, b) => a + b;
            Console.WriteLine(calc(2,3));

            // Action：无返回值
            Action<string> print = msg => Console.WriteLine(msg);
            print("Hello Action");

            // Predicate：判断，返回bool
            Predicate<int> isBig = n => n > 10;
            Console.WriteLine(isBig(15));


        }
    }
}
