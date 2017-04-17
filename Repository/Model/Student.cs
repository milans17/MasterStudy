using System;
using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public int StudentId { get; set; }
        [DataMember]
        public int DepartmentId { get; set; }
        [DataMember]
        public int StudyProgramId { get; set; }
        [DataMember]
        public string StudyProgramName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string NameAndSurname { get; set; }
        [DataMember]
        public string BirthPlace { get; set; }
        [DataMember]
        public DateTime? BirthDate { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public int? StudyYear { get; set; }
        [DataMember]
        public int? Balance { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
