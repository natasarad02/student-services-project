using System.Text;
using System.Text.RegularExpressions;
using StudentskaSluzba.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

namespace StudentskaSluzba.Model;

public class Professor : ISerializable
{
    public int Id { get; set; }

    public int num { get; set; } //number of personal ID card

    public string Name { get; set; }

    public string Surname { get; set; }

    public Address Address { get; set; }

    public DateTime birth_date { get; set; }
    public int employment_year { get; set; }

    public int work_year {
        get; set;
        
    }

    public string email_address { get; set; } //ubaciti regex

    public string phone_number //promeniti regex
    
        { get; set; }
          
    

    public string calling { get; set; }


    public List<Subject> Subjects { get; set; }

    public Professor()
    {
        Subjects = new List<Subject>();
    }


    public Professor(int card, string name, string surname, Address address1, string phone, DateTime br_date, int year, string email, string calling)

    {
        //Id = id;
        Name = name;
        Surname = surname;
        Address = address1;
        birth_date = br_date;
        phone_number = phone;
        email_address = email;
        employment_year = year;
        work_year = DateTime.Now.Year - employment_year;
        num = card;
        Subjects = new List<Subject>();
        this.calling = calling;


    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
        Surname = values[2];
        Address = Address.FromString(values[3]);
        phone_number = values[4];
        email_address = values[5];
        employment_year = int.Parse(values[6]);
        string dateFormat = "MM-dd-yyyy";
        if (DateTime.TryParseExact(values[7], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            birth_date = parsedDate.Date; // Use .Date to get only the date part
        }
        num = int.Parse(values[8]); 
        calling = values[9];

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
            birth_date.ToString("MM-dd-yyyy"),
            num.ToString(),
            calling

        };
        return csvValues;
    }

    public override string ToString()
    {
        int worky = DateTime.Now.Year - employment_year;
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append("ID card: " + num + ", ");
        sb.Append("NAME: " + Name + ", ");
        sb.Append("SURNAME: " + Surname + ", ");
        sb.Append("Address: " + Address.ToString2() + ", ");
        sb.Append("Phone number: "+ phone_number +", ");
        sb.Append("Email address: "+email_address+", ");
        sb.Append("Employment year: "+ employment_year +", ");
        sb.Append("Work years: "+ worky + ", ");
        sb.Append("Birthday: "+birth_date +", ");
        sb.Append("Calling: " +calling);
        return sb.ToString();
    }
}

    

   