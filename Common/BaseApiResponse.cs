namespace CERP.Common
{
    public class BaseApiResponse
    {
        public bool is_success { get; set; }
        public string msg { get; set; }
        public string extra_info { get; set; }
    }
}
