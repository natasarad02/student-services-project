using System.Text;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
using System.Diagnostics.Metrics;
using System.IO;

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
    public int ID { get; set; }


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
            ID.ToString(), Last_Name, First_Name, Date_Of_Birth.ToString(), Address.ToString2(),
            Phone_Number.ToString(), Email, index_number.ToString2(),
            Current_Year.ToString(), Status.ToString(), Average_Grade.ToString()
            
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        ID = int.Parse(values[0]);
        Last_Name = values[1];
        First_Name = values[2];
        Date_Of_Birth = DateOnly.Parse(values[3]);
        Phone_Number = int.Parse(values[4]);
        Address.FromString(values[5]);
        Email = values[6];
        index_number.FromString(values[7]);
        Current_Year = int.Parse(values[8]); // da li treba racunati automatski tren_godina - godina_iz_indeksa
        Enum.Parse(typeof(Status), values[9], true);
        Average_Grade = int.Parse(values[10]);


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