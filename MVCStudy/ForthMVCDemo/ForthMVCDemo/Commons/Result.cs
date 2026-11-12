namespace ForthMVCDemo.Commons
{
    public class Result
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static Result Ok(object data = null, string message = "操作成功")
            => new Result { Success = true, Code = 0, Message = message, Data = data };

        public static Result Fail(string message, int code = 1000)
            => new Result { Success = false, Code = code, Message = message };
    }
}