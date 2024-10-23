using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Utilities;
using SE_Store_Dto;
using SE_Store_Dto.Enum;
using SE_Store_Dto.Request;
using SE_Store_Dto.Response;
using SE_Store_Model.EF;
using SE_Store_Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository
{
    public class OrdenProductoRepository : IOrdenProductoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public OrdenProductoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<OrdenProductoDto> GetByIdAsync(long idOrdenProducto)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_orden_producto               
               .Where(or => or.opr_id == idOrdenProducto);

            var entity = await result.FirstOrDefaultAsync();

            var ordenProductoDto = this._mapper.Map<OrdenProductoDto>(entity);
            
            return ordenProductoDto;
        }

    }
}
