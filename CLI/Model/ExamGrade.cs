using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Serialization;

namespace CLI.Model;

internal class ExamGrade : ISerializable
{

    public DateOnly grading_day { get; set; }

    public int grade 
    {

        get 
        {
            return grade;
        
        }


        set {

            if (grade < 11 && grade > 7)
            {
                grade = value;
            }
            else {
                Console.WriteLine("Grade must be between 6 and 10!");
            }

        }
    
    }

    //student?
    // predmet

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            grading_day.ToString(),
            grade.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        grading_day = DateOnly.Parse(values[0]);
        grade = int.Parse(values[1]);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Date of grading: " + grading_day + ", ");
        sb.Append("Grade: " + grade);

        return sb.ToString();
    }


}
