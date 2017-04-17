using System;
using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class Professor
    {
        [DataMember]
        public int ProfessorId { get; set; }
        [DataMember]
        public bool? IsAdmin { get; set; }
        [DataMember]
        public string NameAndSurname { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public DateTime? BirthDate { get; set; }
        [DataMember]
        public string BirthPlace { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Education { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}


