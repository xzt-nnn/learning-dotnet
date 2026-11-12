using System;

namespace ForthMVCDemo.Commons
{
    /// <summary>
    /// 业务异常：消息可以直接展示给用户
    /// </summary>
    public class BusinessException : Exception
    {
        public int Code { get; }

        public BusinessException(string message, int code = 1000) : base(message)
        {
            Code = code;
        }
    }
}
