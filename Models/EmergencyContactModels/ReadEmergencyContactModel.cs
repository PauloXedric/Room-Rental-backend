namespace RRMS.Models.EmergencyContactModels
{
    public class ReadEmergencyContactModel
    { 
        public int EmergencyContactId { get; set; }

        public  string Name { get; set; } = string.Empty;

        public string Relation { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }


}
