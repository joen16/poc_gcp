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
    public class ProvinciaRepository : IProvinciaRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProvinciaRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<ProvinciaDto>> GetByRegion(int idRegion)
        {
            var provinciaRepository = _unitOfWork.GetRepository<tb_provincia>();
            var listEntity = await provinciaRepository.GetAsync(x => x.reg_id == idRegion);

            var listDto = this._mapper.Map<List<ProvinciaDto>>(listEntity);

            return listDto;
        }
    }
}
