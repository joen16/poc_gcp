using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SE_Store_Dto;
using SE_Store_Model.EF;
using SE_Store_Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository
{
    public class EstadoRepository : IEstadoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EstadoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<EstadoDto>> GetByClasificacionAsync(long idClasificacion)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_estado
                .Where(x => x.ces_id == idClasificacion);

            var listEntity = await result.ToListAsync();

            var listDto = this._mapper.Map<List<EstadoDto>>(listEntity);
            return listDto;
        }

       
    }
}
