using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudy.ServiceReference;

namespace WPFStudy.Events
{
    public class ExamPeriodEventArgs : EventArgs
    {
        private ExamPeriod examPeriod;

        public ExamPeriodEventArgs(ExamPeriod examPeriod)
        {
            this.examPeriod = examPeriod;
        }

        public ExamPeriod ExamPeriod { get { return examPeriod; } }
    }
}
