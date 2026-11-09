using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpGrammarDemo
{
    internal class AsyncDemo
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("主线程开始：" + DateTime.Now.ToString("HH:mm:ss"));
        //    DoWork();
        //    Console.WriteLine("主线程结束：" + DateTime.Now.ToString("HH:mm:ss"));
        //}

        //static void DoWork()
        //{
        //    Console.WriteLine("开始干活，等待2秒");
        //    Thread.Sleep(2000); // 阻塞！线程卡死在这里等
        //    Console.WriteLine("干活完成");
        //}

        //异步Main入口
        static async Task Main(string[] args)
        {
            Console.WriteLine("主线程开始：" + DateTime.Now.ToString("HH:mm:ss"));
            await DoWorkAsync();
            Console.WriteLine("主线程结束：" + DateTime.Now.ToString("HH:mm:ss"));
        }

        // async标记，返回Task
        static async Task DoWorkAsync()
        {
            Console.WriteLine("开始干活，等待2秒");
            await Task.Delay(2000); // 异步等待，不阻塞线程！
            Console.WriteLine("干活完成");
        }

        // 模拟：限制线程池工作线程为2
        //static async Task Main(string[] args)
        //{
        //    ThreadPool.SetMinThreads(2, 2);
        //    ThreadPool.SetMaxThreads(2, 2);

        //    Console.WriteLine("==== 开始5个异步SQL任务，线程池上限2个 ====");
        //    Task[] tasks = new Task[5];
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        int idx = i;
        //        tasks[i - 1] = RunSqlAsync(idx);
        //    }
        //    await Task.WhenAll(tasks);
        //    Console.WriteLine("全部任务完成");
        //}

        ///// <summary>
        ///// 模拟一条耗时3秒的异步SQL查询
        ///// </summary>
        //static async Task RunSqlAsync(int taskId)
        //{
        //    Console.WriteLine($"任务{taskId}：发起SQL，当前线程Id：{Thread.CurrentThread.ManagedThreadId}");
        //    // 模拟数据库IO等待3秒，此时线程释放！
        //    await Task.Delay(3000);
        //    Console.WriteLine($"任务{taskId}：SQL结果返回，收尾，当前线程Id：{Thread.CurrentThread.ManagedThreadId}");
        //}



        static async Task Main1(string[] args)
        {
            //Console.WriteLine("准备获取结果");
            ////await:拿到返回值
            //int result = await GetSumAsync(10, 20);
            //Console.WriteLine($"计算·结果：{result}");

            //Console.WriteLine("顺序开始：" + DateTime.Now.ToString("HH:mm:ss"));
            //await Task.Delay(2000); // 等2s
            //await Task.Delay(3000); // 再等3s
            //Console.WriteLine("顺序结束：" + DateTime.Now.ToString("HH:mm:ss"));

            Console.WriteLine("并行开始：" + DateTime.Now.ToString("HH:mm:ss"));
            // 先把两个任务启动，不要马上await！
            Task t1 = Task.Delay(2000);
            Task t2 = Task.Delay(3000);
            // 等待全部完成
            //同时启动两个线程
            await Task.WhenAll(t1, t2);
            Console.WriteLine("并行结束：" + DateTime.Now.ToString("HH:mm:ss"));

            Console.WriteLine("开始");
            try
            {
                await TestExceptionAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"捕获异常：{ex.Message}");
            }
            Console.WriteLine("结束");

            // 创建取消源，设置2秒后自动取消
            using var cts = new CancellationTokenSource(2000);
            try
            {
                Console.WriteLine("启动慢SQL，最多等待2秒");
                await SlowQueryAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("任务被取消/超时！");
            }
        }

                // 接收取消令牌
        static async Task SlowQueryAsync(CancellationToken token)
        {
            // 模拟耗时5秒的SQL，但是cts2秒就发取消信号
            await Task.Delay(5000, token);
            Console.WriteLine("SQL执行完成");
        }

        static async Task TestExceptionAsync()
        {
            await Task.Delay(1000);
            throw new Exception("数据库查询失败！");
        }

        private static async Task<int> GetSumAsync(int v1, int v2)
        {
            Console.WriteLine("开始计算，模拟IO等待1秒");
            await Task.Delay(1000);
            return v1 + v2;
        }
    }
}
