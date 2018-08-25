using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forlend.Models
{
    public class LendAndSendBackItemRequest
    {
        public string ItemId { get; set; }
        public string LendBy { get; set; }
        public string WitnessBy { get; set; }
    }
}
