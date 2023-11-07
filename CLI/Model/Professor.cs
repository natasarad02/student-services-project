using System.Text;
using SerializationExample.Serialization;

namespace SerializationExample.Model;

class Professor : ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Subject> Subjects { get; set; }

    public Professor()
    {
        Subjects = new List<Subject>();
    }

    public Professor(int id, string name)
    {
        Id = id;
        Name = name;
        Subjects = new List<Subject>();
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("ID: " + Id + ", ");
        sb.Append("NAME: " + Name + ", ");
        sb.Append("SUBJECTS: ");
        sb.AppendJoin(", ", Subjects.Select(subject => subject.Name));
        // ili
        // foreach (Subject s in Subjects)
        // {
        //     sb.Append($"{s.Name}, ");
        // }
        return sb.ToString();
    }
}