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
    public class DireccionProfile: Profile
    {
        public DireccionProfile()
        {
            CreateMap<tb_direccion, DireccionDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.dir_id)
                )
                .ForMember(
                    dest => dest.Provincia,
                    opt => opt.MapFrom(src => src.prv)
                )
                .ForMember(
                    dest => dest.Agencia,
                    opt => opt.MapFrom(src => src.tip_id_agenciaNavigation)
                )
                .ForMember(
                    dest => dest.Distrito,
                    opt => opt.MapFrom(src => src.dtr)
                )
                .ForMember(
                    dest => dest.Region,
                    opt => opt.MapFrom(src => src.reg)
                )
                ;

            CreateMap<DireccionDto, tb_direccion>()
               .ForMember(
                   dest => dest.dir_id ,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.prv_id ,
                   opt => opt.MapFrom(src => src.Provincia.Id)
               )
               .ForMember(
                   dest => dest.tip_id_agencia ,
                   opt => opt.MapFrom(src => src.Agencia.Id)
               )
               .ForMember(
                   dest => dest.dtr_id ,
                   opt => opt.MapFrom(src => src.Distrito.Id)
               )
               .ForMember(
                   dest => dest.reg_id ,
                   opt => opt.MapFrom(src => src.Region.Id)
               )
               ;
        }
    }
}
