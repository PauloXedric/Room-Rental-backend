using RRMS.Abstractions;

namespace RRMS.Helpers
{
    public class AuditHelper
    {
        public static void SetCreatedAndModifiedOn(IAuditable entity)
        {
            var now = LocalTimeHelper.GetPhilippineTimeNow();
            entity.CreatedOn = now;
            entity.ModifiedOn = now;
        }

        public static void SetModifiedOn(IAuditable entity)
        {
            entity.ModifiedOn = LocalTimeHelper.GetPhilippineTimeNow();
        }
    }
}
