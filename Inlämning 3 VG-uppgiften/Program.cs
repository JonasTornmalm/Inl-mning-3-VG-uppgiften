using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.IO;

namespace Inlämning_3_VG_uppgiften
{
    class Program
    {
        static void Main(string[] args)
        {
            //MainAsync().Wait();

            //PrintAllDocuments();

            //AggregateList();

            //PrintDocumentsWithFilter();


            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

        }
        static async Task MainAsync()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("Inlämning3DB");
            var collection = db.GetCollection<BsonDocument>("Restaurants");

            var newDocuments = CreateDocuments();

            await collection.InsertManyAsync(newDocuments);

        }
        private static void AggregateList()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("Inlämning3DB");
            var collection = db.GetCollection<BsonDocument>("Restaurants");

            var filter = Builders<BsonDocument>.Filter.Gte("stars", 4);
            var found = collection.Find(filter).Project("{_id: 0, categories: 0}").ToList();

            foreach (var item in found)
            {
                Console.WriteLine(item.ToJson(new JsonWriterSettings { Indent = true }));
            }

        }
        private static void UpdateDocumentName()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("Inlämning3DB");
            var collection = db.GetCollection<BsonDocument>("Restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "456 Cookies Shop");
            var updateName = Builders<BsonDocument>.Update.Set("name", "123 Cookies Heaven");

            collection.UpdateOne(filter, updateName);
        }
        private static void UpdateDocumentIncrement()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("Inlämning3DB");
            var collection = db.GetCollection<BsonDocument>("Restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "XYZ Coffee Bar");
            var updateIncrement = Builders<BsonDocument>.Update.Inc("stars", 1);

            collection.UpdateOne(filter, updateIncrement);
        }
        private static void PrintDocumentsWithFilter()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("Inlämning3DB");
            var collection = db.GetCollection<BsonDocument>("Restaurants");

            var filter = Builders<BsonDocument>.Filter.Eq("categories", "Cafe");
            var found = collection.Find(filter).Project("{_id: 0, stars: 0, categories: 0}").ToList();


            foreach (var item in found)
            {
                Console.WriteLine(item.ToJson(new JsonWriterSettings { Indent = true }));
            }

        }
        private static void PrintAllDocuments()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("Inlämning3DB");
            var collection = db.GetCollection<BsonDocument>("Restaurants");

            var findAll = collection.Find(new BsonDocument());


            foreach (var item in findAll.ToEnumerable())
            {
                Console.WriteLine(item.ToJson(new JsonWriterSettings { Indent = true }));
            }
        }
        private static IEnumerable<BsonDocument> CreateDocuments()
        {
            var documentOne = new BsonDocument
            {
                {"_id" , "5c39f9b5df831369c19b6bca"},
                { "name", "Sun Bakery Trattoria"},
                { "stars", 4},
                { "categories", new BsonArray { "Pizza", "Pasta", "Italian", "Coffee", "Sandwiches" } }
            };

            var documentTwo = new BsonDocument
            {
                {"_id" , "5c39f9b5df831369c19b6bcb"},
                { "name", "Blue Bagels Grill"},
                { "stars", 3},
                { "categories", new BsonArray { "Bagels", "Cookies", "Sandwiches" } }
            };

            var documentThree = new BsonDocument
            {
                {"_id" , "5c39f9b5df831369c19b6bcc"},
                { "name", "Hot Bakery Cafe"},
                { "stars", 4},
                { "categories", new BsonArray { "Bakery", "Cafe", "Coffee", "Dessert" } }
            };

            var documentFour = new BsonDocument
            {
                {"_id" , "5c39f9b5df831369c19b6bcd"},
                { "name", "XYZ Coffee Bar"},
                { "stars", 5},
                { "categories", new BsonArray { "Coffee", "Cafe", "Bakery", "Chocolates" } }
            };

            var documentFive = new BsonDocument
            {
                {"_id" , "5c39f9b5df831369c19b6bce"},
                { "name", "456 Cookies Shop"},
                { "stars", 4},
                { "categories", new BsonArray { "Bakery", "Cookies", "Cake", "Coffee" } }
            };

            var newDocuments = new List<BsonDocument>();
            newDocuments.Add(documentOne);
            newDocuments.Add(documentTwo);
            newDocuments.Add(documentThree);
            newDocuments.Add(documentFour);
            newDocuments.Add(documentFive);

            return newDocuments;

        }
    }
}
