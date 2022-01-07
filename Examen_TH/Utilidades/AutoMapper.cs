using AutoMapper;
using Examen_TH.DTOs;
using Examen_TH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_TH.Utilidades {
    public class AutoMapper : Profile {
        public AutoMapper() {
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<PropertyDTO_Short, Property>().ReverseMap();
            CreateMap<PropertyDTO_IN, Property>();

            CreateMap<Activity, ActivityDTO>().ReverseMap();
            CreateMap<ActivityDTO_IN, Activity>();

            CreateMap<Survey, SurveyDTO>().ReverseMap();
        }
    }
}
