using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudy.ServiceReference;

namespace WPFStudy.Models
{
    public class StudentRecordViewModel
    {
        public StudentRecordViewModel(Student s)
        {
            studentId = s.StudentId;
        }

        private int studentId;

        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

    }
}
