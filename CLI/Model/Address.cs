﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

class Address : ISerializable
{
    /*
     *  ulica
     *  broj
     *  grad
     *  drzava
     */ 

    public string Street { get; set; }
    public int Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public Address(string street, int number, string city, string country)
    {
        Street = street;
        Number = number;
        City = city;
        Country = country;


    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Street, Number.ToString(), City, Country
        };
        return csvValues;
    }

    public void FromCSV(string[] vals)
    {
        Street = vals[0];
        Number = int.Parse(vals[1]);
        City = vals[2];
        Country = vals[3];
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