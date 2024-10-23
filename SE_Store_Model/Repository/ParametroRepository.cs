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
    public class ParametroRepository : IParametroRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ParametroRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<ParametroDto> GetByCodigoAsync(string codigo)
        {
            var context = _unitOfWork.GetContext();

            var query = context.tb_parametro
                .Where(p => p.par_codigo == codigo);

            var entity = await query.FirstOrDefaultAsync();

            var dto = this._mapper.Map<ParametroDto>(entity);

            return dto;
        }

        public async Task<List<ParametroDto>> GetByListAsync(List<string> listCodigo)
        {
            var context = _unitOfWork.GetContext();

            var query = context.tb_parametro
                .Where(p => listCodigo.Contains(p.par_codigo));

            var listEntity = await query.ToListAsync();

            var dto = this._mapper.Map<List<ParametroDto>>(listEntity);

            return dto;
        }
    }
}
