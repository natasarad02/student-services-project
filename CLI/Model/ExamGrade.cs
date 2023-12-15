using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

public class ExamGrade : ISerializable
{

    public DateOnly grading_day { get; set; }

    public int grade 
    {
        get; set;
       /* get 
        {
            return grade;
        
        }


        set {

            if (grade < 11 && grade > 5)
            {
                grade = value;
            }
            else {
                System.Console.WriteLine("Grade must be between 6 and 10!");
            }

        resiti beskonacnu petlju
        }*/
    
    }

    public int studentID { get; set; }

    public int subjectID { get; set; }

    public int ID { get; set; }


    public ExamGrade()
    {

    }
    public ExamGrade(int studId, int subId, int gr, DateOnly date)
    {
        studentID = studId;
        subjectID = subId;
        grade = gr; 
        grading_day = date;

    }


    public string[] ToCSV()
    {
        string[] csvValues =
        {
            ID.ToString(),
            grading_day.ToString(),
            grade.ToString(),
            studentID.ToString(),
            subjectID.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        ID = int.Parse(values[0]);
        grading_day = DateOnly.Parse(values[1]);
        grade = int.Parse(values[2]);
        studentID = int.Parse(values[3]);
        subjectID = int.Parse(values[4]);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {ID.ToString()}, ");
        sb.Append("Date of grading: " + grading_day + ", ");
        sb.Append("Grade: " + grade + ", ");
        sb.Append("Subject: " + subjectID + ", ");
        sb.Append("Student: " + studentID);

        return sb.ToString();
    }


}
