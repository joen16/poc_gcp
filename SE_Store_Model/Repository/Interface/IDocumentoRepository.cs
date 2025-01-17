﻿using SE_Store_Dto;
using SE_Store_Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository.Interface
{
    public interface IDocumentoRepository
    {
        Task<DocumentoDto?> InsertAsync(DocumentoDto documento);
        Task<DocumentoDto?> UpdateAsync(DocumentoDto documento);
    }
}
