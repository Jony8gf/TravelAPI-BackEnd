using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI_BackEnd.Entidades;
using TravelAPI_BackEnd.ViewModels;

namespace TravelAPI_BackEnd.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            
            CreateMap<IdentityUser, UsuarioViewModel>();

            CreateMap<TipoActividad, TipoActividadViewModel>().ReverseMap();
            CreateMap<TipoActividadCreacionViewModel, TipoActividad>();

            CreateMap<Promocion, PromocionViewModel>().ReverseMap();
            CreateMap<PromocionCreacionViewModel, Promocion>();

        }
    }
}
