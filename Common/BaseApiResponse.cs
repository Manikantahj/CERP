namespace CERP.Common
{
    public class BaseApiResponse
    {
        public bool is_success { get; set; }
        public string msg { get; set; }
       // public string extra_info { get; init; }

        public object data { get; set; }

        public static BaseApiResponse Success(string msg, object data = null)
        {
            return new BaseApiResponse
            {
                is_success = true,
                msg = msg,
                data = data
            };
        }

        public static BaseApiResponse Fail(string msg)
        {
            return new BaseApiResponse
            {
                is_success = false,
                msg = msg
            };
        }
    }
}
