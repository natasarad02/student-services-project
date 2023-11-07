using System.Text;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
namespace StudentskaSluzba.Model;

class Student : ISerializable
{
    public string Last_Name { get; set; }

    public Index index_number { get; set; } // using it as Id
    public string Name { get; set; }

    public List<Subject> Subjects { get; set; }


    public Student()
    {
        Subjects = new List<Subject>();
    }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
        Subjects = new List<Subject>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Name
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