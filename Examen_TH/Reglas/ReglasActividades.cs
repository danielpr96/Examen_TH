using AutoMapper;
using Examen_TH.DTOs;
using Examen_TH.Modelos;
using Examen_TH.Modelos.Constantes;
using Examen_TH.Utilidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.Reglas {
    public class ReglasActividades {
        private readonly AppDBContext dc;
        private readonly IMapper mapper;

        public ReglasActividades(AppDBContext dc, IMapper mapper) {
            this.dc = dc;
            this.mapper = mapper;
        }

        public Resultado GetAllActivities(ActivityDTO_Params activity_params) {
            DateTime VP_F_Inicio = (activity_params.initial_date != null) ? Convert.ToDateTime(activity_params.initial_date) : DateTime.Now.AddDays(-3);
            DateTime VP_F_Fin = (activity_params.final_date != null) ? Convert.ToDateTime(activity_params.final_date) : DateTime.Now.AddDays(15);
            string VP_Status = (!string.IsNullOrEmpty(activity_params.status)) ? activity_params.status : "";

            List<Activity> VP_Li_Actividad = dc.Activities.Where(w => w.schedule >= VP_F_Inicio && w.schedule <= VP_F_Fin && (VP_Status != "" && w.status == VP_Status)).ToList();
            List<ActivityDTO> VP_Li_Actividad_DTO = new();

            foreach(Activity Actividad in VP_Li_Actividad) {
                ActivityDTO VP_Actividad_DTO = Completar_ActividadDTO(Actividad);
                VP_Li_Actividad_DTO.Add(VP_Actividad_DTO);
            }

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.Success,
                result_message = "OK",
                result_data = VP_Li_Actividad_DTO
            };

            return VP_Resultado;
        }

        public Resultado GetActivityById(int id) {
            Activity VP_Actividad = dc.Activities.FirstOrDefault(p => p.id == id);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.NotFound,
                result_message = "Not Found",
                result_data = null
            };

            if (VP_Actividad != null) {
                ActivityDTO VP_Actividad_DTO = Completar_ActividadDTO(VP_Actividad);

                VP_Resultado.result_code = (int)StatusCodes.Success;
                VP_Resultado.result_message = "OK";
                VP_Resultado.result_data = VP_Actividad_DTO;
            }

            return VP_Resultado;
        }

        public async Task<Resultado> InsertActivity(ActivityDTO_IN ActivityDTO_In) {
            Resultado VP_Resultado = new();

            if (ActivityDTO_In.property_id > 0) {
                Property VP_Propiedad = dc.Properties.Where(w => w.id == ActivityDTO_In.property_id).FirstOrDefault();

                if (VP_Propiedad != null && VP_Propiedad.status == "Active") {
                    DateTime VP_F_Inicio = ActivityDTO_In.schedule;
                    DateTime VP_F_Fin = ActivityDTO_In.schedule.AddHours(1);

                    List<Activity> VP_Li_Actividades = dc.Activities.Where(w => w.property_id == VP_Propiedad.id && w.schedule >= VP_F_Inicio && w.schedule <= VP_F_Fin).ToList();

                    if (VP_Li_Actividades.Count() == 0) {
                        Activity VP_Actividad = mapper.Map<Activity>(ActivityDTO_In);
                        VP_Actividad.created_at = DateTime.Now;
                        VP_Actividad.updated_at = DateTime.Now;

                        dc.Add(VP_Actividad);
                        await dc.SaveChangesAsync();

                        Survey VP_Encuesta = new() {
                            activity_id = VP_Actividad.id,
                            answers = "",
                            created_at = DateTime.Now
                        };

                        dc.Add(VP_Encuesta);
                        await dc.SaveChangesAsync();

                        ActivityDTO VP_Actividad_DTO = Completar_ActividadDTO(VP_Actividad);

                        VP_Resultado.result_code = (int)StatusCodes.Success;
                        VP_Resultado.result_message = "OK.";
                        VP_Resultado.result_data = VP_Actividad_DTO;
                    } else {
                        VP_Resultado.result_code = (int)StatusCodes.NoProcede;
                        VP_Resultado.result_message = "Ya existe una Actividad en esa Propiedad a esa hora.";
                        VP_Resultado.result_data = ActivityDTO_In;
                    }
                } else {
                    VP_Resultado.result_code = (int)StatusCodes.NoProcede;
                    VP_Resultado.result_message = "No se puede crear una Actividad en una Propiedad inactiva.";
                    VP_Resultado.result_data = ActivityDTO_In;
                }
            } else {
                VP_Resultado.result_code = (int)StatusCodes.NoProcede;
                VP_Resultado.result_message = "Falta indicar la Propiedad.";
                VP_Resultado.result_data = ActivityDTO_In;
            }

            return VP_Resultado;
        }

        public async Task<Resultado> ReScheduleActivity(int id, ActivityDTO_UP ActivityDTO_UP) {
            Resultado VP_Resultado = new();

            Activity VP_Actividad = await dc.Activities.FirstOrDefaultAsync(p => p.id == id);

            if (VP_Actividad != null) {
                ActivityDTO VP_Actividad_DTO = mapper.Map<ActivityDTO>(VP_Actividad);

                if (VP_Actividad.status == "Active") {
                    VP_Actividad.schedule = ActivityDTO_UP.schedule;
                    VP_Actividad.updated_at = DateTime.Now;

                    dc.Entry(VP_Actividad).State = EntityState.Modified;
                    await dc.SaveChangesAsync();

                    VP_Actividad_DTO = Completar_ActividadDTO(VP_Actividad);

                    VP_Resultado.result_code = (int)StatusCodes.Success;
                    VP_Resultado.result_message = "OK";
                    VP_Resultado.result_data = VP_Actividad_DTO;
                } else {
                    VP_Resultado.result_code = (int)StatusCodes.NoProcede;
                    VP_Resultado.result_message = "No se puede reagendar una Actividad inactiva.";
                    VP_Resultado.result_data = VP_Actividad_DTO;
                }
            } else {
                VP_Resultado.result_code = (int)StatusCodes.NotFound;
                VP_Resultado.result_message = "No se encontró la Actividad.";
                VP_Resultado.result_data = null;
            }

            return VP_Resultado;
        }

        public async Task<Resultado> CancelActivity(int id) {
            Resultado VP_Resultado = new();

            Activity VP_Actividad = await dc.Activities.FirstOrDefaultAsync(p => p.id == id);

            if (VP_Actividad != null) {
                ActivityDTO VP_Actividad_DTO = mapper.Map<ActivityDTO>(VP_Actividad);

                if (VP_Actividad.status == "Active") {
                    VP_Actividad.status = "Inactive";

                    dc.Entry(VP_Actividad).State = EntityState.Modified;
                    await dc.SaveChangesAsync();

                    VP_Actividad_DTO = Completar_ActividadDTO(VP_Actividad);

                    VP_Resultado.result_code = (int)StatusCodes.Success;
                    VP_Resultado.result_message = "OK";
                    VP_Resultado.result_data = VP_Actividad_DTO;
                } else {
                    VP_Resultado.result_code = (int)StatusCodes.NoProcede;
                    VP_Resultado.result_message = "La Actividad ya se encuentra inactiva.";
                    VP_Resultado.result_data = VP_Actividad_DTO;
                }
            } else {
                VP_Resultado.result_code = (int)StatusCodes.NotFound;
                VP_Resultado.result_message = "No se encontró la Actividad.";
                VP_Resultado.result_data = null;
            }

            return VP_Resultado;
        }

        public async Task<Resultado> UpdateActivity(int id, ActivityDTO ActivityDTO) {
            Activity VP_Actividad = await dc.Activities.FirstOrDefaultAsync(p => p.id == id);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.NotFound,
                result_message = "Not Found",
                result_data = null
            };

            if (VP_Actividad != null) {
                mapper.Map(ActivityDTO, VP_Actividad);
                VP_Actividad.updated_at = DateTime.Now;

                dc.Entry(VP_Actividad).State = EntityState.Modified;
                await dc.SaveChangesAsync();

                ActivityDTO.updated_at = VP_Actividad.updated_at;

                VP_Resultado.result_code = (int)StatusCodes.Success;
                VP_Resultado.result_message = "OK";
                VP_Resultado.result_data = ActivityDTO;
            }

            return VP_Resultado;
        }

        public async Task<Resultado> DeleteActivity(int id) {
            Activity VP_Actividad = await dc.Activities.FirstOrDefaultAsync(p => p.id == id);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.NotFound,
                result_message = "Not Found"
            };

            if (VP_Actividad != null) {
                dc.Entry(VP_Actividad).State = EntityState.Deleted;
                await dc.SaveChangesAsync();

                VP_Resultado.result_code = (int)StatusCodes.Success;
                VP_Resultado.result_message = "OK";
            }

            return VP_Resultado;
        }

        private ActivityDTO Completar_ActividadDTO(Activity PP_Actividad) {
            ActivityDTO VP_Actividad_DTO = mapper.Map<ActivityDTO>(PP_Actividad);

            string VP_Condicion = "";

            switch (PP_Actividad.status) {
                case "Active":
                    if (PP_Actividad.schedule >= DateTime.Now)
                        VP_Condicion = "Pendiente a Realizar";

                    if (PP_Actividad.schedule < DateTime.Now)
                        VP_Condicion = "Atrasada";
                    break;
                case "Done":
                    VP_Condicion = "Finalizada";
                    break;
                case "Inactive":
                    VP_Condicion = "Cancelada";
                    break;
            }

            Property VP_Propiedad = dc.Properties.Where(w => w.id == PP_Actividad.property_id).FirstOrDefault();
            PropertyDTO_Short VP_Propiedad_DTO = mapper.Map<PropertyDTO_Short>(VP_Propiedad);

            Survey VP_Encuesta = dc.Surveys.Where(w => w.activity_id == PP_Actividad.id).FirstOrDefault();
            int VP_Id_Encuesta = (VP_Encuesta != null) ? VP_Encuesta.id : 0;

            VP_Actividad_DTO.condition = VP_Condicion;
            VP_Actividad_DTO.property = VP_Propiedad_DTO;
            VP_Actividad_DTO.survey = VP_Id_Encuesta;

            return VP_Actividad_DTO;
        }
    }
}
