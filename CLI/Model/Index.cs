using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model;

class Index : ISerializable
{

    public string college_major { get; set; }

    public int number_mark { get; set; }

    public int YOE { get; set; } //year of enrollment

    //dodati ogranicenja za ova 3

    public string[] ToCSV() {
        string[] csvValues =
        {
            college_major.ToString(),
            number_mark.ToString(),
            YOE.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values) {
        college_major = values[0]; 
        number_mark = int.Parse(values[1]);
        YOE = int.Parse(values[2]);
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
