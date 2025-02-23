namespace Aspire.Hosting.AgileConfig
{
    public class AgileConfigOption
    {
        public static AgileConfigOption Default = new AgileConfigOption();

        public string adminConsole { get; set; } = "true";
        public string saPassword { get; set; } = "123456";
        public string defaultApp { get; set; } = "";
        public string dbProvider { get; set; } = AgileConfigDbProvider.sqlite.ToString();
        public string dbConn { get; set; } = "Data Source=agile_config.db";
    }
}
