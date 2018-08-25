using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forlend.Models
{
    public class LendLog
    {
        /// <summary>
        /// lend,sendback
        /// </summary>
        public string Status { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ActionBy { get; set; }
        public string WitnessBy { get; set; }
    }
}
