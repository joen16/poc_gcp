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
    public class ClienteDireccionRepository : IClienteDireccionRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ClienteDireccionRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

       /* public async Task<ClienteDto> GetByIdAsync(long idCliente, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_cliente
               .Where(c => c.ent_id == idEntidad
               && c.cli_id == idCliente);

            var entity = await result.FirstOrDefaultAsync();

            var dto = this._mapper.Map<ClienteDto>(entity);

            return dto;
        }*/

        public async Task<ClienteDireccionDto> GetByClienteAsync(long idCliente, long idEntidad)
        {
            ClienteDireccionDto dto = null;
            var context = _unitOfWork.GetContext();

            var result = context.tb_cliente_direccion
               .Include(c => c.dir)
               .Include(c => c.cli)
               .Where(c => c.cli.ent_id == idEntidad
               && c.cli_id == idCliente
               && c.cld_es_activo);

            var entity = await result.FirstOrDefaultAsync();

            dto = this._mapper.Map<ClienteDireccionDto>(entity);
            return dto;
        }

      /*  public async Task<ClienteDto?> InsertAsync(ClienteDto clienteDto)
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
      */

    }
}
