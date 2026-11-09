using System.Threading.RateLimiting;
using Net8TodoControllerApi;
using Microsoft.AspNetCore.RateLimiting;

//builder是用来做准备工作的，如注册服务，依赖、读取配置等，在请求之前执行，只执行一次
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 4;
        opt.Window = TimeSpan.FromSeconds(12);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

//IOC：像容器注入依赖
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IHelloService, HelloService>();

// 同一接口注册两个实现：单个解析取最后一个，IEnumerable 解析取全部
builder.Services.AddSingleton<IMyDependency, MyDependency1>();
builder.Services.AddSingleton<IMyDependency, MyDependency2>();

// 【Build】：把所有配置、注册的服务打包，生成应用实例
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("中间件A 前置");
//    await next.Invoke();
//    Console.WriteLine("中间件A 后置");
//});

//app.Use(async (context, next) =>
//{
//    Console.WriteLine("中间件B 前置");
//    await next.Invoke();
//    Console.WriteLine("中间件B 后置");
//});

app.MapControllers();

// 兜底：上面没有匹配到任何路由的请求才走到这里（终端中间件，不再往下）
//app.Run(async context =>
//{
//    Console.WriteLine("Run终端中间件，直接返回内容");
//    await context.Response.WriteAsync("hello");
//});

app.Run();
