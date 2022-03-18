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

            CreateMap<ViajeCreacionViewModel, Viaje>()
                .ForMember(x => x.Foto, options => options.Ignore())
                .ForMember(x => x.Ubicacion, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))))
                .ForMember(x => x.ViajePromociones, options => options.MapFrom(MapearViajePromociones))
                .ForMember(x => x.ViajeTipoActividades, options => options.MapFrom(MapearViajeTipoActividades));

            CreateMap<Viaje, ViajeViewModel>()
                .ForMember(x => x.Latitud, dto => dto.MapFrom(campo => campo.Ubicacion.Y))
                .ForMember(x => x.Longitud, dto => dto.MapFrom(campo => campo.Ubicacion.X))
                .ForMember(x => x.Promociones, options => options.MapFrom(MapearViajePromociones))
                .ForMember(x => x.TipoActividades, options => options.MapFrom(MapearViajeTipoActividades));
        }


        private List<ViajePromocion> MapearViajePromociones(ViajeCreacionViewModel ViajeCreacionViewModel, Viaje viaje)
        {
            var resultado = new List<ViajePromocion>();

            if (ViajeCreacionViewModel.PromocionesIds == null)
                return resultado;

            foreach (var id in ViajeCreacionViewModel.PromocionesIds)
            {
                resultado.Add(new ViajePromocion() { PromocionId = id });
            }

            return resultado;
        }

        private List<PromocionViewModel> MapearViajePromociones(Viaje viaje, ViajeViewModel viajeVM)
        {
            var resultado = new List<PromocionViewModel>();

            if (viaje.ViajePromociones != null)
            {
                foreach (var promocion in viaje.ViajePromociones)
                {
                    resultado.Add(new PromocionViewModel() { Id = promocion.PromocionId, Nombre = promocion.Promocion.Nombre });
                }
            }

            return resultado;
        }

        private List<ViajeTipoActividad> MapearViajeTipoActividades(ViajeCreacionViewModel ViajeCreacionViewModel, Viaje viaje)
        {
            var resultado = new List<ViajeTipoActividad>();

            if (ViajeCreacionViewModel.TipoActividadesIds == null)
                return resultado;

            foreach (var id in ViajeCreacionViewModel.TipoActividadesIds)
            {
                resultado.Add(new ViajeTipoActividad() { TipoActividadId = id });
            }

            return resultado;
        }
        private List<TipoActividadViewModel> MapearViajeTipoActividades(Viaje viaje, ViajeViewModel viajeVM)
        {
            var resultado = new List<TipoActividadViewModel>();

            if (viaje.ViajeTipoActividades != null)
            {
                foreach (var tipoActividad in viaje.ViajeTipoActividades)
                {
                    resultado.Add(new TipoActividadViewModel() { Id = tipoActividad.TipoActividadId, Nombre = tipoActividad.TipoActividad.Nombre });
                }
            }

            return resultado;
        }


    }
}
