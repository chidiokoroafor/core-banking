namespace CoreBanking.DTOs
{
    public class GenericResponse
    {
        public string message { get; set; }
        public bool success { get; set; }
        public int status { get; set; }
        public object data { get; set; }
    }
}
