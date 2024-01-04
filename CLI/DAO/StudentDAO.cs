using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
using CLI.Observer;
using System.Xml.Linq;

namespace StudentskaSluzba.DAO;

public class StudentDAO
{
    private readonly List<Student> students;
    private readonly Storage<Student> storage;
    private static StudentsSubjectsDAO studentsSubjectsDAO = new StudentsSubjectsDAO();
    private static ExamGradeDAO examGradeDAO1 = new ExamGradeDAO();

    public SubjectOB StudentSubject;

    public StudentDAO()
    {
        storage = new Storage<Student>("students.txt");
        students = storage.Load();
        StudentSubject = new SubjectOB();
    }

    private int GenerateId()
    {
        if (students.Count == 0)
            return 0;
        return students[^1].ID + 1;
    }

    public void save() {
        storage.Save(students);
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
        StudentSubject.NotifyObservers();
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
        StudentSubject.NotifyObservers();
        System.Console.WriteLine("Student is updated");
        return oldStudent;
    }

    public Student? removeStudent(int id)  //Kad brisemo studenta, treba prvo proveriti da li postoji i zatim izbrisati sve njegove ocene i konekcije sa predmetima
    {
        
        Student? oldStudent = getStudentbyId(id);
        if (oldStudent is null) return null;

        students.Remove(oldStudent);
        storage.Save(students);
        StudentSubject.NotifyObservers();
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
    
   public void addStudentSubject(StudentsSubjectsDAO studSubDAO, int id_student, int id_subject)
    {
       
        StudentsSubjects connection = new StudentsSubjects(id_student, id_subject);
        studSubDAO.AddStudentsSubjects(connection);
        
    }

    public List<ExamGrade> GetExamGrades(int studID)
    {
        return examGradeDAO1.GetExamGradesByStudent(studID);
    }

   public void grade(ExamGradeDAO examGrDAO, int student, int subject, int grade, DateTime date)
    {

        ExamGrade exam = new ExamGrade(student, subject, grade, date);
        examGrDAO.AddExamGrade(exam);
        
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

    public List<Student> sortedStudent(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
    {
        IEnumerable<Student> sstudents = students;

        switch (sortCriteria)
        {
            case "Id":
                sstudents = students.OrderBy(x => x.ID); 
                break;
            case "Name":
                sstudents = students.OrderBy(x => x.First_Name);
                break;
            case "Last name":
                sstudents = students.OrderBy(x => x.Last_Name);
                break;
            case "Current year":
                sstudents = students.OrderBy(x => x.Current_Year);
                break;
            case "Status":
                sstudents = students.OrderBy(x => x.Status);
                break;
            case "Average grade": //smisliti kako, mozda za svakog studenta set average grade i onda poredjati?
                foreach (Student s in students) {
                    s.Average_Grade = average_grade(s.ID);
                }
                sstudents = students.OrderBy(x => x.Average_Grade);
                break;
        }

        if (sortDirection == SortDirection.Descending)
            sstudents = sstudents.Reverse();

        sstudents = sstudents.Skip((page - 1) * pageSize).Take(pageSize);

        return sstudents.ToList();
    }

}
