using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.DTOs {
    public class SurveyDTO {
        public int id { get; set; }
        public int activity_id { get; set; }
        public string answers { get; set; }
        public DateTime created_at { get; set; }
    }
}
