using CSharp_Practice.enums;
using CSharp_Practice.Main;
using CSharp_Practice.practice1;
using System.Collections;
using System.IO.Pipes;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Text;
//public record Server(string Host, int Port, bool UseSsl);
//public record Person(string FirstName, string LastName);
//public record Student(string FirstName, string LastName,int GradeLevel) : Person(FirstName, LastName);

namespace CSharp_Practice
{
    internal class Program
    {
        static string DescribeSeason(Season season)
        {
            switch (season)
            {
                case Season.Spring:
                    return "春天";
                case Season.Summer:
                    return "夏天";
                case Season.Fall:
                    return "秋天";
                case Season.Winter:
                    return "冬天";
                default:
                    throw new ArgumentOutOfRangeException(nameof(season));
            }
        }


        static T Max<T>(T a,T b) where T:IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }


        static (double Minimum, double Maximum, double Average) ComputeStats(List<double> values)
        {
            var min = values.Min();
            var max = values.Max();
            var avg = values.Average();
            return (min, max, avg);
        }

        // 城市类
        public class City
        {
            public string Name { get; set; }
            public int Population { get; set; }
        }

        // 国家类：每个国家里面包含城市集合
        public class Country
        {
            public string Name { get; set; }
            public double Area { get; set; } // 面积，单位万平方公里
        }

        delegate int Transformer(int val);
        static async Task Main(string[] args)
        {
            //var greeter = new Greeter();
            //Console.WriteLine(greeter.Greet("张三"));

            //var options = new ConnectionOptions
            //{
            //    Host = "db.example.com",
            //    //Port = 5432,
            //    UseSsl = true,
            //};
            //Console.WriteLine($"{options.Host}:{options.Port}(SSL:{options.UseSsl})");

            //var options1 = new Server("127.0.0.1", 8080, true);
            //Console.WriteLine($"{options1.Host}:{options1.Port}(SSL:{options1.UseSsl})");
            //var options2 = new Server("127.0.0.1", 8080, true);
            //Console.WriteLine(options2==options1);

            //var newOptions = options1 with
            //{
            //    //Port = 8834,
            //    //UseSsl = true
            //};
            //Console.WriteLine($"{newOptions.Host}:{newOptions.Port}(SSL:{newOptions.UseSsl})");

            //var (host, port, ssl) = options1;
            //Console.WriteLine($"解构：host={host},port = {port},ssl={ssl}");

            //Person p=new Person("zhang","san");
            //Student s = new Student("zhang", "san", 9);
            //Console.WriteLine(p == s);

            //var s2 = p with
            //{
            //    FirstName = "shvcioewahrvnewaio"
            //};
            //Console.WriteLine(s2);

            //Student s3 = (Student)p with { };
            //Console.WriteLine(s3);

            //Console.WriteLine(Season.Spring);
            //var today = Season.Spring;
            //Console.WriteLine(DescribeSeason(today));

            //var parsed = Enum.Parse<Season>("Winter");//转换为字符串
            //Console.WriteLine(parsed);

            //List<int> scores = [1,2,3,4];
            //Dictionary<string,string> prices = new Dictionary<string,string>();
            //Task<string> greeting = Task.FromResult("Hello,11111");
            //Func<int, bool> isPositive = n => n > 0;

            //Console.WriteLine($"First score: {scores[0]}");
            //Console.WriteLine($"Widget price: {prices["Widget"]:C}");
            //Console.WriteLine($"Greeting: {await greeting}");
            //Console.WriteLine($"Is 5 positive? {isPositive(5)}");

            //List<int> first = [1, 2, 3];
            //List<int> second = [4, 5, 6];

            //List<int> combine = [.. first, .. second];
            //Console.WriteLine($"Combined list: {string.Join(", ", combine)}");

            //Dictionary<string,int> firstDict = new Dictionary<string, int>
            //{
            //    ["one"] = 1,
            //    ["two"] = 2
            //};

            //foreach(var(name,score) in firstDict)
            //{
            //    Console.WriteLine($"Name: {name}, Score: {score}");
            //}
            //Console.WriteLine($"Max of 5 and 10: {Max(5, 10)}");

            //(string Name, int Age) person = ("Alice", 300);
            //Console.WriteLine($"Name:{person.Name},Age:{person.Age}");

            //(string, int) unnamed = ("Bob", 25);
            //Console.WriteLine($"Name:{unnamed.Item1},Age:{unnamed.Item2}");

            //var city = (Name: "New York", Population: 8419600);
            //Console.WriteLine($"City: {city.Name}, Population: {city.Population}");

            //var (min, max, avg) = ComputeStats(new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 });//编译器会推断出ComputeStats方法的返回类型是(double Minimum, double Maximum, double Average)
            //Console.WriteLine($"Min: {min}, Max: {max}, Average: {avg}");

            //var point = (x:3,y:4);
            //var (_, y) = point;//自动推算类型
            //Console.WriteLine($"Point: ({y})");

            //(int x1,int y1) = point;
            //Console.WriteLine($"Point: ({x1}, {y1})");
            //int a = 0, b = 0;
            //(_,b) = point;
            //Console.WriteLine($"Point: ({a}, {b})");

            //名字不会影响元组的相等性，只有值才会影响相等性
            //var order1 = (Product: "Widget", Quantity: 5);
            //var order2 = (Product: "Widget", Quantity: 5);
            //var order3 = (Product: "Gadget", Quantity: 3);

            //Console.WriteLine(order1 == order2); // True
            //Console.WriteLine(order1 == order3); // False

            //// Element names don't affect equality—only values matter
            //var named = (X: 1, Y: 2);
            //var different = (A: 1, B: 2);
            //Console.WriteLine(named == different); // True

            //var first = (Name:"first",Age:20);

            //var second = first with { Age = 30 };

            //Console.WriteLine($"First: {first.Name}, Age: {first.Age}");
            //Console.WriteLine($"Second: {second.Name}, Age: {second.Age}");

            //var Dic = new Dictionary<string,(int a,int b)>
            //{
            //    ["first"] = (1, 2),
            //    ["second"] = (3, 4),
            //    ["third"] = (5, 6)
            //};

            //if(Dic.TryGetValue("second", out var value))
            //{
            //    Console.WriteLine($"Value: ({value.a}, {value.b})");
            //}
            //else
            //{
            //    Console.WriteLine("Key not found.");
            //}

            //Transformer first = X => X + 1;
            //Console.WriteLine(first(5));

            //int[] numbers = [1, 2, 3, 4, 5, 6];
            //int[] evenNumbers = numbers.Where(value => value % 2 == 0).ToArray();

            //Console.WriteLine(string.Join(", ", evenNumbers));

            //var person = new
            //{
            //    Name = "aaa",
            //    Age = 30,
            //};
            //Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
            //var person2 = new { Name = "Alice", Age = 30 };
            //Console.WriteLine($"{person2.Name} is {person2.Age} years old.");

            // Accessing a member on null throws NullReferenceException at runtime:
            // string? name = null;
            // int length = name.Length; // throws NullReferenceException

            //// Check before you dereference:
            //string? name = null;
            //Console.WriteLine(name.Length);
            //if (name is not null)
            //{
            //    Console.WriteLine($"Name has {name.Length} characters.");
            //}
            //else
            //{
            //    Console.WriteLine("Name has no value.");
            //}
            //// Output: Name has no value.
            ///
            //var sb = new StringBuilder(); 
            //sb.Append(1).Append(2).Append(3);
            //Console.WriteLine(sb.ToString()); // Output: 123

            //await main1.main();

            //string[] names = ["Nancy", "John", "Alice", "Bob"];
            //for(int i = 0; i < names.Length; i++)
            //{
            //    Console.WriteLine($"Name {i + 1}: {names[i]}");
            //}
            //foreach (var name in names)
            //{
            //    Console.WriteLine($"Name: {name}");
            //}
            //string[] names = ["Nancy", "John", "Alice", "Bob"];

            //IEnumerable<string> query =
            //    from name in names
            //    where name.Length > 3
            //    orderby name
            //    select name;
            //foreach (var name in query)
            //{
            //    Console.WriteLine(name + " ");
            //}

            //IEnumerable<string> query2 =
            //    from name in names
            //    where name.Length > 1
            //    orderby name
            //    select name;
            //foreach (var name in query2)
            //{
            //    Console.WriteLine(name + " ");
            //}

            //string[] names = ["Nancy", "John", "Alice", "Bob"];
            //IEnumerable<string> query3 = names.Where(name => name.Length > 3).OrderBy(name => name).Select(name => name);
            //for(int i = 0; i < query3.Count(); i++)
            //{
            //    Console.WriteLine($"Name {i + 1}: {query3.ElementAt(i)}");
            //}

            //(string Area, int Priority)[] workItems =
            //    [
            //            ("docs", 2),
            //            ("tests", 1),
            //            ("deploy", 4),
            //            ("api", 1)
            //    ];

            //IEnumerable<(string,int)> query = 
            //    from item in workItems
            //    where item.Priority>1
            //    orderby item.Area
            //    select (item.Area, item.Priority);

            //for(int i = 0; i < query.Count(); i++)
            //{
            //    var (area, priority) = query.ElementAt(i);
            //    Console.WriteLine($"Area: {area}, Priority: {priority}");
            //}
            //IEnumerable<string> nextItems =
            //from item in workItems
            //let label = $"{item.Area}: P{item.Priority}"
            //where item.Priority <= 2
            //orderby item.Priority, item.Area
            //select label;

            //foreach (string item in nextItems)
            //{
            //    Console.WriteLine(item);
            //}

            //string[] names = ["Nancy", "Zohn", "Zlice", "Bob"];
            ////把名字长度大于三的都找出来
            //IEnumerable<string> query = names.Where(name => name.Length > 3).OrderBy(name => name[0]).Select(name => name.ToUpper());

            //IEnumerable<string> query2 =
            //    from name in names
            //    where name.Length > 4
            //    select name;

            //foreach (var name in query)
            //{
            //    Console.WriteLine(name);
            //}

            //foreach (var name in query)
            //{
            //    Console.WriteLine(name);
            //}


            // This example produces the following output:
            //
            //    Code: 1
            //    Docs: 2
            //    Test: 3
            //

            //Dictionary<string, int> priorities = new()
            //{
            //    ["Docs"] = 2,
            //    ["Code"] = 1,
            //    ["Test"] = 3,
            //    ["Deploy"] = 4
            //};

            //IEnumerable<string> query = 
            //    priorities.Where(pri=>pri.Value<=3).OrderBy(pri => pri.Value).Select(pri => $"{pri.Key}:{pri.Value}");

            //foreach (var item in query)
            //{
            //    Console.WriteLine(item);
            //}

            //List<string> labels = ["api", "auth", "docs", "deploy"];
            // This example produces the following output:
            //
            //    a: api, auth
            //    d: docs, deploy

            //IEnumerable<IGrouping<char, string>> query =
            //    labels.GroupBy(label => label[0]).OrderBy(group => group.Key);
            //labels.Add("test");

            //foreach (var label in query)
            //{
            //    Console.WriteLine($"{label.Key}: {string.Join(", ", label)}");
            //}

            //(string Title, int Priority, bool IsOpen)[] items =
            //    [
            //        ("docs", 2, true),
            //        ("tests", 1, true),
            //        ("deploy", 3, false),
            //        ("api", 1, true)
            //    ];

            //IEnumerable<(string Title, int Priority, bool IsOpen)> openItems =
            //    from item in items
            //    where item.IsOpen
            //    select item;

            //foreach (var item in openItems)
            //{
            //    Console.WriteLine($"Title: {item.Title}, Priority: {item.Priority}, IsOpen: {item.IsOpen}");
            //}

            //IEnumerable<string> topOpenItems =
            //    from item in openItems
            //    where item.Priority == 1
            //    orderby item.Title
            //    select item.Title;

            //foreach (string title in topOpenItems)
            //{
            //    Console.WriteLine(title);
            //}
            // This example produces the following output:
            //
            //    api
            //    tests
            //

            //List<(string Title, int Priority, bool IsOpen)> items =
            //    [
            //        ("docs", 2, true),
            //        ("tests", 1, true),
            //        ("deploy", 3, false)
            //    ];

            //List<(string Title, int Priority, bool IsOpen)> openItems = items
            //    .Where(item => item.IsOpen)
            //    .ToList();

            //items.Add(("api", 1, true));

            //IEnumerable<string> cachedTopOpenItems = openItems
            //    .Where(item => item.Priority == 1)
            //    .OrderBy(item => item.Title)
            //    .Select(item => item.Title);

            //foreach (string title in cachedTopOpenItems)
            //{
            //    Console.WriteLine(title); // => tests
            //}

            // 第一个数据源：分类
            //var categories = new List<string>
            //    {
            //        "水果",
            //        "电器",
            //        "图书"
            //    };

            //// 第二个数据源：商品
            //var products = new List<(string Name, string Category)>
            //    {
            //        ("苹果", "水果"),
            //        ("香蕉", "水果"),
            //        ("吹风机", "电器"),
            //        ("C#入门", "图书"),
            //        ("篮球", "运动") // 这个分类在categories里没有，内连接不会查出来
            //    };

            //var query = 
            //    from cat in categories
            //    join prod in products on cat equals prod.Category
            //    select new { Category = cat, ProductName = prod.Name };

            //foreach (var item in query)
            //{
            //    Console.WriteLine($"Category: {item.Category}, Product: {item.ProductName}");
            //}

            //int[] scores= [85, 92, 78, 90, 88];


            //int count = scores.Where(score=> score >= 90).Count();
            //Console.WriteLine(count);
            //int max = scores.Max();
            //Console.WriteLine(max);
            //int min = scores.Min();
            //Console.WriteLine(min);
            //int sum = scores.Sum();
            //Console.WriteLine(sum);


            // IEnumerable<int> query = 
            //    scores.Where(score=>score>=90).OrderBy(score=>score).Select(score=>score);
            //foreach (int score in query) {
            //    Console.WriteLine(score);
            //}

            //Console.WriteLine("----");

            //IEnumerable<string> query2 = 
            //    from score in scores
            //    where score>=90
            //    orderby score descending
            //    select $"score:{score}";
            //foreach (string result in query2) {
            //    Console.WriteLine(result);
            //}
            // 构造测试数据
            //var countries = new List<Country>
            //{
            //    new Country
            //    {
            //        Name = "中国",
            //        Cities = new List<City>
            //        {
            //            new City { Name = "广州", Population = 18000000 },
            //            new City { Name = "肇庆", Population = 4000000 }
            //        }
            //    },
            //    new Country
            //    {
            //        Name = "日本",
            //        Cities = new List<City>
            //        {
            //            new City { Name = "东京", Population = 14000000 },
            //            new City { Name = "小村镇", Population = 8000 }
            //        }
            //    }
            //};
            //查询所有国家中人口超过10000的城市，并按人口从大到小排序

            //var query =
            //    from country in countries
            //    from city in country.Cities
            //    orderby city.Population descending
            //    where city.Population > 10000
            //    select new { CountryName = country.Name, CityName = city.Name, Population = city.Population };

            //foreach (var item in query)
            //{
            //    Console.WriteLine($"Country: {item.CountryName}, City: {item.CityName}, Population: {item.Population}");
            //}

            // 构造测试数据
            var countries = new List<Country>
            {
                new Country { Name = "中国", Area = 960 },
                new Country { Name = "美国", Area = 937 },
                new Country { Name = "韩国", Area = 10.03 },
                new Country { Name = "澳大利亚", Area = 769 }
            };
            //// 示例代码：按面积从小到大排序，select直接返回原Country对象
            //IEnumerable<Country> query = 
            //    from country in countries
            //    orderby country.Area ascending
            //    select country;

            //foreach (var country in query)
            //{
            //    Console.WriteLine($"Country: {country.Name}, Area: {country.Area}");
            //}

            //找出面积大于100的国家，分组查询
            var query =
                from country in countries
                group country by country.Area > 100 into grouped
                where grouped.Count() > 0
                orderby grouped.Key descending
                select grouped;

            foreach (var g in query)
            {
                Console.WriteLine($"分组key（面积大于1亿）：{g.Key}");
                foreach (var item in g)
                {
                    Console.WriteLine($"  -- Country: {item.Name}, Area: {item.Area}");
                }
            }

            string[] groupingQuery = ["carrots", "cabbage", "broccoli", "beans", "barley"];
            IEnumerable<IGrouping<char, string>> queryFoodGroups =
                from item in groupingQuery
                group item by item[0] into g
                select g;

            foreach(IGrouping<char,string> g in queryFoodGroups)
            {
                Console.WriteLine(g.Key);
                foreach (var item in g)
                {
                    Console.WriteLine(item);
                }
            }




        }
    }
}
