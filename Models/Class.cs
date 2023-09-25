namespace S_Assignment.Models;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; } = "CL-" + Faker.RandomNumber.Next(1000, 9999);
    public List<Student> Students { get; set; }
    public List<Course> Courses { get; set; }
}