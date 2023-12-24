using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace MongoLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {


            try
            {
                // MongoDB'ye bağlanmak için gerekli bilgileri girin
                string connectionString = "mongodb://admin:changeme@127.0.0.1:27017/admin";
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));

                // SCRAM-SHA-1 yetkilendirme yöntemini kullanmak istiyorsanız aşağıdaki satırı ekleyin
                settings.UseSsl = false; // SSL kullanımına bağlı olarak ayarlayabilirsiniz


                // MongoClient oluşturun
                MongoClient client = new MongoClient(settings);

                // Bağlantıyı test etmek için bir veritabanı adı seçin
                string dbName = "admin";
                var database = client.GetDatabase(dbName);

                Console.WriteLine("MongoDB'ye bağlantı başarılı!");

                // İşlemlerinizi burada gerçekleştirebilirsiniz

                // Örnek bir koleksiyona erişim sağlayabilirsiniz
                var collection = database.GetCollection<BsonDocument>("wis");


                var document = new BsonDocument
                {
                    { "name", "John Doe" },
                    { "age", 30 },
                    { "email", "johndoe@example.com" }
                };


                collection.InsertOne(document);
                // Koleksiyona sorgu yapabilir, veri ekleyebilir, güncelleyebilir veya silebilirsiniz

                // Bağlantıyı kapatmayı unutmayın


            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu: " + ex.ToString());
            }

           

            return Ok();

        }
    }
}
