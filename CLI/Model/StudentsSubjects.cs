using System;
using System.Text;
using StudentskaSluzba.Serialization;
using System.Diagnostics.Metrics;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Markup;
namespace StudentskaSluzba.Model;


public class StudentsSubjects : ISerializable
{
    public  int studentID { get; set; }

    public  int subjectID { get; set; }

    public StudentsSubjects() { }

    public StudentsSubjects(int id1, int id2) 
    {
        studentID = id1;
        subjectID = id2;
    }

    public string[] ToCSV() {

        string[] csvValues = {
            studentID.ToString(),
            subjectID.ToString()
        };
        return csvValues;        
    }

    public void FromCSV(string[] values)
    {
        studentID = int.Parse(values[0]);
        subjectID = int.Parse(values[1]);   

    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Student id:" +studentID +", ");
        sb.Append("Subject id" + subjectID);

        return sb.ToString();
    }


}
