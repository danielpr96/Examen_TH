using AutoMapper;
using Examen_TH.DTOs;
using Examen_TH.Modelos.Constantes;
using Examen_TH.Reglas;
using Examen_TH.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.Controllers {
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : Controller {
        private readonly ReglasActividades ReglasActividades;

        public ActivitiesController(AppDBContext dc, IMapper mapper) {
            this.ReglasActividades = new(dc, mapper);
        }

        [HttpPost("activity_params")]
        public async Task<ActionResult<List<ActivityDTO>>> Get(ActivityDTO_Params activity_params) {
            Resultado VP_Resultado = ReglasActividades.GetAllActivities(activity_params);
            return Json(VP_Resultado);
        }

        [HttpGet("id", Name = "GetActivityById")]
        public async Task<ActionResult<ActivityDTO>> Get(int id) {
            Resultado VP_Resultado = ReglasActividades.GetActivityById(id);
            return Json(VP_Resultado);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActivityDTO_IN activityDTO_IN) {
            Resultado VP_Resultado = await ReglasActividades.InsertActivity(activityDTO_IN);
            return Json(VP_Resultado);
        }

        [HttpPut]
        [Route("ReScheduleActivity")]
        public async Task<ActionResult> ReScheduleActivity(int id, ActivityDTO_UP activityDTO_UP) {
            Resultado VP_Resultado = await ReglasActividades.ReScheduleActivity(id, activityDTO_UP);
            return Json(VP_Resultado);
        }

        [HttpPut]
        [Route("CancelActivity")]
        public async Task<ActionResult> CancelActivity(int id) {
            Resultado VP_Resultado = await ReglasActividades.CancelActivity(id);
            return Json(VP_Resultado);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, ActivityDTO activityDTO) {
        //    Resultado VP_Resultado = await ReglasActividades.UpdateActivity(id, activityDTO);
        //    return Json(VP_Resultado);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id) {
        //    Resultado VP_Resultado = await ReglasActividades.DeleteActivity(id);
        //    return Json(VP_Resultado);
        //}
    }
}
