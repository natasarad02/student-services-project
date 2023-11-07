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
    public int employment_year { get; set; }

    public int work_year {
        get {  return employment_year; }

        set {
            int currentYear = DateTime.Now.Year;
            work_year = currentYear - employment_year; 
            }
    }

    public string email_address { get; set; } //ubaciti regex

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


    public List<Subject> Subjects { get; set; }

    public Professor()
    {
        Subjects = new List<Subject>();
    }


    public Professor(int id, string name, string surname, Address address1, string phone, int year, DateOnly date, string email)

    {
        Id = id;
        Name = name;
        Surname = surname;
        Address = address1;
        phone_number = phone;
        email_address = email;
        employment_year = year;
        work_year = DateTime.Now.Year - employment_year;
        Subjects = new List<Subject>();
        //Zvanje???


    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Surname = values[2];
        Address.FromString(values[3]);
        phone_number = values[4];
        email_address = values[5];
        employment_year = int.Parse(values[6]); 
        birth_date = DateOnly.Parse(values[7]);

    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Name,
            Surname,
            Address.ToString2(),
            phone_number,
            email_address,
            employment_year.ToString(),
            birth_date.ToString()

        };
        return csvValues;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("ID: " + Id + ", ");
        sb.Append("NAME: " + Name + ", ");
        sb.Append("SURNAME: " + Surname + ", ");
        sb.Append("Address: " + Address.ToString2() + ", ");
        sb.Append("Phone number: "+ phone_number +", ");
        sb.Append("Email address: "+email_address+", ");
        sb.Append("Employment year: "+ employment_year +", ");
        sb.Append("Work years: "+ work_year +", ");
        sb.Append("Birthday: "+birth_date +", ");
        sb.Append("SUBJECTS: ");
        sb.AppendJoin(", ", Subjects.Select(subject => subject.Name));
        return sb.ToString();
    }
}

    

   