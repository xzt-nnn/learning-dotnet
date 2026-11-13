namespace LibraryDemo.Common
{
    public class AjaxResult
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static AjaxResult Ok(object data = null, string message = "操作成功")
        {
            return new AjaxResult { Success = true, Code = "0", Message = message, Data = data };
        }

        public static AjaxResult Fail(string message, string code = "500")
        {
            return new AjaxResult { Success = false, Code = code, Message = message };
        }
    }
}