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
    public class ClasificacionTipoProfile: Profile
    {
        public ClasificacionTipoProfile()
        {
            CreateMap<tb_clasificacion_tipo, ClasificacionTipoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.cti_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.cti_nombre)
                )
                ;

            CreateMap<ClasificacionTipoDto, tb_clasificacion_tipo>()
                .ForMember(
                    dest => dest.cti_id ,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.cti_nombre ,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                ;
        }
    }
}
