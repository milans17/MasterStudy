using System.Runtime.Serialization;

namespace WCFStudy
{
    [DataContract]
    public class DeleteFault
    {
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}