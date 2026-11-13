namespace LibraryDemo.ViewModels
{
    //列表里的一行数据
    public class BookListItemVm
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
        public string Publisher { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
    }
}