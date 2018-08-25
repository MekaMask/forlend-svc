using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forlend.Models
{
    public class LendItem
    {
        public string _id { get; set; }
        public string ItemId { get; set; }
        public IEnumerable<LendLog> Log { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
