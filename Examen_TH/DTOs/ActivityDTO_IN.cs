using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.DTOs {
    public class ActivityDTO_IN {
        public int property_id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public string status { get; set; }
    }
}
