using WalkthroughWritingLinqQueries;

// Create a data source by using a collection initializer.
IEnumerable<Student> students =
[
    new Student(First: "Svetlana", Last: "Omelchenko", ID: 111, Scores: [97, 92, 81, 60]),
    new Student(First: "Claire",   Last: "O'Donnell",  ID: 112, Scores: [75, 84, 91, 39]),
    new Student(First: "Sven",     Last: "Mortensen",  ID: 113, Scores: [88, 94, 65, 91]),
    new Student(First: "Cesar",    Last: "Garcia",     ID: 114, Scores: [97, 89, 85, 82]),
    new Student(First: "Debra",    Last: "Garcia",     ID: 115, Scores: [35, 72, 91, 70]),
    new Student(First: "Fadi",     Last: "Fakhouri",   ID: 116, Scores: [99, 86, 90, 94]),
    new Student(First: "Hanying",  Last: "Feng",       ID: 117, Scores: [93, 92, 80, 87]),
    new Student(First: "Hugo",     Last: "Garcia",     ID: 118, Scores: [92, 90, 83, 78]),

    new Student("Lance",   "Tucker",      119, [68, 79, 88, 92]),
    new Student("Terry",   "Adams",       120, [99, 82, 81, 79]),
    new Student("Eugene",  "Zabokritski", 121, [96, 85, 91, 60]),
    new Student("Michael", "Tucker",      122, [94, 92, 91, 91])
];

// Create the query.
// The first line could also be written as "var studentQuery ="
//IEnumerable<Student> studentQuery =
//    from student in students
//    where student.Scores[0] > 90 && student.Scores[3] < 80
//    select student;

//foreach (Student student in studentQuery)
//{
//    Console.WriteLine($"{student.Last}, {student.First}");
//}


//IEnumerable<IGrouping<char, Student>> studentQuery =
//    from student in students
//    group student by student.Last[0];

//foreach (IGrouping<char, Student> studentGroup in studentQuery)
//{
//    Console.WriteLine(studentGroup.Key);
//    foreach (Student student in studentGroup)
//    {
//        Console.WriteLine($"   {student.Last}, {student.First}");
//    }
//}


//List<string> phrases = ["an apple a day", "the quick brown fox"];

//var query = from phrase in phrases
//            from word in phrase.Split(' ')
//            select word;

//foreach (string s in query)
//{
//    Console.WriteLine(s);
//}

//string[] words = ["the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog"];

//IEnumerable<string> query = from word in words.DistinctBy(word => word.Length)
//                            select word;

//foreach (var str in query)
//{
//    Console.WriteLine(str);
//}
//string[] words1 = ["the", "quick", "brown", "fox"];
//string[] words2 = ["jumped", "over", "the", "lazy", "dog"];

////返回共有的元素列表
//IEnumerable<string> res = words1.Intersect(words2);

//foreach (var str in res)
//{
//    Console.WriteLine(str);
//}


