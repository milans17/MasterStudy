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
    
    public partial class tblProfessors
    {
        public tblProfessors()
        {
            this.tblCourses = new HashSet<tblCourses>();
        }
    
        public int ProfessorId { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string NameAndSurname { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Phone { get; set; }
        public string Education { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<tblCourses> tblCourses { get; set; }
    }
}
