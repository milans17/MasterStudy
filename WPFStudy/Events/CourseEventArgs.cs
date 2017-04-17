using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudy.ServiceReference;

namespace WPFStudy.Events
{
    public class CourseEventArgs : EventArgs
    {
        private Course course;

        public CourseEventArgs(Course course)
        {
            this.course = course;
        }

        public Course Course { get { return course; } }
    }
}
