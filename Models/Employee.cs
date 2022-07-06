namespace ApiEmployee_NewWayForMinimalApi_.Models
{
    public record Employee(int Id, 
                           string FirstName, 
                           string LastName, 
                           double Salary, 
                           string Email,
                           DateTime DateOfBirth,
                           string Department,
                           string Skills);
}
