using MongoDB.Driver;
using MongoDB.Bson;
using Log_In.API.Models;

namespace Log_In.API.Data
{
    public class MongoCrud
    {
        private IMongoDatabase db;

        public MongoCrud(string database, string connectionString)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

        public LoginCredential? GetLoginCredential(string email)
        {
            var usersWithEmail = LoadRecords<LoginCredential>("Credentials").Where(record => record.Email == email);
            return usersWithEmail.FirstOrDefault();
        }

        public string? Login(LoginCredential request)
        {
            var credential = GetLoginCredential(request.Email);

            if(credential == null)
                return null;

            string rehashedPass = SHA256Hash.PasswordHash(request.Password, credential.Salt);

            if (rehashedPass != credential.Password)
                return null;


            var sessions = LoadRecords<Session>("Sessions");

            var existingSession = sessions.Where(s => s.Email == credential.Email).FirstOrDefault();

            if (existingSession != null)
                return existingSession.Token;

            string sessionToken = Guid.NewGuid().ToString();

            Session session = new Session()
            {
                Email = credential.Email,
                Token = sessionToken
            };

            InsertRecord("Sessions", session);

            return sessionToken;
        }

        public void Logout(string sessionToken)
        {
            var collection = db.GetCollection<Session>("Sessions");
            var filter = Builders<Session>.Filter.Eq("Token", sessionToken);
            collection.DeleteOne(filter);
        }

        public bool CreateUser(LoginCredential credential)
        {
            if (GetLoginCredential(credential.Email) != null) 
                return false;

            string salt = Guid.NewGuid().ToString();
            credential.Salt = salt;

            string passwordHash = SHA256Hash.PasswordHash(credential.Password, salt);
            credential.Password = passwordHash;

            InsertRecord("Credentials", credential);

            return true;  
        }
    }
}
