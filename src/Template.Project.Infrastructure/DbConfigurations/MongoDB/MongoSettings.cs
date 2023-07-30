namespace Template.Project.Infrastructure.DbConfigurations.MongoDB
{
    public class MongoSettings
    {
        public string ConnectionString;
        public string DatabaseName;

        //Configuration için kullanılacak
        #region Const Values

        public const string ConnectionStringValue = nameof(ConnectionString);
        public const string DatabaseValue = nameof(DatabaseName);

        #endregion
    }
}
