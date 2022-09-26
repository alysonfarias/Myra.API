namespace Myra.Application.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
        public string Emissary { get; set; }
        public string ValidOn { get; set; }
    }
}
