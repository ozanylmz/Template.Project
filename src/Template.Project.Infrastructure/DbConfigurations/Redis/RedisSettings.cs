namespace Template.Project.Infrastructure.DbConfigurations.Redis
{
    public class RedisSettings
    {
        public string ConnectionString;

        //Configuration için kullanılacak
        #region Const Values

        public const string ConnectionStringValue = nameof(ConnectionString);

        #endregion
    }
}
