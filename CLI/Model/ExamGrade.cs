using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

public class ExamGrade : ISerializable
{

    public DateTime grading_day { get; set; }

    public int grade 
    {
        get; set;
    
    }

    public int studentID { get; set; }

    public int subjectID { get; set; }

    public int ID { get; set; }


    public ExamGrade()
    {

    }
    public ExamGrade(int studId, int subId, int gr, DateTime date)
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
            grading_day.ToString("MM-dd-yyyy"),
            grade.ToString(),
            studentID.ToString(),
            subjectID.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        ID = int.Parse(values[0]);
        string dateFormat = "MM-dd-yyyy";
        if (DateTime.TryParseExact(values[1], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            grading_day = parsedDate.Date;
        }
        grading_day = DateTime.Parse(values[1]);
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
