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
    public class RolRepository : IRolRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public RolRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<RolDto>> GetAllAsync()
        {
            var rolRepository = _unitOfWork.GetRepository<tb_rol>();
            var listEntity = await rolRepository.GetAllAsync();

            var listDto = this._mapper.Map<List<RolDto>>(listEntity);

            return listDto;
        }

    }
}
