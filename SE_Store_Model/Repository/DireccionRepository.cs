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
    public class DireccionRepository : IDireccionRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public DireccionRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<DireccionDto> GetByIdAsync(long idDireccion)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_direccion
               .Where(c => c.dir_id == idDireccion);

            var entity = await result.FirstOrDefaultAsync();

            var dto = this._mapper.Map<DireccionDto>(entity);

            return dto;
        }


        public async Task<DireccionDto?> InsertAsync(DireccionDto direccionDto)
        {
            var entity = this._mapper.Map<tb_direccion>(direccionDto);

            var documentoRepository = _unitOfWork.GetRepository<tb_direccion>();
            var newEntity = await documentoRepository.InsertAsync(entity);
            _unitOfWork.SaveChanges();

            direccionDto.Id = newEntity.dir_id;

            return direccionDto;
        }

        public async Task<DireccionDto?> UpdateAsync(DireccionDto direccionDto)
        {
            var entity = this._mapper.Map<tb_direccion>(direccionDto);

            var documentoRepository = _unitOfWork.GetRepository<tb_direccion>();
            await documentoRepository.UpdateAsync(entity);
            _unitOfWork.SaveChanges();

            return direccionDto;
        }

        public async Task<DireccionDto> GetByCodigoClienteAsync(string codigoCliente, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_cliente_direccion
                .Include(x=> x.dir)
                .ThenInclude(x=> x.reg)
                .Include(x => x.dir)
                .ThenInclude(x => x.prv)
                .Include(x => x.dir)
                .ThenInclude(x => x.dtr)
                .Include(x => x.dir)
                .ThenInclude(x => x.tip_id_agenciaNavigation)
               .Where(x => x.cli.cli_codigo == codigoCliente && x.cli.ent_id == idEntidad  && x.cld_es_activo)
               ;

            var entity = await result.FirstOrDefaultAsync();
            if (entity != null)
            {
                var dto = this._mapper.Map<DireccionDto>(entity.dir);

                return dto;
            }
            return null;
        }
    }
}
