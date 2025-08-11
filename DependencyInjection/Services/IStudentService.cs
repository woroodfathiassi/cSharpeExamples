using DependencyInjection.DataAccess;

namespace DependencyInjection.Services;

public interface IStudentService
{
    List<Student> GetStudents();
    void AddStudent(Student student);
}
