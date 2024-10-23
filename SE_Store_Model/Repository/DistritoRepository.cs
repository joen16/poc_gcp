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
    public class DistritoRepository : IDistritoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public DistritoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<DistritoDto>> GetByProvincia(int idProvincia)
        {
            var provinciaRepository = _unitOfWork.GetRepository<tb_distrito>();
            var listEntity = await provinciaRepository.GetAsync(x => x.dtr_id == idProvincia);

            var listDto = this._mapper.Map<List<DistritoDto>>(listEntity);

            return listDto;
        }
    }
}
