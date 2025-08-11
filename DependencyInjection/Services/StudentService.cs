using DependencyInjection.DataAccess;

namespace DependencyInjection.Services;

public class StudentService : IStudentService
{
    private readonly MockUpData _mockUpData;
    private readonly LoggerService _logger;

    public StudentService(LoggerService logger, MockUpData mockUpData)
    {
        _mockUpData = mockUpData;
        _logger = logger;
    }

    public List<Student> GetStudents()
    {
        _logger.Log("GetStudents!");
        return _mockUpData.Students;
    }

    public void AddStudent(Student student)
    {
        _logger.Log($"{student.Name} added!");
        _mockUpData.Students.Add(student);
    }
}
