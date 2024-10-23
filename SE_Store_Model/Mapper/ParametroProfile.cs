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
    public class ParametroProfile: Profile
    {
        public ParametroProfile()
        {
            CreateMap<tb_parametro, ParametroDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.par_id)
                )
                .ForMember(
                    dest => dest.Codigo,
                    opt => opt.MapFrom(src => src.par_codigo)
                )
                .ForMember(
                    dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.par_descripcion)
                )
                .ForMember(
                    dest => dest.Valor,
                    opt => opt.MapFrom(src => src.par_valor)
                )                
                ;
        }
    }
}
