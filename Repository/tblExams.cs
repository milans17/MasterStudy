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
    
    public partial class tblExams
    {
        public tblExams()
        {
            this.tblExamRegistrations = new HashSet<tblExamRegistrations>();
        }
    
        public int ExamId { get; set; }
        public int ExamPeriodId { get; set; }
        public int CourseId { get; set; }
        public System.DateTime DateAndTime { get; set; }
        public string Place { get; set; }
        public Nullable<bool> IsPassed { get; set; }
        public Nullable<int> Price { get; set; }
    
        public virtual tblCourses tblCourses { get; set; }
        public virtual tblExamPeriods tblExamPeriods { get; set; }
        public virtual ICollection<tblExamRegistrations> tblExamRegistrations { get; set; }
    }
}