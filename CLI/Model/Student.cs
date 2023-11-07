﻿using System.Text;
using SerializationExample.Serialization;

namespace SerializationExample.Model;

class Student : ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Subject> Subjects { get; set; }


    public Student()
    {
        Subjects = new List<Subject>();
    }

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
        Subjects = new List<Subject>();
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

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id}, ");
        sb.Append($"IME: {Name}, ");
        sb.Append("SUBJECTS:");
        sb.AppendJoin(", ", Subjects.Select(subject => subject.Name));
        // ili
        // foreach (Subject sub in Subjects)
        // {
        //     text += sub.Name + ", ";
        // }
        return sb.ToString();
    }
}