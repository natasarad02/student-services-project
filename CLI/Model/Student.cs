using System.Text;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
using System.Diagnostics.Metrics;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Markup;

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

    public Index index_number { get; set; }
    public int Current_Year { get; set; }
    public Status Status { get; set; }
   public float Average_Grade {
        get { return 9; }// return Average_Grade; }
        set { }//Average_Grade = calculate_average_grade(); } //implementirati posle racunanje ovde!!!, napravljena je metoda calculate_average_grade
    }
    public List<ExamGrade> Passed_Exams { get; set; }
    public List<ExamGrade> Failed_Exams { get; set; }
    public int ID { get; set; }
   
    public Boolean Is_Deleted { get; set; }


    public Student()
    {
        Passed_Exams = new List<ExamGrade>();
        Failed_Exams = new List<ExamGrade>();
    }

    public Student(string lname, string fname, DateOnly brdate, Address adr, int num, string email, Index idnum, int cyear, Status s)
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
        Average_Grade = 0; //no grades yet
        Passed_Exams = new List<ExamGrade>();
        Failed_Exams = new List<ExamGrade>();
        Is_Deleted = false; //is not deleted when created
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            ID.ToString(), Last_Name, First_Name, Date_Of_Birth.ToString(), Address.ToString2(),
            Phone_Number.ToString(), Email, index_number.ToString2(),
            Current_Year.ToString(), Status.ToString(), Average_Grade.ToString(), Is_Deleted.ToString()
            
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        ID = int.Parse(values[0]);
        Last_Name = values[1];
        First_Name = values[2];
        Date_Of_Birth = DateOnly.Parse(values[3]);
       // System.Console.WriteLine(values[4] + " " + values[4].GetType());
        Address = Address.FromString(values[4]);
        Phone_Number = int.Parse(values[5]);
       
        Email = values[6];
        index_number = Index.FromString(values[7]);
        Current_Year = int.Parse(values[8]); // da li treba racunati automatski tren_godina - godina_iz_indeksa? IZMENITI
        Enum.Parse(typeof(Status), values[9], true);
        Average_Grade = float.Parse(values[10]);
        Is_Deleted= bool.Parse(values[11]);


    }

    public void Delete() 
    {   
        Is_Deleted = true;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
       // System.Console.WriteLine(index_number);
        sb.Append($"INDEX NUMBER: {index_number.ToString2()}, ");
        sb.Append($"NAME: {First_Name + Last_Name}, ");
        sb.Append($"DATE OF BIRTH: {Date_Of_Birth}, ");
        sb.Append($"ADDRESS: {Address.ToString2()}, ");
        sb.Append($"PHONE NUMBER: {Phone_Number.ToString()}, ");
        sb.Append($"E-MAIL: {Email.ToString()}, ");
        sb.Append($"COLLEGE YEAR: {Current_Year.ToString()}, ");
        sb.Append($"STATUS: {Status.ToString()},");
        sb.Append($"AVERAGE GRADE: {Average_Grade.ToString()}, ");
        sb.AppendJoin(", ", Passed_Exams.Select(passed_grade => passed_grade.grade));
        return sb.ToString();
    }

   /* public float calculate_average_grade()
    { 
        float sum = 0;
        int i = 0;
        for (; i!=Passed_Exams.Count; i++)
        {
            sum += Passed_Exams.ElementAt(i).grade;
        }

        return sum/i;
    }
    Ovo treba proveriti
    */
}