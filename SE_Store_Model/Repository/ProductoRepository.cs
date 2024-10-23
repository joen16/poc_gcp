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
    public class ProductoRepository : IProductoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<ProductoDto> GetByIdAsync(long idProducto)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_producto
                .Include(x=> x.est)
                .Where(x => x.pro_id == idProducto);

            var entity = await result.FirstOrDefaultAsync();

            var dto = this._mapper.Map<ProductoDto>(entity);
            return dto;
        }

        public async Task<ProductoDto?> InsertAsync(ProductoDto producto)
        {
            var productoEntity = this._mapper.Map<tb_producto>(producto);

            var productoRepository = _unitOfWork.GetRepository<tb_producto>();
            var newEntity = await productoRepository.InsertAsync(productoEntity);
            _unitOfWork.SaveChanges();

            producto.Id = newEntity.pro_id;

            return producto;
        }
    }
}
