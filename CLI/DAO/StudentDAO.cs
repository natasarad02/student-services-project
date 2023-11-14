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
    private StudentsSubjectsDAO studentsSubjectsDAO = new StudentsSubjectsDAO();
    private static ExamGradeDAO examGradeDAO1 = new ExamGradeDAO();

    public StudentDAO()
    {
        storage = new Storage<Student>("students.txt");
        students = storage.Load();
    }

    private int GenerateId()
    {
        if (students.Count == 0)
            return 0;
        return students[^1].ID + 1;
    }

    public Student addStudent(Student sstudent) { 
        sstudent.ID = GenerateId();
        students.Add(sstudent);
        storage.Save(students);
        return sstudent;    
    }

    public Student? UpdateStudent(Student student)
    {
        Student? oldStudent = getStudentbyId(student.ID);
        if (oldStudent is null) return null;

        oldStudent.Last_Name = student.Last_Name;
        oldStudent.First_Name = student.First_Name;
        oldStudent.Date_Of_Birth = student.Date_Of_Birth;
        oldStudent.Address = student.Address;
        oldStudent.Phone_Number = student.Phone_Number;
        oldStudent.Email = student.Email;
        oldStudent.index_number = student.index_number;
        oldStudent.Current_Year = student.Current_Year;
        oldStudent.Status = student.Status;
        oldStudent.Average_Grade = student.Average_Grade;
        oldStudent.Passed_Exams = student.Passed_Exams;
        oldStudent.Failed_Exams = student.Failed_Exams;


        storage.Save(students);
        return oldStudent;
    }

    public Student? removeStudent(int id)
    {
        Student? oldStudent = getStudentbyId(id);
        if (oldStudent is null) return null;

        students.Remove(oldStudent);
        storage.Save(students);
        return oldStudent;
    }

    public Student? getStudentbyId (int id)
    {
        return students.Find(t => t.ID == id);
    }

    public List<Student> GetAllStudents()
    {
        return students;
    }

    public List<Subject> GetSubjects(int subjectID)
    {
        return studentsSubjectsDAO.GetSubjects(subjectID);
    }
    
    public void addStudentSubject(int id_student, int id_subject)
    {
        StudentsSubjects connection = new StudentsSubjects(id_student, id_subject);
        studentsSubjectsDAO.AddStudentsSubjects(connection);
    }

    public List<ExamGrade> GetExamGrades(int studID)
    {
        return examGradeDAO1.GetExamGradesByStudent(studID);
    }

    public void grade(int student, int subject, int grade, DateOnly date)
    {
        ExamGrade exam = new ExamGrade(student, subject, grade, date);
        examGradeDAO1.AddExamGrade(exam);

    }


}
