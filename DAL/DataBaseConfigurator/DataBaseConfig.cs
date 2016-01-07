using EntityModels;

namespace DAL.DataBaseConfigurator
{
    public static class DataBaseConfig
    {
        public static void Config()
        {
            using (var context = new Entities())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}