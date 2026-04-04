namespace CERP.Logics
{
    public static class MUtils
    {

        public static DateTime? getCurrentDateTime()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }
    }
}
