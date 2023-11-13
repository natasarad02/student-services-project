using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

class Index : ISerializable
{

    public int ID { get; set; }
    public string college_major { get; set; }

    public int number_mark { get; set; }

    public int YOE { get; set; } //year of enrollment

    //dodati ogranicenja za ova 3
    public Index()
    {

    }
    public Index(string cm, int nm, int y)
    {
        college_major = cm;
        number_mark = nm;
        YOE = y;
    }

    public string[] ToCSV() {
        string[] csvValues =
        {
            ID.ToString(),
            college_major.ToString(),
            number_mark.ToString(),
            YOE.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values) {
        ID = int.Parse(values[0]);
        college_major = values[1]; 
        number_mark = int.Parse(values[2]);
        YOE = int.Parse(values[3]);
    }

    public string ToString2()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(college_major+"-"+number_mark + "-" + YOE);

        return sb.ToString();
    }

    public static Index FromString(string index)
    {
        string[] index_parts = index.Split('-');
        string college_major = index_parts[0];
        int number_mark = int.Parse(index_parts[1]);
        int YOE = int.Parse(index_parts[2]);
        return new Index(college_major, number_mark, YOE);

    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("College major mark: " + college_major + ", ");
        sb.Append("Enrollment number mark: " + number_mark + ", ");
        sb.Append("Enrollment year: " + YOE);

        return sb.ToString();
    }

}
