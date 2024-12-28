namespace BooksWeb.ViewModels
{
    public class UsersViewModel
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreationIDUser { get; set; }
        public DateTime? InactiveDate { get; set; }
    }
}
