using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.DTOs {
    public class ActivityDTO_Params {
        public DateTime? initial_date { get; set; }
        public DateTime? final_date { get; set; }
        public string status { get; set; }
    }
}
