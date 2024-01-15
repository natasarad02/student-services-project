using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
//using StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

public class Department : ISerializable
{
    public int Idd { get; set; } //department id
    public int Id { get; set; } //id for search
    public string Name { get; set; }
    public string Hod { get; set; } // Head Of Department

    public  List<int> Department_Professors { get; set; }

    public Department()
    {
       Department_Professors = new List<int>();
    }
    public Department(int idd, string name, string hod)
    {
        Idd = idd;
        Name = name;
        Hod = hod;
        Department_Professors = new List<int>();
    }

    public Department(string name, string hod)
    {
        Name = name;
        Hod = hod;
    }

    public string[] ToCSV()
    {
        /* List<String> Department_Professors_String = new List<String>();
         foreach (int profId in Department_Professors)
         {
             Department_Professors_String.Add(profId.ToString());
         }
         string[] csvValues =
           {
               Id.ToString(), Idd.ToString(), Name, Hod, string.Join(",", Department_Professors_String)

           };*/

        /*string[] csvValues =
        {
            Id.ToString(), Idd.ToString(), Name, Hod
        };*/

        string professorIds = string.Join(",", Department_Professors.Select(id => id.ToString()));

        string[] csvValues =
        {
                Id.ToString(), Idd.ToString(), Name, Hod, professorIds
            };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {

        Id = int.Parse(values[0]);
        Idd = int.Parse(values[1]);
        Name = values[2];
        Hod = values[3];

        if (values.Length > 4 && !string.IsNullOrEmpty(values[4]))
        {
            string professorIds = values[4];
            Department_Professors = professorIds.Split(',')
                                                  .Where(id => !string.IsNullOrEmpty(id))
                                                  .Select(int.Parse)
                                                  .ToList();
        }
        else
        {
            Department_Professors = new List<int>();
        }

    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"DEP_ID: {Idd}, ");
        sb.Append($"DEPARTMENT NAME: {Name}, ");
        sb.Append($"HEAD OF DEPARTMENT: {Hod} ");
        sb.Append($"Professor list: \n");

        foreach (int professorId in Department_Professors)
        {
            sb.Append($"Professor ID: {professorId.ToString()}, ");
        }

        return sb.ToString();

    }

    
}
