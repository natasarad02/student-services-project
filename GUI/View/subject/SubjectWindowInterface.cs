using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace GUI.View
{
    public interface SubjectWindowInterface
    {
        double Left { get; set; }
        double Top { get; set; }
        double Width { get; }
        double Height { get; }

        bool IsEnabled { get; set;  }
        void Show();
    }
}
