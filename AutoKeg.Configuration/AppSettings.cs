namespace AutoKeg.Configuration
{
    public class AppSettings
    {
        public int ListenToPin { get; set; }
        
        /// <summary>
        /// Time to wait for counter to become idle in seconds
        /// </summary>
        public int IdleTimer { get; set; }
    }

    public class MongoSettings
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }

    public class SqliteSettings
    {
        public string Database { get; set; }
    }
}