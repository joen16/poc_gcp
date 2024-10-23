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
    public class ProductoDocumentoRepository : IProductoDocumentoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductoDocumentoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task UpdateAsync(ProductoDocumentoDto documento)
        {
            var entity = this._mapper.Map<tb_producto_documento>(documento);

            var productoDocumentoRepository = _unitOfWork.GetRepository<tb_producto_documento>();
            await productoDocumentoRepository.UpdateAsync(entity);
            _unitOfWork.SaveChanges();
        }
    }
}
