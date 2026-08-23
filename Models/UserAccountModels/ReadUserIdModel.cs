using RRMS.Models.Identity;
using RRMS.Models.UserAccountModels;

namespace RRMS.Models.UserAccountModels
{
    public class ReadUserIdModel
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty; 
    }
}
