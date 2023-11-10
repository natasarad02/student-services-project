using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;

namespace StudentskaSluzba.DAO;
class StudentDAO
{
    private readonly List<Student> students;
    private readonly Storage<Student> storage;

    public StudentDAO(string filename)
    {
        storage = new Storage<Student>(filename + ".txt");
        students = storage.Load();
    }

    private int GenerateId()
    {
        if (students.Count == 0)
            return 0;
        return students[^1].ID + 1;
    }

    public Student? UpdateStudent(Student student)
    {
        Student? oldStudent = getStudentbyId(student.ID);
        if (oldStudent is null) return null;

        //add updates

        storage.Save(students);
        return oldStudent;
    }

    public Student? getStudentbyId (int id)
    {
        return students.Find(t => t.ID == id);
    }



}
