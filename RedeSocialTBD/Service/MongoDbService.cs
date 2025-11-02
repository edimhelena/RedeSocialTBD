using MongoDB.Driver;

namespace RedeSocialTBD.Data
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuracao;
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuracao)
        {
            _configuracao = configuracao;

            string stringconexao = _configuracao.GetConnectionString("DbConnection");

            var mongourl = MongoUrl.Create(stringconexao);
            var mongoclient = new MongoClient(mongourl);
            _database = mongoclient.GetDatabase(mongourl.DatabaseName);
        }

        public IMongoDatabase Database => _database;
    }
}