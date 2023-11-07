using System.Diagnostics;
using System.Text;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

enum semester {winter, summer}
class Subject : ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Espb { get; set; }

    // Id professora se serijalizuje
    //public int ProfessorId { get; set; }

    // Professor se ne serijalizuje
    public Professor Professor { get; set; }

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
                year = value;
            }
            else
            {
                Console.WriteLine("Year must be between 1 and 4!");
            }

        }
    }

    public Subject()
    {
        Students_passed = new List<Student>();
        Students_attending = new List<Student>();
    }

    public Subject(int id, string name, int espb, semester SEM, int school_year, Professor P)
    {
        Id = id;
        Name = name;
        Espb = espb;
        semester = SEM;
        Professor = P;
        //ProfessorId = professorId;
        year = school_year;
        Students_passed = new List<Student>();
        Students_attending = new List<Student>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Name,
            Espb.ToString(),
            //ProfessorId.ToString()
            semester.ToString(),
            Professor.ToString(),
            year.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Espb = int.Parse(values[2]);
        //ProfessorId = int.Parse(values[3]);
        ///Professor = FromCSV(values[3]),
        Enum.Parse(typeof(semester), values[4], true),
        year = int.Parse(values[5]);

    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"ESPB: {Espb}, ");
        sb.Append("STUDENTS: ");
        sb.AppendJoin(", ", Students_passed.Select(student => student.Name));
        sb.AppendJoin(", ", Students_attending.Select(student => student.Name));
        return sb.ToString();
    }
}