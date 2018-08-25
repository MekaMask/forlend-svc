using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forlend.Models
{
    public class Item
    {
        [BsonId]
        public string _id { get; set; }
        public string Name { get; set; }
        
        public Locker locker { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public bool Canlend { get; set; }
        public bool CanSendBack { get; set; }

        //public IEnumerable<LendLog> Log { get; set; }
    }
}
