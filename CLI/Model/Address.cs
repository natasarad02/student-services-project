using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

class Address : ISerializable
{

    public string Street { get; set; }
    public int Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public int ID { get; set; }
    public Address(string street, int number, string city, string country)
    {
        Street = street;
        Number = number;
        City = city;
        Country = country;


    }
    public string ToString2()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Street + "," + Number + "," + City + "," + Country);

        return sb.ToString();
    }

    public void FromString(string adr)
    {
        string[] address_parts = adr.Split(',');
        Street = address_parts[0];
        Number = int.Parse(address_parts[1]);
        City = address_parts[2];
        Country = address_parts[3];
        

    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            ID.ToString(), Street, Number.ToString(), City, Country
        };
        return csvValues;
    }

    public void FromCSV(string[] vals)
    {
        ID = int.Parse(vals[0]);
        Street = vals[1];
        Number = int.Parse(vals[2]);
        City = vals[3];
        Country = vals[4];
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ULICA: {Street},");
        sb.Append($"BROJ: {Number},");
        sb.Append($"GRAD: {City},");
        sb.Append($"DRZAVA: {Country}");
        return sb.ToString();
    }
}