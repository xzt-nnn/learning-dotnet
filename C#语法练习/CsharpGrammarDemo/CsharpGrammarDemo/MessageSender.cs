using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpGrammarDemo
{

    // 1. 先定义委托类型（用来约束事件触发时回调方法的格式）
    public delegate void NotifyDelegate(string msg);
    internal class MessageSender
    {
        public event NotifyDelegate? OnSendMessage;

        public void Send()
        {
            Console.WriteLine("开始发送消息...");
            // 触发事件：先判断有没有人订阅，不为null才调用
            OnSendMessage?.Invoke("一条消息来了");
        }

        static void Main(string[] args)
        {
            // 主程序
            MessageSender sender = new MessageSender();
            // += 订阅事件
            sender.OnSendMessage += ReceiveMsg;
            // -= 取消订阅
            //sender.OnSendMessage -= ReceiveMsg;

            sender.Send();
        }

        static void ReceiveMsg(string content)
        {
            Console.WriteLine($"收到通知：{content}");
        }
    }
}
