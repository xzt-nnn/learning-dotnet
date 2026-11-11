using System;

namespace GitStudy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "GitStudy - 配合 SourceTree 学习 Git";

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== GitStudy 控制台练习 ===");
                Console.WriteLine("1. 跟我打个招呼");
                Console.WriteLine("2. 加法计算");
                Console.WriteLine("3. 减法计算");
                Console.WriteLine("0. 退出");
                Console.Write("请选择: ");

                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Console.WriteLine("再见，记得提交你的改动！");
                    return;
                }

                switch (choice)
                {
                    case "1":
                        Console.Write("请输入你的名字: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("你好, " + name + "! 这个项目就是用来练 Git 的。");
                        break;
                    case "2":
                        RunMath(true);
                        break;
                    case "3":
                        RunMath(false);
                        break;
                    default:
                        Console.WriteLine("没有这个选项，请重新输入。");
                        break;
                }
            }
        }

        private static void RunMath(bool isAdd)
        {
            double left = ReadNumber("第一个数字: ");
            double right = ReadNumber("第二个数字: ");

            double result = isAdd ? Calculator.Add(left, right) : Calculator.Subtract(left, right);
            Console.WriteLine("结果: " + result);
        }

        private static double ReadNumber(string tip)
        {
            while (true)
            {
                Console.Write(tip);
                double value;
                if (double.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }

                Console.WriteLine("这不是有效数字，请重试。");
            }
        }
    }
}
