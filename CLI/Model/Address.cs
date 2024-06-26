﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

public class Address : ISerializable
{

    public string Street { get; set; }
    public int Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public int ID { get; set; }

    public Address()
    {

    }
    
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
        sb.Append(Street ?? "");
        sb.Append(",");
        sb.Append(Number.ToString());
        sb.Append(",");
        sb.Append(City ?? "");
        sb.Append(",");
        sb.Append(Country ?? "");

        return sb.ToString();
    }

    public static Address FromString(string adr)
    {
        
            string[] address_parts = adr.Split(',');
          
                string Str = address_parts[0];
                int Num = int.Parse(address_parts[1]);
                string Cit = address_parts[2];
                string Countr = address_parts[3];
                return new Address(Str, Num, Cit, Countr);
           
        


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

   /* public void ToAddressDTO()
    {
        AddressDTO addressDTO = new AddressDTO(Street, Number, City, Country);
        return addressDTO;
    }*/
}