using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SE_Store_Dto;
using SE_Store_Helper;
using Microsoft.AspNetCore.Http.HttpResults;
using SE_Store_Helper.Exceptions;

namespace SE_Store_Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Error no controlado:");
            Type a = typeof(BusinessException);
            Type b = context.Exception.GetType();
            ObjectResult response = null;
            if (a.Equals(b))
            {
                response = new ObjectResult(Helper.ConvertToJson(new ResponseEntity(403, context.Exception.Message)));
                response.StatusCode = 200;
            } else
            {
                response = new ObjectResult(Helper.ConvertToJson(new ResponseEntity(500, "La solicitud no ha podido ser procesada")));
                response.StatusCode = 500;
            }

           

            context.Result = response;

        }

    }
}
