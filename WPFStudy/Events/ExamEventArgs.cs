using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudy.ServiceReference;

namespace WPFStudy.Events
{
    public class ExamEventArgs : EventArgs
    {
        private Exam exam;

        public ExamEventArgs(Exam exam)
        {
            this.exam = exam;
        }

        public Exam Exam { get { return exam; } }
    }
}
