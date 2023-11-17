using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Console;

class StudentConsoleView
{
    private readonly StudentDAO studentDAO;
    private static StudentsSubjectsDAO studentsSubjectsDAO =  new StudentsSubjectsDAO();
    private static ExamGradeDAO examGradeDAO = new ExamGradeDAO();
    private static SubjectDAO subjectDAO = new SubjectDAO();
    public StudentConsoleView(StudentDAO studDAO)
    {
        studentDAO = studDAO;
    }

    private void PrintStudents(List<Student> students)
    {
        System.Console.WriteLine("Students: ");
        string header = $"INDEX NUMBER {"",10} | FIRST NAME {"",10} | LAST NAME {"", 11} | BIRTH DATE {"", 12} | ADDRESS{"",25} | PHONE NUMBER {"",12} | E-MAIL {"",30} | COLLEGE YEAR {"",4} | STATUS {"",10} | AVERAGE GRADE {"",4} |";
        
        //System.Console.WriteLine(header);
        foreach(Student s in students)
        {
            System.Console.WriteLine(s);
        }
    }

    private Student InputStudent()
    {
        Model.Index indNum = new Model.Index();
        System.Console.WriteLine("Enter student's index number: ");
        string indn = ConsoleViewUtils.SafeInputIndex(); 
        Model.Index ind_num = Model.Index.FromString(indn);
       

        System.Console.WriteLine("Enter student's last name: "); 
        string lName = ConsoleViewUtils.SafeInputName(); 
        
        System.Console.WriteLine("Enter student's first name: "); 
        string fName = ConsoleViewUtils.SafeInputName();
       
        System.Console.WriteLine("Enter student's date of birth: ");
        DateOnly br_date = ConsoleViewUtils.SafeInputDate(); 

        System.Console.WriteLine("Enter student's street: ");
        string street = ConsoleViewUtils.SafeInputName(); 
        System.Console.WriteLine("Enter student's street number: ");
        int streetNum = ConsoleViewUtils.SafeInputInt(); 
        System.Console.WriteLine("Enter student's city: ");
        string city = ConsoleViewUtils.SafeInputName(); 
        System.Console.WriteLine("Enter student's country: ");
        string country = ConsoleViewUtils.SafeInputName(); 
        Address adr = new Address(street, streetNum, city, country);


        System.Console.WriteLine("Enter student's phone number: ");
        string phone_num = ConsoleViewUtils.SafeInputPhoneNumber(); 

        System.Console.WriteLine("Enter student's e-mail: ");
        string email = ConsoleViewUtils.SafeInputEmail();

        System.Console.WriteLine("Enter student's year of college: ");
        int college_year = ConsoleViewUtils.SafeInputInt(); 

        System.Console.WriteLine("Enter student's status: ");
        Status status = ConsoleViewUtils.SafeInputStatus();
        

        return new Student(lName, fName, br_date, adr, phone_num, email, ind_num, college_year, status);
    }

    
    private int InputId()
    {
        System.Console.WriteLine("Enter student's id: ");
        return ConsoleViewUtils.SafeInputInt(); 
       
    }
   

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All students");
        System.Console.WriteLine("2: Add student");
        System.Console.WriteLine("3: Update student");
        System.Console.WriteLine("4: Remove student");
        System.Console.WriteLine("5: Show subjects for specific student");
        System.Console.WriteLine("6: Add subjects that student attends");
        System.Console.WriteLine("7: Show grades from specific student");
        System.Console.WriteLine("8: Student passed an exam - add grade");
        System.Console.WriteLine("9: Show average grade for specific student");
        System.Console.WriteLine("0: Close");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllStudents();
                break;
            case "2":
                AddStudent();
                break;
            case "3":
                UpdateStudent();
                break;
            case "4":
                RemoveStudent();
                break;
            case "5":
                System.Console.WriteLine("Enter student ID: ");
                int stud_ID = int.Parse(System.Console.ReadLine());
                System.Console.WriteLine("Enter student ID: " + stud_ID);
                List<Subject> temp = studentDAO.GetSubjects(stud_ID);
                foreach (Subject subject in temp)
                {
                    System.Console.WriteLine(subject.Name + " "+ subject.Ids);
                }
                break;
            case "6":
                System.Console.WriteLine("Enter students ID: ");
                int id = int.Parse(System.Console.ReadLine());
                
                if(!studentDAO.doesStudentExist(id))
                {
                    System.Console.WriteLine("Student doesn't exist, try again: ");
                    System.Console.WriteLine("Enter students ID: ");
                    id = int.Parse(System.Console.ReadLine());
                }
                System.Console.WriteLine("Enter subjects ID: ");
                int sub_id = int.Parse(System.Console.ReadLine());
                
                while (!subjectDAO.doesSubjectExist(sub_id))
                {
                    System.Console.WriteLine("Subject doesn't exist, try again: ");
                    System.Console.WriteLine("Enter subjects ID: ");
                    sub_id = int.Parse(System.Console.ReadLine());
                    
                }
                if (studentsSubjectsDAO.doesConnectionExist(id,sub_id))
                {
                    System.Console.WriteLine("Student is already taking this class");
                    break;
                }
                studentDAO.addStudentSubject(id, sub_id);
                break;
            case "7":
                System.Console.WriteLine("Enter students ID: ");
                int ids = int.Parse(System.Console.ReadLine());
                while (!studentDAO.doesStudentExist(ids))
                {
                    System.Console.WriteLine("Student doesn't exist, try again: ");
                    System.Console.WriteLine("Enter students ID: ");
                    ids = int.Parse(System.Console.ReadLine());
                }
                List<ExamGrade> temp1 = studentDAO.GetExamGrades(ids);
                foreach (ExamGrade exam in temp1)
                {
                    System.Console.WriteLine(exam);
                }
                break;
            case "8": //Student passed an exam - add grade
                System.Console.WriteLine("Enter students ID: ");
                int idss = int.Parse(System.Console.ReadLine());

                while (!studentDAO.doesStudentExist(idss))
                {
                    System.Console.WriteLine("Student doesn't exist, try again: ");
                    System.Console.WriteLine("Enter students ID: ");
                    idss = int.Parse(System.Console.ReadLine());
                }

                System.Console.WriteLine("Enter subjects ID: ");
                int subid = int.Parse(System.Console.ReadLine());
                

                while (examGradeDAO.grade_exists(idss, subid))
                {
                    System.Console.WriteLine("Student already passed this exam, try again:");
                    System.Console.WriteLine("Enter students ID: ");
                    idss = int.Parse(System.Console.ReadLine());
                    System.Console.WriteLine("Enter subjects ID: ");
                    subid = int.Parse(System.Console.ReadLine());
                    
                }
                                
                int grade = ConsoleViewUtils.SafeInputGrade();
                System.Console.WriteLine("Enter date in format mm/dd/yyyy:" );
                DateOnly studentDate = DateOnly.Parse(System.Console.ReadLine());
                
                studentDAO.grade(idss, subid, grade, studentDate);
                if (studentsSubjectsDAO.doesConnectionExist(idss, subid)) 
                { 
                    studentsSubjectsDAO.RemoveStudentsSubjects(idss, subid); //izgleda ne radi?
                }

                break;
            case "9":
                System.Console.WriteLine("Enter students ID: ");
                ids = int.Parse(System.Console.ReadLine());
                while (!studentDAO.doesStudentExist(ids))
                {
                    System.Console.WriteLine("Student doesn't exist, try again: ");
                    System.Console.WriteLine("Enter students ID: ");
                    ids = int.Parse(System.Console.ReadLine());
                }
                
                float avg= studentDAO.average_grade(ids);
                if (avg >= 6) 
                {
                    System.Console.WriteLine("Average grade of this student is: " + avg);
                }
                else {
                    System.Console.WriteLine("This student has no grades yet.");
                }
                break;

        }
    }
    public void RunMenu()
    {
        while(true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0")
                break;
            HandleMenuInput(userInput);
        }
    }

    private void ShowAllStudents()
    {
        PrintStudents(studentDAO.GetAllStudents());

    }

    private void RemoveStudent()
    {
        int id = InputId();
        while (!studentDAO.doesStudentExist(id))
        {
            System.Console.WriteLine("Student doesn't exist, try again: ");
            System.Console.WriteLine("Enter students ID: ");
            id = int.Parse(System.Console.ReadLine());
        }

        List<ExamGrade> grades = studentDAO.GetExamGrades(id);
        List<Subject> student_subjects = studentDAO.GetSubjects(id);
        Student? removedStudent = studentDAO.removeStudent(id);

        foreach(ExamGrade grade in grades)
        {
            
            examGradeDAO.RemoveExamGrade(grade.ID); //NE radi BITNO
        }

        
        
            List<int> sub_ids = studentsSubjectsDAO.findAllConnectionsByStudent(id);

        foreach(int subid in sub_ids)
        {            
                studentsSubjectsDAO.RemoveStudentsSubjects(id, subid);
 

        }
        
        


        if (removedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;


        }
        System.Console.WriteLine("Student is removed");
    }

    private void UpdateStudent()
    {
       int id = InputId();

        while (!studentDAO.doesStudentExist(id))
        {
            System.Console.WriteLine("Student doesn't exist, try again: ");
            System.Console.WriteLine("Enter students ID: ");
            id = int.Parse(System.Console.ReadLine());

        }
        Student student = InputStudent();
       student.ID = id;
       Student? updatedStudent = studentDAO.UpdateStudent(student);
       if (updatedStudent == null) 
       {
            System.Console.WriteLine("Student not found");
            return;
        
        
       }
      
    }

    private void AddStudent()
    {
        Student student = InputStudent();
        studentDAO.addStudent(student);
        //System.Console.WriteLine("Student is added");
    }

}