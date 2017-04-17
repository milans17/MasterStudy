using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFStudy.ServiceReference;

namespace WPFStudy.Events
{
    public class ProfessorEventArgs
    {
        private Professor professor;

        public ProfessorEventArgs(Professor professor)
        {
            this.professor = professor;
        }

        public Professor Professor { get { return professor; } }
    }
}
