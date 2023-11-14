using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
//using StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

class Department : ISerializable
{
    public int Idd { get; set; } //department id
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Hod { get; set; } // Head Of Department

    public List<Professor> Department_Professors { get; set; }

    public Department()
    {
        Department_Professors = new List<Professor>();
    }
    public Department(int idd, string name, string hod)
    {
        Idd = idd;
        Name = name;
        Hod = hod;
        Department_Professors = new List<Professor>();
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Idd.ToString(), Name, Hod
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Idd = int.Parse(values[1]);
        Name = values[2];
        Hod = values[3];
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Idd}, ");
        sb.Append($"IME KATEDRE: {Name}, ");
        sb.Append($"SEF KATEDRE: {Hod}, ");
        sb.Append($"PROFESORI:");
        sb.AppendJoin(", ", Department_Professors.Select(prof => prof.Name));
        sb.AppendJoin(", ", Department_Professors.Select(prof => prof.Surname));
        return sb.ToString();
    }

}
