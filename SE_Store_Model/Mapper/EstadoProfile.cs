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
    public class EstadoProfile: Profile
    {
        public EstadoProfile()
        {
            CreateMap<tb_estado, EstadoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.est_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.est_nombre)
                )
                ;

            CreateMap<EstadoDto, tb_estado>()
                .ForMember(
                    dest => dest.est_id ,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.est_nombre ,
                    opt => opt.MapFrom(src => src.Nombre)
                )
                ;
        }
    }
}
