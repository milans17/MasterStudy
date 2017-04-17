using System;
using System.Runtime.Serialization;

namespace Repository.Model
{
    [DataContract]
    public class ExamPeriod
    {
        [DataMember]
        public int ExamPeriodId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public int SchoolYear { get; set; }
        [DataMember]
        public bool? IsApsolvent { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }
}
