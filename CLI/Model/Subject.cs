using System.Diagnostics;
using System.Text;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

enum semester {winter, summer}
class Subject : ISerializable
{
    public int Ids { get; set; }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Espb { get; set; }

    // Id professora se serijalizuje
    // Professor se ne serijalizuje
    public int ProfessorID { get; set; }

    public List<Student> Students_passed { get; set; }

    public List<Student> Students_attending { get; set; }

    public semester semester { get; set; }

    public int year {
        get
        {
            return year;

        }
        set
        {

            if (year < 1 && year > 4) //could be 6 for students in the medical field
            {
                System.Console.WriteLine("Year must be between 1 and 4!");
            }
            else
            {
                year = value;
            }

        }
    }

    public Subject()
    {
        Students_passed = new List<Student>();
        Students_attending = new List<Student>();
    }

    public Subject(int id, string name, int espb, semester SEM, int school_year, int professorId)
    {
        Ids = id;
        Name = name;
        Espb = espb;
        semester = SEM;
        ProfessorID = professorId; // izbaciti, za pocetak treba bez profesora???
        year = school_year;
        Students_passed = new List<Student>();
        Students_attending = new List<Student>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Ids.ToString(),
            Name,
            Espb.ToString(),
            //enum semestar
            ProfessorID.ToString(),
            semester.ToString(),
            year.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ids = int.Parse(values[1]);
        Name = values[2];
        Espb = int.Parse(values[3]);
        ProfessorID = int.Parse(values[4]);
        Enum.Parse(typeof(semester), values[5], true);
        year = int.Parse(values[6]);

    }

    public string getID()
    {
        string ID = Id.ToString();
        return ID;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Ids.ToString()}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"ESPB: {Espb}, ");
        sb.Append("Semester: " + semester + ", ");
        sb.Append("STUDENTS: ");
        sb.AppendJoin(", ", Students_passed.Select(student => student.Last_Name));
        sb.AppendJoin(", ", Students_passed.Select(student => student.First_Name));
        sb.AppendJoin(", ", Students_attending.Select(student => student.Last_Name));
        sb.AppendJoin(", ", Students_attending.Select(student => student.First_Name));

        return sb.ToString();
    }
}