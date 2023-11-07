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
            Last_Name, First_Name, Date_Of_Birth.ToString(), Address.ToString2(),
            Phone_Number.ToString(), Email, index_number.ToString2(),
            Current_Year.ToString(), Status.ToString(), Average_Grade.ToString()
            
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Last_Name = values[0];
        First_Name = values[1];
        Date_Of_Birth = DateOnly.Parse(values[2]);
        Phone_Number = int.Parse(values[3]);
        Address.FromString(values[4]);
        Email = values[5];
        index_number.FromString(values[6]);
        Current_Year = int.Parse(values[7]); // da li treba racunati automatski tren_godina - godina_iz_indeksa
        Enum.Parse(typeof(Status), values[8], true);
        Average_Grade = int.Parse(values[9]);


    }

    public string getID()
    {
        string ID = index_number.ToString2();
        return ID;
    
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"BROJ INDEKSA: {index_number.ToString2()}, ");
        sb.Append($"IME: {First_Name + Last_Name}, ");
        sb.Append($"DATUM RODJENJA: {Date_Of_Birth}, ");
        sb.Append($"ADRESA: {Address.ToString2()}, ");
        sb.Append($"KONTAKT TELEFON: {Phone_Number.ToString()}, ");
        sb.Append($"E-MAIL: {Email.ToString()}, ");
        sb.Append($"GODINA STUDIJA: {Current_Year.ToString()}, ");
        sb.Append($"STATUS: {Status.ToString()},");
        sb.Append($"PROSECNA OCENA: {Average_Grade.ToString()}, ");
        sb.AppendJoin(", ", Passed_Exams.Select(passed_grade => passed_grade.grade));
        return sb.ToString();
    }
    
}