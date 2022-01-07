using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.Modelos.Constantes {
    public class Resultado {
        public int result_code { set; get; }
        public string result_message { set; get; }
        public object result_data { set; get; }

        public Resultado() {
            result_data = new object();
        }
    }
}
