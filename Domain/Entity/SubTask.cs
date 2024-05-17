namespace Domain.Entity.Tasks;

public class SubTask
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Tag { get; set; }
    
    // Foreign Keys
    public Tasks Tasks { get; set; }
}