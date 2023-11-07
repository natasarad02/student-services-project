using System.Text;
using System.Text.RegularExpressions;
using StudentskaSluzba.Serialization;
using System.Text.RegularExpressions;

namespace StudentskaSluzba.Model;

class Professor : ISerializable
{
    public int Id { get; set; } //number of personal ID card
    public string Name { get; set; }

    public string Surname { get; set; }

    public Address Address { get; set; }

    public DateOnly birth_date { get; set; }

    public string phone_number
    {
        get { return phone_number; }

        set
        {
            string reg_pattern = @"06[0-9]\/[0-9]{6,6}[0-9]?";
            Regex rg = new Regex(reg_pattern);
            if (Regex.Match(value, reg_pattern).Success) { 
                phone_number= value;
            }
            else 
            {   
                Console.WriteLine("Phone number isn't in the right format (06x/xxxxxxx)");
            }
        }
    
    }



    }

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