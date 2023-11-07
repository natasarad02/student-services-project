using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

class StudentSubject : ISerializable
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public StudentSubject()
    {
    }

    public StudentSubject(int studentId, int subjectId)
    {
        StudentId = studentId;
        SubjectId = subjectId;
    }

    public void FromCSV(string[] values)
    {
        StudentId = int.Parse(values[0]);
        SubjectId = int.Parse(values[1]);
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            StudentId.ToString(),
            SubjectId.ToString()
        };
        return csvValues;
    }
}