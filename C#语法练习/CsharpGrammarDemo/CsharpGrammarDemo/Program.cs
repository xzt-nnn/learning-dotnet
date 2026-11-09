namespace CsharpGrammarDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("你好，世界");

            int a = 1;
            a = 2;

            //char a='AB';

            //var test;

            //Console.WriteLine("请输入你的名字");
            //string input = Console.ReadLine();
            //Console.WriteLine(input);

            //Console.Write("请输入等级（ABCD）");
            //string level = Console.ReadLine();
            //string result = level switch
            //{
            //    "A" => "优秀",
            //    _ => "无效等级"
            //};
            //Console.WriteLine(result);

            List<int> list = new List<int>()
            {
                1,2,3
            };

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=====");

            list.Add(10);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=====");

            list.AddRange(new int[] { 20, 30 });

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=====");

            list.Remove(20);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=====");

            list.RemoveAt(0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=====");

            list.Insert(0, 5);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(list.Contains(2));
            Console.WriteLine("=====");

            list.Clear();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(list.Contains(2));
            Console.WriteLine("=====");

            Console.WriteLine(list.Contains(1));
            Console.WriteLine(list.Contains(2));


            Dictionary<string, int> userScore = new Dictionary<string, int>
            {
                { "张三", 88 },
                { "李四", 92 }
            };

            // foreach遍历
            foreach (KeyValuePair<string, int> item in userScore)
            {
                Console.WriteLine($"姓名：{item.Key}，分数：{item.Value}");
            }

            // 只遍历所有key
            foreach (var k in userScore.Keys)
            {
                Console.WriteLine(k);
            }

            // 只遍历所有value
            foreach (var v in userScore.Values)
            {
                Console.WriteLine(v);
            }

            int x = 10;
            ChangeNum(ref x);
            Console.WriteLine(x); // 输出999

            //Divide(10, 3, out int res, out int yu);
            //Console.WriteLine($"商：{res}，余数：{yu}");

            // 调用
            CalcFive(10, 3,
                out int jia,
                out int jian,
                out int cheng,
                out int chu,
                out int yu);

            Console.WriteLine($"加:{jia} 减:{jian} 乘:{cheng} 除:{chu} 余:{yu}");

            Console.Write("请输入一个数字：");
            string input = Console.ReadLine();

            try
            {
                int num = int.Parse(input);
                Console.WriteLine($"转换成功，数字是：{num}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"出错了！原因：{ex.Message}");
            }

            try
            {
                CheckAge(-5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("校验失败：" + ex.Message);
            }

            List<Student> stuList = new List<Student>()
                {
                    new Student { Id = 1, Name = "小明", Score = 85 },
                    new Student { Id = 2, Name = "小红", Score = 92 },
                    new Student { Id = 3, Name = "小刚", Score = 58 },
                    new Student { Id = 4, Name = "小宇", Score = 76 },
                    new Student { Id = 5, Name = "小琪", Score = 92 }
                };


            var result = stuList.Where(s => s.Score > 80);
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name} {item.Score}");
            }

        }

        static void CheckAge(int age)
        {
            if (age < 0)
            {
                throw new ArgumentException("年龄不能是负数！");
            }
            Console.WriteLine($"年龄合法：{age}");
        }

        static void CalcFive(int a, int b,
                            out int add,
                            out int sub,
                            out int mul,
                            out int div,
                            out int mod)
                {
                    add = a + b;
                    sub = a - b;
                    mul = a * b;
                    div = a / b;
                    mod = a % b;
                }

        static void ChangeNum(ref int num)
        {
            num = 999;
        }

        static void Divide(int a, int b, out int result, out int mod)
        {
            result = a / b;
            mod = a % b;
        }
    }
}
