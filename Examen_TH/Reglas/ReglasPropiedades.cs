using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Examen_TH.DTOs;
using Examen_TH.Modelos;
using Examen_TH.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_TH.Modelos.Constantes;

namespace Examen_TH.Reglas {
    public class ReglasPropiedades {
        private readonly AppDBContext dc;
        private readonly IMapper mapper;

        public ReglasPropiedades(AppDBContext dc, IMapper mapper) {
            this.dc = dc;
            this.mapper = mapper;
        }

        public Resultado GetAllProperties() {
            List<Property> VP_Li_Propiedad = dc.Properties.ToList();
            List<PropertyDTO> VP_Li_Propiedad_DTO = mapper.Map<List<PropertyDTO>>(VP_Li_Propiedad);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.Success,
                result_message = "OK",
                result_data = VP_Li_Propiedad_DTO
            };

            return VP_Resultado;
        }

        public Resultado GetPropertyById(int id) {
            Property VP_Propiedad = dc.Properties.FirstOrDefault(p => p.id == id);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.NotFound,
                result_message = "Not Found",
                result_data = null
            };

            if (VP_Propiedad != null) {
                PropertyDTO VP_Propiedad_DTO = mapper.Map<PropertyDTO>(VP_Propiedad);

                VP_Resultado.result_code = (int)StatusCodes.Success;
                VP_Resultado.result_message = "OK";
                VP_Resultado.result_data = VP_Propiedad_DTO;
            }

            return VP_Resultado;
        }

        public async Task<Resultado> InsertProperty(PropertyDTO_IN propertyDTO_In) {
            Property VP_Propiedad = mapper.Map<Property>(propertyDTO_In);
            VP_Propiedad.created_at = DateTime.Now;
            VP_Propiedad.updated_at = DateTime.Now;

            dc.Add(VP_Propiedad);
            await dc.SaveChangesAsync();

            PropertyDTO VP_Propiedad_DTO = mapper.Map<PropertyDTO>(VP_Propiedad);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.Success,
                result_message = "OK",
                result_data = VP_Propiedad_DTO
            };

            return VP_Resultado;
        }

        public async Task<Resultado> UpdateProperty(int id, PropertyDTO propertyDTO) {
            Property VP_Propiedad = await dc.Properties.FirstOrDefaultAsync(p => p.id == id);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.NotFound,
                result_message = "Not Found",
                result_data = null
            };

            if (VP_Propiedad != null) {
                mapper.Map(propertyDTO, VP_Propiedad);
                VP_Propiedad.updated_at = DateTime.Now;

                if (VP_Propiedad.status == "Inactive")
                    VP_Propiedad.disabled_at = DateTime.Now;

                dc.Entry(VP_Propiedad).State = EntityState.Modified;
                await dc.SaveChangesAsync();

                propertyDTO.updated_at = VP_Propiedad.updated_at;

                VP_Resultado.result_code = (int)StatusCodes.Success;
                VP_Resultado.result_message = "OK";
                VP_Resultado.result_data = propertyDTO;
            }

            return VP_Resultado;
        }

        public async Task<Resultado> DeleteProperty(int id) {
            Property VP_Propiedad = await dc.Properties.FirstOrDefaultAsync(p => p.id == id);

            Resultado VP_Resultado = new() {
                result_code = (int)StatusCodes.NotFound,
                result_message = "Not Found"
            };

            if (VP_Propiedad != null) {
                dc.Entry(VP_Propiedad).State = EntityState.Deleted;
                await dc.SaveChangesAsync();

                VP_Resultado.result_code = (int)StatusCodes.Success;
                VP_Resultado.result_message = "OK";
            }

            return VP_Resultado;
        }
    }
}
