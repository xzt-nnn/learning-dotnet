using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Practice.Main
{
    internal class main1
    {

        private static async IAsyncEnumerable<int> GenerateAsync()
        {
            for (int i = 1; i <= 3; i++)
            {
                await Task.Delay(1000); // 模拟耗时操作：查数据库、调接口，要等一会才拿到这一条
                yield return i; // 产出一个元素：等一会 → 产出1 → 等一会 → 产出2 → 等一会 → 产出3
            }
        }


        internal static async Task main()
        {
            await foreach (int value in GenerateAsync())
            {
                Console.WriteLine(value); // 产出一个就立刻打印一个：1，然后2，然后3
            }
        }
    }
}
