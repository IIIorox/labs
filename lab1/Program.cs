namespace lab1
{
    class Book
    {
        private string _title;
        private string _author;
        private DateTime _date;

        public Book(string title, string author, DateTime date)
        {
            _title = title;
            _author = author;
            _date = date;
        }

        public void DisplayInfo()
        {
            Console.WriteLine(
                $"Title: {_title}\n" +
                $"Author: {_author}\n" +
                $"Date: {_date.ToString()}"
                );
        }
    };

    public class Bootstrap
    {
        public static void Main(string[] args)
        {
            var book1 = new Book("Pipay", "Me", DateTime.Now);
            book1.DisplayInfo();
            Console.WriteLine("#########################");
            var book2 = new Book("Kto", "Ti", new DateTime(2024, 3, 5));
            book2.DisplayInfo();
        }
    }
}
