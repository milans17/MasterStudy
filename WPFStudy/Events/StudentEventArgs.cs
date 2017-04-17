using System;
using WPFStudy.Models;
using WPFStudy.ServiceReference;

namespace WPFStudy.Events
{
    public class StudentEventArgs : EventArgs
    {
        private Student student;

        public StudentEventArgs(Student student)
        {
            this.student = student;
        }

        public Student Student { get { return student; } }
        //private StudentRecordViewModel student;

        //public StudentEventArgs(StudentRecordViewModel student)
        //{
        //    this.student = student;
        //}

        //public StudentRecordViewModel Student { get { return student; } }
    }
}
