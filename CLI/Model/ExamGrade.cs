using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

class ExamGrade : ISerializable
{

    public DateOnly grading_day { get; set; }

    public int grade 
    {

        get 
        {
            return grade;
        
        }


        set {

            if (grade < 11 && grade > 5)
            {
                grade = value;
            }
            else {
                Console.WriteLine("Grade must be between 6 and 10!");
            }

        }
    
    }

    public Student student { get; set; }

    public Subject subject { get; set; }

    public int ID { get; set; }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            ID.ToString(),
            grading_day.ToString(),
            grade.ToString(),
            student.ToString(),
            subject.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        ID = int.Parse(values[0]);
        grading_day = DateOnly.Parse(values[1]);
        grade = int.Parse(values[2]);
        // student = 
        //subject = 

    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Date of grading: " + grading_day + ", ");
        sb.Append("Grade: " + grade);

        return sb.ToString();
    }


}
