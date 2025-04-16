namespace BlogAppMVC.Context.Entities;

public enum CategoryColors
{
    primary, danger, warning, success, secondary, info
}
public class Category
{
    public int CategoryId { get; set; }
    public string? Text { get; set; }
    public string? Url { get; set; }
    public CategoryColors? Color { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}