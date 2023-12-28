using System.Text;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
using System.Diagnostics.Metrics;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Markup;
using System.Globalization;

namespace StudentskaSluzba.Model;

public enum Status{B, S}
public class Student : ISerializable
{

    public string Last_Name { get; set; }
    public string First_Name { get; set; }

    public DateTime Date_Of_Birth { get; set; }
    public Address Address { get; set; }
    public string Phone_Number { get; set; }
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

    public Student(string lname, string fname, DateTime brdate, Address adr, string num, string email, Index idnum, int cyear, Status s)
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
            ID.ToString(), Last_Name, First_Name, Date_Of_Birth.ToString("MM-dd-yyyy"), Address.ToString2(),
            Phone_Number, Email, index_number.ToString2(),
            Current_Year.ToString(), Status.ToString()
            
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        ID = int.Parse(values[0]);
        Last_Name = values[1];
        First_Name = values[2];

        string dateFormat = "MM-dd-yyyy";
        if (DateTime.TryParseExact(values[3], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            Date_Of_Birth = parsedDate.Date; // Use .Date to get only the date part
        }
        Date_Of_Birth = DateTime.Parse(values[3]);
       // System.Console.WriteLine(values[4] + " " + values[4].GetType());
        Address = Address.FromString(values[4]);
        Phone_Number = values[5];
       
        Email = values[6];
        index_number = Index.FromString(values[7]);
        Current_Year = int.Parse(values[8]); // da li treba racunati automatski tren_godina - godina_iz_indeksa? IZMENITI
        
        if (values[9].Equals("B"))
        {
            Status = Status.B;
        }
        else {
            Status = Status.S;
        }


    }

    public void Delete() 
    {   
        Is_Deleted = true;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        // System.Console.WriteLine(index_number);
        sb.Append($"ID: {ID.ToString()}, ");
        sb.Append($"INDEX NUMBER: {index_number.ToString2()}, ");
        sb.Append($"NAME: {First_Name +" "+ Last_Name}, ");
        sb.Append($"DATE OF BIRTH: {Date_Of_Birth}, ");
        sb.Append($"ADDRESS: {Address.ToString2()}, ");
        sb.Append($"PHONE NUMBER: {Phone_Number.ToString()}, ");
        sb.Append($"E-MAIL: {Email.ToString()}, ");
        sb.Append($"COLLEGE YEAR: {Current_Year.ToString()}, ");
        sb.Append($"STATUS: {Status.ToString()},");
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