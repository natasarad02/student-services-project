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
    private static StudentsSubjectsDAO studentsSubjectsDAO = new StudentsSubjectsDAO();
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
        
        foreach(Student stu in students)
        {
            if (stu.index_number.ToString2().Equals(sstudent.index_number.ToString2()) && stu.First_Name.Equals(sstudent.First_Name) && stu.Last_Name.Equals(sstudent.Last_Name))
            {
                System.Console.WriteLine("Student can't be added because it already exists.");
                return sstudent;
                
            }
        }
        sstudent.ID = GenerateId();
        students.Add(sstudent);
        storage.Save(students);
        System.Console.WriteLine("Student added");
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
        System.Console.WriteLine("Student is updated");
        return oldStudent;
    }

    public Student? removeStudent(int id)  //Kad brisemo studenta, treba prvo proveriti da li postoji i zatim izbrisati sve njegove ocene i konekcije sa predmetima
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
        if(examGradeDAO1.grade_exists(student, subject))
        {
            studentsSubjectsDAO.RemoveStudentsSubjects(student, subject);
        }
    }

  
    public float average_grade(int id)
    {
        float sum = 0;
        List<ExamGrade> Passed_Exams = GetExamGrades(id);
        // BITNO ako ne nadje nista napisati grasku!!!
        int i = 0;
        for (; i != Passed_Exams.Count; i++)
        {
            sum += Passed_Exams.ElementAt(i).grade;
        }
        //treba li popuniti polje average grade?
        return sum / i;
    }

    public bool doesStudentExist(int id)
    {
        Student student = students.Find(s=> s.ID == id);
        if(student == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
