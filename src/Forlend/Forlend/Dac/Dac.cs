using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Forlend.Models;
using MongoDB.Driver;

namespace Forlend.Dac
{
    public class Dac : IDac
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Item> ItemCollection;
        private readonly IMongoCollection<Locker> LockerCollection;
        private readonly IMongoCollection<LendItem> LendItemCollection;

        public Dac()
        {
            client = new MongoClient("mongodb://forlend:WBEBotZK6mIRC49cHbMBZI7CCeDdE1ydmlM8f15mM675DHX6SY3XZXVkVPKXqTgp9jpfPf7nwX0uQaQD8xptuw==@forlend.documents.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=150000&minPoolSize=2");
            database = client.GetDatabase("forlend");

            ItemCollection = database.GetCollection<Item>("item");
            LockerCollection = database.GetCollection<Locker>("locker");
            LendItemCollection = database.GetCollection<LendItem>("lenditem");
        }

        public void CreateItem(Item item)
        {
            ItemCollection.InsertOne(item);
        }

        public void CreateLendItem(LendItem lendItem)
        {
            LendItemCollection.InsertOne(lendItem);
        }

        public void CreateLocker(Locker locker)
        {
            LockerCollection.InsertOne(locker);
        }

        public Item GetItem(Expression<Func<Item, bool>> expression)
        {
            return ItemCollection.Find(expression).FirstOrDefault();
        }

        public LendItem GetLendItem(Expression<Func<LendItem, bool>> expression)
        {
            return LendItemCollection.Find(expression).FirstOrDefault();
        }

        public Locker GetLocker(Expression<Func<Locker, bool>> expression)
        {
            return LockerCollection.Find(expression).FirstOrDefault();
        }

        public IEnumerable<Item> ListItems(Expression<Func<Item, bool>> expression)
        {
            return ItemCollection.Find(expression).ToList();
        }

        public IEnumerable<Locker> ListLockers(Expression<Func<Locker, bool>> expression)
        {
            return LockerCollection.Find(expression).ToList();
        }

        public void UpdateItem(Item item)
        {
            ItemCollection.ReplaceOne(x => x._id == item._id, item);
        }

        public void UpdtaeLendItem(LendItem lendItem)
        {
            LendItemCollection.ReplaceOne(x => x._id == lendItem._id, lendItem);
        }
    }
}
