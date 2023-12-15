using System.Diagnostics;
using System.Text;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

public enum semester {winter, summer}
public class Subject : ISerializable
{
    public int Ids { get; set; }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Espb { get; set; }

    public int ProfessorID { get; set; }

    public List<Student> Students_passed { get; set; }

    public List<Student> Students_attending { get; set; }

    public semester semester { get; set; }

    public int year {
        get; set;
       
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
        ProfessorID = professorId; 
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
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"SUB_ID: {Ids.ToString()}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"ESPB: {Espb}, ");
        sb.Append("Semester: " + semester + ", ");
        sb.Append("Professors ID:" +ProfessorID);
        
        return sb.ToString();
    }
}