using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class Department
    {
        [DataMember]
        public int DepartmentId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int? FoundationYear { get; set; }
        [DataMember]
        public string DepartmentHead { get; set; }
        [DataMember]
        public string Website { get; set; }
    }
}
