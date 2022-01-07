using AutoMapper;
using Examen_TH.DTOs;
using Examen_TH.Modelos;
using Examen_TH.Modelos.Constantes;
using Examen_TH.Reglas;
using Examen_TH.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.Controllers {
    [ApiController]
    [Route("api/properties")]
    public class PropertiesController : Controller {
        private readonly ReglasPropiedades ReglasPropiedades;

        public PropertiesController(AppDBContext dc, IMapper mapper) {
            this.ReglasPropiedades = new(dc, mapper);
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyDTO>>> Get() {
            Resultado VP_Resultado = ReglasPropiedades.GetAllProperties();
            return Json(VP_Resultado);
        }

        [HttpGet("id", Name = "GetPropertyById")]
        public async Task<ActionResult<PropertyDTO>> Get(int id) {
            Resultado VP_Resultado = ReglasPropiedades.GetPropertyById(id);
            return Json(VP_Resultado);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PropertyDTO_IN propertyDTO_IN) {
            Resultado VP_Resultado = await ReglasPropiedades.InsertProperty(propertyDTO_IN);
            return Json(VP_Resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, PropertyDTO propertyDTO) {
            Resultado VP_Resultado = await ReglasPropiedades.UpdateProperty(id, propertyDTO);
            return Json(VP_Resultado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            Resultado VP_Resultado = await ReglasPropiedades.DeleteProperty(id);
            return Json(VP_Resultado);
        }
    }
}
