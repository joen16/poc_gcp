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
    public class DistritoProfile: Profile
    {
        public DistritoProfile()
        {
            CreateMap<tb_distrito, DistritoDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.dtr_id)
                )
                .ForMember(
                    dest => dest.Nombre,
                    opt => opt.MapFrom(src => src.dtr_nombre)
                )

                ;

           // CreateMap<List<tb_distrito>, List<DistritoDto>>();
        }
    }
}
