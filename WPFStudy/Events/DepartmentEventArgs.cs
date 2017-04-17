using System;
using WPFStudy.ServiceReference;

namespace WPFStudy.Events
{
    public class DepartmentEventArgs : EventArgs
    {
        private Department department;

        public DepartmentEventArgs(Department department)
        {
            this.department = department;
        }

        public Department Department { get { return department; } }
    }
}
