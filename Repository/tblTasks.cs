//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTasks
    {
        public tblTasks()
        {
            this.tblCalendars = new HashSet<tblCalendars>();
        }
    
        public int TaskId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<tblCalendars> tblCalendars { get; set; }
        public virtual tblCourses tblCourses { get; set; }
        public virtual tblStudents tblStudents { get; set; }
    }
}
