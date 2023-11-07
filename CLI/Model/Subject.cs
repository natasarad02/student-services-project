using System.Text;
using SerializationExample.Serialization;

namespace SerializationExample.Model;

class Subject : ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Espb { get; set; }

    // Id professora se serijalizuje
    public int ProfessorId { get; set; }

    // Professor se ne serijalizuje
    public Professor Professor { get; set; }

    public List<Student> Students { get; set; }

    public Subject()
    {
        Students = new List<Student>();
    }

    public Subject(int id, string name, int espb, int professorId)
    {
        Id = id;
        Name = name;
        Espb = espb;
        ProfessorId = professorId;
        Students = new List<Student>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Name,
            Espb.ToString(),
            ProfessorId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Espb = int.Parse(values[2]);
        ProfessorId = int.Parse(values[3]);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"ESPB: {Espb}, ");
        sb.Append("STUDENTS: ");
        sb.AppendJoin(", ", Students.Select(student => student.Name));
        // ili
        // foreach (Student st in Students)
        // {
        //     sb.Append($"{st.Name}, ");
        // }
        return sb.ToString();
    }
}