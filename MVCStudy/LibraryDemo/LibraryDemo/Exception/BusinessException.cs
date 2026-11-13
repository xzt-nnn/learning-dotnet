using System;

public class BusinessException : Exception
{
    public string Code { get; }
    public int StatusCode { get; }
    public BusinessException(string message, string code = "400", int statusCode = 400)
        : base(message) { Code = code; StatusCode = statusCode; }
}