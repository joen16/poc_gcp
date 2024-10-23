using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

namespace SE_Store_Model.Repository
{
    public class TipoRepository : ITipoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public TipoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<List<TipoDto>> GetByClasificacionAsync(long idClasificacion)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_tipo
                .Where(x => x.cti_id == idClasificacion);

            var listEntity = await result.ToListAsync();

            var listDto = this._mapper.Map<List<TipoDto>>(listEntity);
            return listDto;
        }

        public async Task<TipoDto> GetByIdAsync(long idTipo, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_tipo
                .Include(x=> x.cti)
                .Include(x => x.ent)
                .Where(x => x.tip_id == idTipo
                && x.ent_id == idEntidad);

            var listEntity = await result.FirstOrDefaultAsync();

            var listDto = this._mapper.Map<TipoDto>(listEntity);
            return listDto;
        }


        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerTipoRequest request, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var idClasificacion = request.IdClasificacion;


            var result = context.tb_tipo
               .Include(x => x.cti)
               .Where(gp => gp.ent_id == idEntidad
               && (idClasificacion == 0 || gp.cti_id == idClasificacion))
               .OrderBy(a => a.tip_id);

            var listEntity = await result
                .Skip((paginado.currentPage - 1) * paginado.regPerPage)
                .Take(paginado.regPerPage)
                .ToListAsync();
            var pages = (decimal)result.Count() / paginado.regPerPage;

            PaginadoResponse response = new PaginadoResponse();
            response.TotalPage = Convert.ToInt32(Math.Ceiling(pages));
            var listOrden = this._mapper.Map<List<TipoDto>>(listEntity);

            response.Data = listOrden;
            return response;
        }

        public async Task<TipoDto?> InsertAsync(TipoDto tipoDto)
        {
            var tipoEntity = this._mapper.Map<tb_tipo>(tipoDto);

            var tipoRepository = _unitOfWork.GetRepository<tb_tipo>();
            var contex = _unitOfWork.GetContext();

            if (tipoEntity.cti_id > 0)
            {
                tipoEntity.cti = null;
            }

            if (tipoEntity.ent_id > 0)
            {
                tipoEntity.ent = null;
            }

            var transacction = _unitOfWork.BeginTransaction();

            try
            {
                var newEntity = await tipoRepository.InsertAsync(tipoEntity);

                _unitOfWork.SaveChanges();
                transacction.Commit();
                tipoDto.Id = newEntity.tip_id;


                return tipoDto;
            }
            catch (Exception ex)
            {
                transacction.Rollback();
                throw;
            }
        }

        public async Task<TipoDto?> UpdateAsync(TipoDto tipoDto)
        {
            var tipoEntityNew = this._mapper.Map<tb_tipo>(tipoDto);

            var context = _unitOfWork.GetContext();
            var tipoRepository = _unitOfWork.GetRepository<tb_tipo>();


            var transacction = _unitOfWork.BeginTransaction();

            try
            {
                if(tipoEntityNew.ent_id > 0)
                {
                    tipoEntityNew.ent = null;
                }
                if(tipoEntityNew.cti_id > 0)
                {
                    tipoEntityNew.cti = null;
                }
                await tipoRepository.UpdateAsync(tipoEntityNew);
                _unitOfWork.SaveChanges();
                transacction.Commit();
            }
            catch (Exception ex)
            {
                transacction.Rollback();
                throw;
            }
            return tipoDto;
        }


        public async Task DeleteAsync(long idTipo)
        {
            var tipoRepository = _unitOfWork.GetRepository<tb_tipo>();

            var tipoEntity = await tipoRepository.GetByIdAsync(idTipo);
            if (tipoEntity != null)
            {
                tipoEntity.tip_es_activo = true;
                await tipoRepository.UpdateAsync(tipoEntity);
            }

            _unitOfWork.SaveChanges();
        }

    }
}
