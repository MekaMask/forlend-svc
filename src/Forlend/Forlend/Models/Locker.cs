using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forlend.Models
{
    public class Locker
    {
        [BsonId]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Row { get; set; }
        public string Col { get; set; }
        public DateTime? CreateDate { get; set; }

    }
}
