namespace S_Assignment.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; } = "ST-" + Faker.RandomNumber.Next(1000, 9999);
    public DateTime DateOfBirth { get; set; }
    public Course Course { get; set; }
}