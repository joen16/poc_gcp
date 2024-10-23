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
    public class ClienteDireccionProfile: Profile
    {
        public ClienteDireccionProfile()
        {
            CreateMap<tb_cliente_direccion, ClienteDireccionDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.cli_id)
                )
                .ForMember(
                    dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.cli)
                )
                .ForMember(
                    dest => dest.Direccion,
                    opt => opt.MapFrom(src => src.dir)
                )
                .ForMember(
                    dest => dest.EsActivo,
                    opt => opt.MapFrom(src => src.cld_es_activo)
                )
                
                ;
        }
    }
}
