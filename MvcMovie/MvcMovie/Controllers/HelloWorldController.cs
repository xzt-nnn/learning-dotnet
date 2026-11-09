using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers;

public class HelloWorldController : Controller
{
    // 
    // GET: /HelloWorld/
    //public string Index()
    //{
    //    return "This is my default action...";
    //}
    // 
    // GET: /HelloWorld/Welcome/ 
    public string Welcome()
    {
        return "This is the Welcome action method...";
    }

    // GET: /HelloWord/Like
    public string Like()
    {
        return "我喜欢看番";
    }

    // GET: /HelloWorld/NBA?name=lebron&team=76ers
    public string NBA(int id,string name,string team="cav")
    {
        return "name:"+name+"team:"+team+"id:"+id;
    }

    //GET: /HelloWorld/index2
    public IActionResult Index()
    {
        return View();
    }
}