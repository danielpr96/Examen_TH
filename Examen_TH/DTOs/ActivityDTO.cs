using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.DTOs {
    public class ActivityDTO {
        public int id { get; set; }
        public int property_id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        public string condition { get; set; }
        public PropertyDTO_Short property { get; set; }
        public int survey { get; set; }
    }
}
