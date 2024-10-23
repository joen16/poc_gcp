using AutoMapper;
using SE_Store_Dto;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Mapper
{
    public class ClasificacionEstadoProfile: Profile
    {
        public ClasificacionEstadoProfile()
        {
            CreateMap<tb_clasificacion_estado, ClasificacionEstadoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.ces_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.ces_nombre)
                )
                .ForMember(
                    dest => dest.ListEstado,
                    opt => opt.MapFrom(src => src.tb_estado)
                )
                ;
        }
    }
}
