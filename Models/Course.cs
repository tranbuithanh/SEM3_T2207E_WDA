namespace S_Assignment.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; } = "CR-" + Faker.RandomNumber.Next(1000, 9999);
    public string CourseDescription { get; set; }
    public List<Student>? Students { get; set; }
    public Class? Class { get; set; }
}