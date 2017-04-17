using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class ExamRegistration
    {
        [DataMember]
        public int ExamRegistrationId { get; set; }
        [DataMember]
        public int StudentId { get; set; }
        [DataMember]
        public string StudentNameAndSurname { get; set; }
        [DataMember]
        public int ExamId { get; set; }
        [DataMember]
        public bool IsRegistred { get; set; }
    }
}
