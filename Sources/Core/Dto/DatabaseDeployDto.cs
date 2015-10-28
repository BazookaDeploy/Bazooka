namespace Bazooka.Core.Dto
{
    public class DatabaseDeployDto
    {
        public string PackageName { get; set; }
        public string Version { get; set; }
        public string Repository { get; set; }
        public string ConnectionString { get; set; }
        public string DataBase { get; set; }
    }
}
