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

    public List<Professor> department_professors { get; set; }

    public Department()
    {
        department_professors = new List<Professor>();
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


}
