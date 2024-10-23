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
    public class DocumentoRepository : IDocumentoRepository
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public DocumentoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<DocumentoDto?> InsertAsync(DocumentoDto documento)
        {
            var entity = this._mapper.Map<tb_documento>(documento);

            var documentoRepository = _unitOfWork.GetRepository<tb_documento>();
            var newEntity = await documentoRepository.InsertAsync(entity);
            _unitOfWork.SaveChanges();

            documento.Id = newEntity.doc_id;

            return documento;
        }

        public async Task<DocumentoDto?> UpdateAsync(DocumentoDto documento)
        {
            var entity = this._mapper.Map<tb_documento>(documento);

            var documentoRepository = _unitOfWork.GetRepository<tb_documento>();
            await documentoRepository.UpdateAsync(entity);
            _unitOfWork.SaveChanges();

            return documento;
        }
    }
}
