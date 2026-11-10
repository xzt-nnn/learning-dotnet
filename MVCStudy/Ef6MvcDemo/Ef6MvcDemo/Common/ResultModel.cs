namespace Ef6MvcDemo.Common
{
    public class ResultModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ResultModel Ok(object data = null, string message = "操作成功")
        {
            return new ResultModel
            {
                Code = (int)ResultCode.Success,
                Message = message,
                Data = data
            };
        }

        public static ResultModel Fail(string message = "操作失败", int code = (int)ResultCode.Fail, object data = null)
        {
            return new ResultModel
            {
                Code = code,
                Message = message,
                Data = data
            };
        }
    }

    //强类型版本，Data 不需要装箱，编译期就能约束类型
    public class ResultModel<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResultModel<T> Ok(T data, string message = "操作成功")
        {
            return new ResultModel<T>
            {
                Code = (int)ResultCode.Success,
                Message = message,
                Data = data
            };
        }

        public static ResultModel<T> Fail(string message = "操作失败", int code = (int)ResultCode.Fail)
        {
            return new ResultModel<T>
            {
                Code = code,
                Message = message,
                Data = default(T)
            };
        }
    }
}