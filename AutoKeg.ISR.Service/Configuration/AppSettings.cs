namespace AutoKeg.ISR.Service.Configuration
{
    public class AppSettings
    {
        public int ListenToPin { get; set; }
        public MongoSettings Mongo { get; set; }
    }

    public class MongoSettings
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}