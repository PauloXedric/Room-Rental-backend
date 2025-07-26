namespace RRMS.Helpers
{
    public static class LocalTimeHelper
    {
        private static readonly TimeZoneInfo _philippineTimeZone =
          TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

        public static DateTime GetPhilippineTimeNow()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _philippineTimeZone);
        }
    }
}
