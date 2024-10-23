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
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public TipoDocumentoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<TipoDocumentoDto> GetByIdAsync(long idTipo)
        {
            var repository = _unitOfWork.GetRepository<tb_tipo_documento>();

            var entity = await repository.GetByIdAsync(idTipo);

              var dto = this._mapper.Map<TipoDocumentoDto>(entity);
            return dto;
        }

       
    }
}
