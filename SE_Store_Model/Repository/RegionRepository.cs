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
    public class RegionRepository : IRegionRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public RegionRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<RegionDto>> GetAllAsync()
        {
            var regionRepository = _unitOfWork.GetRepository<tb_region>();
            var listEntity = await regionRepository.GetAllAsync();

            var listDto = this._mapper.Map<List<RegionDto>>(listEntity);

            return listDto;
        }

        public async Task<List<RegionDto>> GetAllWithRelationsAsync()
        {
            var context = _unitOfWork.GetContext();
           
            var result = context.tb_region
                .Include(x => x.tb_provincia)
                .ThenInclude(y => y.tb_distrito);

            var listEntity = await result.ToListAsync();

            var listDto = this._mapper.Map<List<RegionDto>>(listEntity);

            return listDto;
        }
    }
}
