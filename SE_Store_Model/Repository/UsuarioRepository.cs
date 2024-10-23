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
    public class UsuarioRepository : IUsuarioRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UsuarioRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<UsuarioDto?> GetByIdAsync(long idUsuario)
        {
            UsuarioDto usuario = null;
            var context = _unitOfWork.GetContext();

            var result = context.tb_usuario
                .Include(x => x.ent)
                .Include(x => x.est)
                .Include(x => x.rol)
                .ThenInclude(y => y.tb_rol_funcionalidad)
                .ThenInclude(z => z.fun)
                .Where(x => x.usu_id == idUsuario);

            var listEntity = await result.FirstOrDefaultAsync();

            if (listEntity != null)
            {
                usuario = this._mapper.Map<UsuarioDto>(listEntity);
                if (listEntity.rol.tb_rol_funcionalidad != null)
                {
                    usuario.Rol.ListRolFuncionalidad = this._mapper.Map<List<RolFuncionalidadDto>>(listEntity.rol.tb_rol_funcionalidad);
                }
            }
            return usuario;
        }

        public async Task<PaginadoResponse> GetAsync(PaginadoRequest paginado, ObtenerUsuarioRequest request, long idEntidad)
        {
            var context = _unitOfWork.GetContext();

            var result = context.tb_usuario
               .Include(x => x.ent)
               .Include(x => x.est)
               .Include(x => x.rol)
               .Where(gp => gp.ent_id == idEntidad

               && (request.IdEstado == 0 || gp.est_id == request.IdEstado))
               .OrderBy(a => a.usu_id);

            var listEntity = await result
                .Skip((paginado.currentPage - 1) * paginado.regPerPage)
                .Take(paginado.regPerPage)
                .ToListAsync();
            var pages = (decimal)result.Count() / paginado.regPerPage;

            PaginadoResponse response = new PaginadoResponse();
            response.TotalPage = Convert.ToInt32(Math.Ceiling(pages));
            var listOrden = this._mapper.Map<List<UsuarioDto>>(listEntity);

            response.Data = listOrden;
            return response;
        }

        public async Task<UsuarioDto> InsertAsync(UsuarioDto usuario)
        {
            var usuarioEntity = this._mapper.Map<tb_usuario>(usuario);

            var usuarioRepository = _unitOfWork.GetRepository<tb_usuario>();

            if (usuarioEntity.rol_id > 0)
            {
                usuarioEntity.rol = null;
            }
            if (usuarioEntity.est_id > 0)
            {
                usuarioEntity.est = null;
            }
            if (usuarioEntity.ent_id > 0)
            {
                usuarioEntity.ent = null;
            }
            if (usuario.Password != null)
            {
                tb_password pwdEntity = this._mapper.Map<tb_password>(usuario.Password);
                usuarioEntity.tb_password.Add(pwdEntity);
            }

            var newUsuarioEntity = await usuarioRepository.InsertAsync(usuarioEntity);
            _unitOfWork.SaveChanges();

            usuario.Id = newUsuarioEntity.usu_id;

            return usuario;
        }

        public async Task UpdateAsync(UsuarioDto usuario)
        {
            var usuarioEntity = this._mapper.Map<tb_usuario>(usuario);

            var usuarioRepository = _unitOfWork.GetRepository<tb_usuario>();
            await usuarioRepository.UpdateAsync(usuarioEntity);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(long idUsuario)
        {
            var usuarioRepository = _unitOfWork.GetRepository<tb_usuario>();
            var usuario = await usuarioRepository.GetByIdAsync(idUsuario);
            if (usuario != null)
            {
                usuario.est_id = (int)EstadoUsuarioEnum.ELIMINADO;
                await usuarioRepository.UpdateAsync(usuario);
            }
            _unitOfWork.SaveChanges();
        }

        public async Task UpdateEstadoAsync(long idUsuario, long idEstado)
        {
            var context = _unitOfWork.GetContext();
            var usuarioRepository = _unitOfWork.GetRepository<tb_usuario>();

            var usuarioEntity = context.tb_usuario
                     .Where(p => p.usu_id == idUsuario)
                     .AsNoTracking().FirstOrDefault();

            if (usuarioEntity != null)
            {
                usuarioEntity.est_id = idEstado;
                await usuarioRepository.UpdateAsync(usuarioEntity);
                _unitOfWork.SaveChanges();
            }
        }


        public async Task<UsuarioDto> LoginAsync(LoginRequest request)
        {
            var context = _unitOfWork.GetContext();
            UsuarioDto usuario = null;

            var query = context.tb_usuario
               .Include(x => x.ent)
               .Include(x => x.est)
               .Include(x => x.rol)
               .ThenInclude(y=> y.tb_rol_funcionalidad)
               .ThenInclude(z=> z.fun)
               .Include(x => x.tb_password)
               .Where(u => u.usu_email.ToLower().Equals(request.Usuario.ToLower()))
               .Where(u => u.tb_password.Any(p => p.pwd_es_activo && p.pwd_valor.Equals(request.Clave)));
              

            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                usuario = this._mapper.Map<UsuarioDto>(result);
                if(result.rol.tb_rol_funcionalidad != null)
                {
                    usuario.Rol.ListRolFuncionalidad = this._mapper.Map<List<RolFuncionalidadDto>>(result.rol.tb_rol_funcionalidad);
                }
            }
            return usuario;
        }


    }
}
