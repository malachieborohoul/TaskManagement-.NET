using Domain.Entity.Tasks;


public class Activity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
 
    
    // Foreign Keys
    public Tasks Tasks { get; set; }
}