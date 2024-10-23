using SE_Store_Helper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SE_Store_Integration
{
    public abstract class BaseInt
    {
        //public TipoServicioIntegracionEnum? TipoServicio = null;
        //public long? IdDenuncio;
        //public long? IdInvolucrado { get; set; }


     /*   public BaseInt(TipoServicioIntegracionEnum tipoServicio, long idDenuncio)
        {
            this.TipoServicio = tipoServicio;
            this.IdDenuncio = idDenuncio;
        }*/

        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected async Task<TResp> PostAsync<TReq, TResp>(string url, TReq request, Dictionary<string, Object> customHeaders = null)
        {
            string strResponse = string.Empty;
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent payload = null;
                    /*if (request is string)
                    {
                        payload = new StringContent(request.ToString(), Encoding.UTF8, "application/json");
                    }*/

                    payload = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                    //var payload = "";
                    
                    
                    if (customHeaders != null && customHeaders.Any())
                    {
                        foreach (KeyValuePair<string, object> header in customHeaders)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                        }
                    }


                    using (HttpResponseMessage response = await client.PostAsync(url, payload))
                    {
                        strResponse = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            strResponse = await response.Content.ReadAsStringAsync();
                            return JsonSerializer.Deserialize<TResp>(strResponse);
                        }
                        else
                        {
                            throw new IntegrationException(strResponse);
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                /*log.Error("================ DETAIL =============");
                log.Error($"URL:{url}");
                log.Error($"Request: {Util.ConvertToJson(request)}");
                log.Error($"Response: {Util.ConvertToJson(strResponse)}");
                log.Error($"Reason:{ex.Message}");*/
                throw;
            }
        }
        /*
        protected async Task<TResp> PostAsync<TReq, TResp>(string url, TReq request)
        {
            return await PostAsync<TReq, TResp>(url, request, null, null, null);
        }

        protected async Task<string> PostAsyncStr<TReq>(string url, TReq request)
        {
            return await this.PostAsyncStr(url, request, null, null);
        }*/

        /*
        protected async Task<string> PostAsyncStr<TReq>(string url, TReq request, long? rut, string dv)
        {
            string strResponse = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    StringContent payload = null;
                    if (request != null)
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        payload = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    }

                    var paylaod = JsonConvert.SerializeObject(request);

                    using (HttpResponseMessage response = await client.PostAsync(url, payload))
                    {
                        strResponse = await response.Content.ReadAsStringAsync();
                        await RegistrarAuditoriaAsync(response.IsSuccessStatusCode, url, request, strResponse, rut, dv);

                        if (response.IsSuccessStatusCode)
                        {
                            strResponse = await response.Content.ReadAsStringAsync();
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            strResponse = string.Empty;
                        }
                        else
                        {
                            throw new IntegrationException(strResponse);
                        }
                    }

                    return strResponse;
                }
            }
            catch (Exception ex)
            {
                log.Error("================ DETAIL =============");
                log.Error($"URL:{url}");
                log.Error($"Request: {Util.ConvertToJson(request)}");
                log.Error($"Response: {Util.ConvertToJson(strResponse)}");
                log.Error($"Reason:{ex.Message}");
                throw;
            }
        }

        protected async Task<CMResponse> EnviarDocumentoAsync(string url, CMRequest request)
        {
            string strResponse = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var documentoByteArray = Convert.FromBase64String(request.doc);
                    var req = new HttpRequestMessage(HttpMethod.Post, url);

                    request.doc = null;

                    var payload = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var json = JsonConvert.SerializeObject(request);
                    var content = new MultipartFormDataContent();
                    content.Add(payload, "jsondata");
                    content.Add(new ByteArrayContent(documentoByteArray, 0, documentoByteArray.Length), "doc", request.nombreArchivo.Normalizar());

                    req.Content = content;

                    using (HttpResponseMessage response = await client.SendAsync(req))
                    {
                        strResponse = await response.Content.ReadAsStringAsync();
                        await RegistrarAuditoriaAsync(response.IsSuccessStatusCode, url, request, strResponse);

                        if (response.IsSuccessStatusCode)
                        {
                            strResponse = await response.Content.ReadAsStringAsync();

                        }
                        else
                        {
                            throw new IntegrationException(strResponse);
                        }
                    }

                    return JsonConvert.DeserializeObject<CMResponse>(strResponse);
                }
            }
            catch (Exception ex)
            {
                log.Error("================ DETAIL =============");
                log.Error($"URL:{url}");
                log.Error($"Request: {Util.ConvertToJson(request)}");
                log.Error($"Response: {Util.ConvertToJson(strResponse)}");
                log.Error($"Reason:{ex.Message}");
                throw;
            }
        }


        protected async Task<TResp> GetAsyncStr<TResp>(string url, string user, string password)
        {
            string strResponse = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var clientId = user;
                    var clientSecret = password;
                    var authenticationString = $"{clientId}:{clientSecret}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        strResponse = await response.Content.ReadAsStringAsync();
                        await RegistrarAuditoriaAsync(response.IsSuccessStatusCode, url, null, strResponse);
                        if (response.IsSuccessStatusCode)
                        {
                            return JsonConvert.DeserializeObject<TResp>(strResponse);
                        }
                        else
                        {
                            throw new IntegrationException(strResponse);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("================ DETAIL =============");
                log.Error($"URL:{url}");
                //log.Error($"Request: {Util.ConvertToJson(request)}");
                log.Error($"Response: {Util.ConvertToJson(strResponse)}");
                log.Error($"Reason:{ex.Message}");
                throw;
            }
        }

        protected async Task<TResp> GetAsyncStr<TResp>(string url, string accessToken)
        {
            string strResponse = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        strResponse = await response.Content.ReadAsStringAsync();
                        await RegistrarAuditoriaAsync(response.IsSuccessStatusCode, url, null, strResponse);
                        if (response.IsSuccessStatusCode)
                        {
                            return (TResp)(object)true;
                        }
                        else
                        {
                            throw new IntegrationException(strResponse);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("================ DETAIL =============");
                log.Error($"URL:{url}");
                //log.Error($"Request: {Util.ConvertToJson(request)}");
                log.Error($"Response: {Util.ConvertToJson(strResponse)}");
                log.Error($"Reason:{ex.Message}");
                throw;
            }
        }

        protected async Task<TResp> PostAsyncStr<TReq, TResp>(string url, TReq request, string user, string password, long? rut, string dv)
        {
            string strResponse = string.Empty;
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var clientId = user;
                    var clientSecret = password;
                    var authenticationString = $"{clientId}:{clientSecret}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

                    var payload = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var json = JsonConvert.SerializeObject(request);
                    using (HttpResponseMessage response = await client.PostAsync(url, payload))
                    {
                        strResponse = await response.Content.ReadAsStringAsync();

                        await RegistrarAuditoriaAsync(response.IsSuccessStatusCode, url, request, strResponse, rut, dv);
                        if (response.IsSuccessStatusCode)
                        {
                            return JsonConvert.DeserializeObject<TResp>(strResponse);
                        }
                        else
                        {
                            throw new IntegrationException(strResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("================ DETAIL =============");
                log.Error($"URL:{url}");
                log.Error($"Request: {Util.ConvertToJson(request)}");
                log.Error($"Response: {Util.ConvertToJson(strResponse)}");
                log.Error($"Reason:{ex.Message}");
                throw;
            }
        }

        protected async Task<TResp> PostAsyncStr<TReq, TResp>(string url, TReq request, string user, string password)
        {
            return await this.PostAsyncStr<TReq, TResp>(url, request, user, password, null, null);
        }

        /// <summary>
        /// Permite guardar datos de la integracion en la auditoria de integracion
        /// </summary>
        protected async Task RegistrarAuditoriaAsync(bool esExito, string url, string request, string response, long? rut, string dv)
        {

            if (this.IdDenuncio.HasValue && TipoServicio.HasValue)
            {
                string Nok = $"\"NOK\"";
                AuditoriaIntegracionDal auditoriaIntegracionDal = new AuditoriaIntegracionDal();
                AuditoriaIntegracionDto dto = new AuditoriaIntegracionDto();
                dto.Request = request;
                dto.Response = response;
                dto.EsExitoso = esExito && !response.Contains(Nok);
                dto.Url = url;
                dto.FechaCreacion = DateTime.Now;
                dto.Denuncio = new DenuncioDto() { Id = this.IdDenuncio.Value };
                dto.TipoIntegracion = new TipoDto() { Id = (int)TipoServicio };
                dto.Involucrado = (this.IdInvolucrado.HasValue && this.IdInvolucrado.Value > 0) ? new InvolucradoDto() { Id = this.IdInvolucrado.Value } : null;
                dto.NumeroIntento = await getNumeroIntento(auditoriaIntegracionDal, request);

                await auditoriaIntegracionDal.InsertAsync(dto);
            }
            else
            {
                await registrarBitacoraIntegracion(url, request, response, rut, dv);
            }
        }
        private async Task registrarBitacoraIntegracion(string url, string request, string response, long? rut, string dv)
        {
            List<TipoServicioIntegracionEnum> listServiciosConsiderados = new List<TipoServicioIntegracionEnum>()
                {
                    TipoServicioIntegracionEnum.LOGIN_CONSULTAR_DATOS_USUARIO,
                    TipoServicioIntegracionEnum.LOGIN_VALIDAR_CREDENCIALES,
                    TipoServicioIntegracionEnum.POLIZAS_CONSULTA_CORE
                };
            if (TipoServicio.HasValue && listServiciosConsiderados.Contains(TipoServicio.Value))
            {
                BitacoraIntegracionDal bitacoraIntegracionDal = new BitacoraIntegracionDal();
                BitacoraIntegracionDto dto = new BitacoraIntegracionDto();
                dto.Request = request;
                dto.Response = response;
                dto.Rut = rut.HasValue ? rut.Value : 0;
                dto.Dv = dv;
                dto.Url = url;
                dto.FechaCreacion = DateTime.Now;
                dto.Tipo = new TipoDto() { Id = (int)TipoServicio };
                await bitacoraIntegracionDal.InsertAsync(dto);
            }
        }
        private async Task<int> getNumeroIntento(AuditoriaIntegracionDal auditoriaIntegracionDal, string request)
        {
            if ((long)TipoServicio.Value != (long)TipoServicioIntegracionEnum.INSERTAR_INFO_TECERO_VEHICULO)
            {
                var intentos = await auditoriaIntegracionDal.GetIntentosRequestAsync(this.IdDenuncio.Value, (long)TipoServicio);
                if (intentos == 0)
                {
                    return 1;
                }
                else
                {
                    intentos++;
                    return intentos;
                }
            }
            else
            {
                return await getNumeroIntentoInvolucrado(auditoriaIntegracionDal, request);
            }
        }

        private async Task<int> getNumeroIntentoInvolucrado(AuditoriaIntegracionDal auditoriaIntegracionDal, string request)
        {
            var auditorias = await auditoriaIntegracionDal.ListByTipoYdenuncioAsync(this.IdDenuncio.Value, (long)TipoServicioIntegracionEnum.INSERTAR_INFO_TECERO_VEHICULO);
            if (auditorias.Any())
            {
                var checkProcesadoOk = await auditoriaIntegracionDal.GeRequestOkAsync(this.IdDenuncio.Value, (long)TipoServicioIntegracionEnum.INSERTAR_INFO_TECERO_VEHICULO, request);
                var insertadoConError = auditorias.LastOrDefault(w => w.Involucrado != null && this.IdInvolucrado.HasValue && w.Involucrado.Id == this.IdInvolucrado && w.EsExitoso.HasValue && !w.EsExitoso.Value);
                if (insertadoConError != null && !checkProcesadoOk)
                {
                    return (insertadoConError.NumeroIntento.Value + 1);
                }

                var insertado = auditorias.LastOrDefault(w => w.Involucrado != null && w.Involucrado.Id == this.IdInvolucrado);
                if (insertado == null)
                {
                    return 1;
                }
                return 1;
            }
            else
            {
                return 1;
            }
        }

        protected async Task RegistrarAuditoriaAsync<TReq>(bool esExito, string url, TReq request, string response)
        {
            await RegistrarAuditoriaAsync(esExito, url, Util.ConvertToJson(request), response, null, null);
        }

        protected async Task RegistrarAuditoriaAsync(bool esExito, string url, string request, string response)
        {
            await RegistrarAuditoriaAsync(esExito, url, Util.ConvertToJson(request), response, null, null);
        }

        protected async Task RegistrarAuditoriaAsync<TReq>(bool esExito, string url, TReq request, string response, long? rut, string dv)
        {
            await RegistrarAuditoriaAsync(esExito, url, Util.ConvertToJson(request), response, rut, dv);
        }*/
    }
}
