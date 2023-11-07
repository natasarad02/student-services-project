using System.Text;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
namespace StudentskaSluzba.Model;

enum Status{B, S}
class Student : ISerializable
{
    public string Last_Name { get; set; }
    public string First_Name { get; set; }

    public DateOnly Date_Of_Birth { get; set; }
    public Address Address { get; set; }
    public int Phone_Number { get; set; }
    public string Email { get; set; }

    public Index index_number { get; set; } // using it as Id
    public int Current_Year { get; set; }
    public Status Status { get; set; }
    public int Average_Grade { get; set; }
    public List<ExamGrade> Passed_Exams { get; set; }
    public List<ExamGrade> Failed_Exams { get; set; }


  //  public List<Subject> Subjects { get; set; }


    public Student()
    {
        Passed_Exams = new List<ExamGrade>();
        Failed_Exams = new List<ExamGrade>();
    }

    public Student(string lname, string fname, DateOnly brdate, Address adr, int num, string email, Index idnum, int cyear, Status s, int avg)
    {
        Last_Name = lname;
        First_Name = fname;
        Date_Of_Birth = brdate;
        Address = adr;
        Phone_Number = num;
        Email = email;
        index_number = idnum;
        Current_Year = cyear;
        Status = s;
        Average_Grade = avg;
        Passed_Exams = new List<ExamGrade>();
        Failed_Exams = new List<ExamGrade>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Last_Name, First_Name, Date_Of_Birth.ToString(),
            Phone_Number.ToString(), Email, index_number.ToString(),
            Current_Year.ToString(), Status.ToString(), Average_Grade.ToString()
            
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id}, ");
        sb.Append($"IME: {Name}, ");
        sb.Append("SUBJECTS:");
        sb.AppendJoin(", ", Subjects.Select(subject => subject.Name));
        // ili
        // foreach (Subject sub in Subjects)
        // {
        //     text += sub.Name + ", ";
        // }
        return sb.ToString();
    }
}