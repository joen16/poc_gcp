using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository
{
    public class FuncionalidadRepository : IFuncionalidadRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FuncionalidadRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        public async Task<List<FuncionalidadDto>> GetByRolAsync(long idRol)
        {
            var context = _unitOfWork.GetContext();

            var query = context.tb_funcionalidad
               .Include(x=> x.mod)
               .Include(f => f.tb_rol_funcionalidad.Where(rf => rf.rol_id == idRol));

            var result = await query.AsNoTracking().ToListAsync();

            var listGrupoProducto = this._mapper.Map<List<FuncionalidadDto>>(result);
           
            return listGrupoProducto;
        }

    }
}
