//using System;
//using System.Threading.Tasks;

////Console.WriteLine("主线程开始执行");

//////调用异步方法
////var task = DoWorkAsync();

////Console.WriteLine("主线程继续往下跑，不会卡住");

////await task;

////Console.WriteLine("全部结束");

//////异步方法
////async Task DoWorkAsync()
////{
////    Console.WriteLine("异步任务开始执行");
////    await Task.Delay(2000);
////    Console.WriteLine("异步任务2秒后完成");


////}


//using System;
//using System.Threading.Tasks;

//Console.WriteLine("1. 启动任务，不等待");
//Task<string> task = GetMsgAsync();//

//Console.WriteLine("2. 主线程继续做别的事情");
//await Task.Delay(5000);

//Console.WriteLine("3. 现在再来等待任务结果");
//string msg = await task;//这里写了一遍await
//Console.WriteLine($"4. 结果：{msg}");

//async Task<string> GetMsgAsync()
//{
//    await Task.Delay(10000);//这里为什么还有写一遍await
//    return "hello async";
//}

using System;

// 1. 先定义委托：规定事件触发时，调用的方法长什么样
delegate void AlarmDelegate(string msg);

// 发布者：闹钟类
class AlarmClock
{
    // ✅ 事件！基于上面的委托 AlarmDelegate
    public event AlarmDelegate OnAlarm;

    public void Ring()
    {
        Console.WriteLine("闹钟时间到！准备触发事件");
        // 触发事件：通知所有订阅了这个事件的方法
        OnAlarm?.Invoke("叮叮叮！积极I级起床啦");
    }
}

// 订阅者
//class Program
//{
//    static void Main()
//    {
//        AlarmClock clock = new AlarmClock();

//        // 订阅事件：把方法挂上去 +=
//        clock.OnAlarm += WakeUp;
//        clock.OnAlarm += TurnOnLight;

//        // 闹钟响，触发事件
//        clock.Ring();

//        Console.WriteLine("\n取消订阅 WakeUp");
//        clock.OnAlarm -= WakeUp;
//        clock.Ring();
//    }

//    static void WakeUp(string tip)
//    {
//        Console.WriteLine($"收到通知：{tip}，我起床了");
//    }
//    static void TurnOnLight(string tip)
//    {
//        Console.WriteLine($"收到通知：{tip}，开灯");
//    }
//}

// 定义委托：两个int参数，返回int
delegate int MyCalculateDelegate(int a, int b);

class Program
{
    static int Add(int a, int b) => a + b;
    static int Sub(int a, int b) => a - b;

    static void Main()
    {
       // MyCalculateDelegate cal = Add;
       //Console.WriteLine(cal(10, 5));
       // cal=Sub;
       // Console.WriteLine(cal(10, 5));
       Func<int,int,int> cal = Add;
        Console.WriteLine(cal(10, 5));
        cal = Sub;
        Console.WriteLine(cal(10, 5));
    }
}
