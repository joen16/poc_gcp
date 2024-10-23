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
    public class ClasificacionTipoRepository : IClasificacionTipoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ClasificacionTipoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<ClasificacionTipoDto>> GetAllAsync()
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_clasificacion_tipo;

            var entity = await result.ToListAsync();

            var dto = this._mapper.Map<List<ClasificacionTipoDto>>(entity);

            return dto;
        }


    }
}
