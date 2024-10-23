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
    public class ClienteRepository : IClienteRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ClienteRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<ClienteDto> GetByIdAsync(long idCliente, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_cliente
               .Where(c => c.ent_id == idEntidad
               && c.cli_id == idCliente);

            var entity = await result.FirstOrDefaultAsync();

            var dto = this._mapper.Map<ClienteDto>(entity);

            return dto;
        }

        public async Task<ClienteDto> GetByCodigoAsync(string codigo, long idEntidad)
        {
            ClienteDto dto = null;
            var context = _unitOfWork.GetContext();

            var result = context.tb_cliente
               .Include(c => c.ent)
               .Where(c => c.ent_id == idEntidad
               && c.cli_codigo == codigo);

            var entity = await result.FirstOrDefaultAsync();

            dto = this._mapper.Map<ClienteDto>(entity);

            if (dto != null)
            {
                var resultDir = context.tb_cliente_direccion
                 .Include(c => c.dir)
                 .ThenInclude(r=> r.reg)
                 .Include(c => c.dir)
                 .ThenInclude(r => r.dtr)
                 .Include(c => c.dir)
                 .ThenInclude(r => r.prv)
                 .Include(c => c.dir)
                 .ThenInclude(r => r.tip_id_agenciaNavigation)
                 .Where(c => c.cli_id == dto.Id
                 && c.cld_es_activo);

                var listDir = await resultDir.ToListAsync();

                dto.ListClienteDireccion = this._mapper.Map<List<ClienteDireccionDto>>(listDir);

                return dto;
            }
            return null;
        }

        public async Task<ClienteDto?> InsertAsync(ClienteDto clienteDto)
        {
            var entity = this._mapper.Map<tb_cliente>(clienteDto);

            var documentoRepository = _unitOfWork.GetRepository<tb_cliente>();
            var newEntity = await documentoRepository.InsertAsync(entity);
            _unitOfWork.SaveChanges();

            clienteDto.Id = newEntity.cli_id;

            return clienteDto;
        }

        public async Task<ClienteDto?> UpdateAsync(ClienteDto clienteDto)
        {
            var entity = this._mapper.Map<tb_cliente>(clienteDto);

            var documentoRepository = _unitOfWork.GetRepository<tb_cliente>();
            await documentoRepository.UpdateAsync(entity);
            _unitOfWork.SaveChanges();

            return clienteDto;
        }


    }
}
