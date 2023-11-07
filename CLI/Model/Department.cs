using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Katedra;

class Department : ISerializable
{
    /*
     * sifra katedre
     * naziv
     * sef
     * spisak
     */
    public int Id { get; set; }
    public string Name { get; set; }
    public string Hod { get; set; } // Head Of Department

    public List<Professor> Department_Professors { get; set; }

    public Department()
    {
        Department_Professors = new List<Professor>();
    }
    public Department(int id, string name, string hod)
    {
        Id = id;
        Name = name;
        Hod = hod;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(), Name, Hod
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Hod = values[2];
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id}, ");
        sb.Append($"IME KATEDRE: {Name}, ");
        sb.Append($"SEF KATEDRE: {Hod}, ");
        sb.Append($"PROFESORI:");
        sb.AppendJoin(", ", Department_Professors.Select(prof => prof.Name());
        return sb.ToString();
    }

}
