namespace LibraryApi.Models;
public class Author {
    public int Id {get;set;}
    public string FullName {get;set;} = "";
    public string Country {get;set;} = "";
    public List<Book> Books {get;set;} = new();
}
