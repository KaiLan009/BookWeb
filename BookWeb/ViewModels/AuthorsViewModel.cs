namespace BooksWeb.ViewModels
{
    public class AuthorsViewModel
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public int BornYear { get; set; }
        public int DiedYear { get; set; }
        public string Nationality { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreationIDUser { get; set; }
        public DateTime? InactiveDate { get; set; }
    }
}
