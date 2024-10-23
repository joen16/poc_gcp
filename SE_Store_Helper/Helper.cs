using SE_Store_Helper.File;
using SE_Store_Helper.Extends;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SE_Store_Helper
{
    public class Helper
    {
        /// <summary>
        /// Permite ofuscar una direccion de correo electronico ejemplo: jo*****s@mail.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string Ofuscar(string email)
        {
            string[] separada = email.Split('@');
            int inicio = 2; //Caracteres al inicio de la cadena que dejamos visibles
            int final = separada.Length - 1; //Caracteres al final de la cadena que dejamos visibles
            int longitud;
            if (separada[0].Length > inicio + final)
                longitud = separada[0].Length - final - inicio;
            else
                longitud = 1;

            separada[0] = separada[0].Remove(inicio, longitud).Insert(inicio, new string('*', longitud));
            email = String.Join("@", separada);
            return email;
        }

        /// <summary>
        /// permite validar si una lista es vacia o nula
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            if (list != null && list.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Permite validar si una fecha tiene un valor valido o no
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(DateTime fecha)
        {
            if (fecha >= DateTime.MinValue)
            {
                return false;
            }
            return true;
        }

        public static bool IsNullOrEmpty(int value)
        {
            if (value > 0)
            {
                return false;
            }
            return true;
        }


        public static bool IsNullOrEmpty(decimal value)
        {
            if (value > 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Permite obtener el mimetype de un archivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMimeType(string fileName)
        {
            var arrayOfValues = fileName.Split('.');

            if (arrayOfValues.Length > 1)
            {
                string extension = arrayOfValues[1];

                if (!extension.StartsWith("."))
                {
                    extension = "." + extension;
                }

                string mime;
                return MimeTypesMapper.Map.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
            }
            else
            {
                return null;
            }

        }

        public static T ConvertFromXml<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                var test = (T)serializer.Deserialize(reader);
                return test;
            }
        }


        public static string ConvertToXml<T>(T objeto)
        {
            using (var stringwriter = new Utf8StringWriter())
            {
                var serializer = new XmlSerializer(objeto.GetType());
                serializer.Serialize(stringwriter, objeto);
                return stringwriter.ToString();
            }

        }
        public static Y ConvertFromJson<Y>(string objeto)
        {
            return JsonSerializer.Deserialize<Y>(objeto);
        }

        public static string ConvertToJson<T>(T objeto)
        {
            return JsonSerializer.Serialize(objeto);
        }

        public static string ConvertToString<T>(T objeto)
        {
            return string.IsNullOrWhiteSpace(Convert.ToString(objeto)) ? "" : objeto.ToString();
        }

        public static DateTime? ParseDateTimeFromString(string item, string format)
        {
            try
            {
                if (!string.IsNullOrEmpty(item))
                {
                    return DateTime.ParseExact(item, format, null);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"No fue posible convertir cadena [{item}] en tipo Datetime usando el formato [{format}]. Error: {ex.Message}");
            }
        }

        public static bool? ParseBooleanFromString(string item, string trueVakue)
        {
            if (!string.IsNullOrEmpty(item))
            {
                if (item == trueVakue)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return null;
        }

        /// <summary>
        /// Calcula el dígito verificador de un rut a partir de la parte entera del rut
        /// </summary>
        /// <param name="rut"></param>
        public static string CalculaDigitoRut(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return Convert.ToString(suma);
            }
        }

        public static string GenerateRandomCode(int length = 8)
        {
            string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static string FormatearRut(string rut)
        {
            string rutFormateado = string.Empty;

            if (rut.Length == 0)
            {
                rutFormateado = "";
            }
            else
            {
                string rutTemporal;
                string dv;
                Int64 rutNumerico;

                rut = rut.Replace("-", "").Replace(".", "");

                if (rut.Length == 1)
                {
                    rutFormateado = rut;
                }
                else
                {
                    rutTemporal = rut.Substring(0, rut.Length - 1);
                    dv = rut.Substring(rut.Length - 1, 1);

                    //aqui convierto a un numero el RUT si ocurre un error lo deja en CERO
                    if (!Int64.TryParse(rutTemporal, out rutNumerico))
                    {
                        rutNumerico = 0;
                    }

                    //este comando es el que formatea con los separadores de miles
                    rutFormateado = rutNumerico.ToString("N0");

                    if (rutFormateado.Equals("0"))
                    {
                        rutFormateado = string.Empty;
                    }
                    else
                    {
                        //si no hubo problemas con el formateo agrego el DV a la salida
                        rutFormateado += "-" + dv;

                        //y hago este replace por si el servidor tuviese configuracion anglosajona y reemplazo las comas por puntos
                        rutFormateado = rutFormateado.Replace(",", ".");
                    }
                }
            }

            return rutFormateado;
        }

        public static string Replace(string cont, string aReemplazar, string reemplazo, string reemplazoVacio = "Sin información")
        {
            try
            {
                if (cont.Contains(aReemplazar))
                {
                    if (string.IsNullOrEmpty(reemplazo))
                    {
                        return cont.Replace(aReemplazar, reemplazoVacio);
                    }
                    else
                    {
                        return cont.Replace(aReemplazar, reemplazo);
                    }
                }

                return cont;
            }
            catch (Exception e)
            {
                //throw new Exception("Ocurrio un error al intentar reemplazar una etiqueta de datos", e);
                return reemplazoVacio;
            }
        }

        public static string ReplaceSpecialCharsCore(string cadena)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                var normalizedString = cadena.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();

                foreach (var c in normalizedString)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c);
                    }
                }

                return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            }
            else
            {
                return string.Empty;
            }
            //return cadena;
        }

        public static string AllowAscii(string cadena)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                cadena = Regex.Replace(cadena.Normalizar(), @"[^\u0000-\u007F]+", string.Empty);
                return cadena;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string Truncate(string cadena, int limite)
        {
            string newCadena = string.Empty;
            if (!string.IsNullOrEmpty(cadena))
            {
                if (cadena.Length > limite)
                {
                    newCadena = cadena.Substring(0, limite);
                }
            }
            return newCadena;
        }

        public static string ToMoneyString(int cantidad)
        {
            NumberFormatInfo nfi = new CultureInfo("es-CL", false).NumberFormat;
            nfi.CurrencyPositivePattern = 2;
            nfi.CurrencyDecimalSeparator = ".";

            return string.Format(nfi, "{0:C0}", cantidad);
        }

        public static string GetExtension(string fileName)
        {
            var arrayOfValues = fileName.Split('.');

            if (arrayOfValues.Length > 1)
            {
                string extension = arrayOfValues[arrayOfValues.Length - 1];
                return extension;
            }
            else
            {
                return null;
            }

        }

        public static bool IsVideo(string extension)
        {
            //var extension = GetExtension(fileName);
            List<string> videoExtension = new List<string>() { "mp4", "avi" };

            return videoExtension.Contains(extension);
        }

        /// <summary>
        /// formatea un telefono al formato 22 222 2222
        /// </summary>
        /// <param name="fono">telefono a formatear de 9 digitos</param>
        /// <returns>telefono formateado, el telefono ingresado sin cambios si ocurre algun error</returns>
        public static string FormatearFono(string fono)
        {
            string fonoTrim = fono.Trim().Replace(" ", "");

            if (!string.IsNullOrEmpty(fonoTrim) && fonoTrim.Length >= 9 && int.TryParse(fonoTrim, out int fonoInt))
            {
                string retorno = fonoTrim.Substring(0, 2) + " ";
                retorno = retorno + fonoTrim.Substring(2, 3) + " ";
                retorno = retorno + fonoTrim.Substring(5, fonoTrim.Length - 5);

                return retorno;
            }

            return fono;
        }

        public static string EliminaEtiquetasExcepto(List<string> etiquetasReemplazo, string plantillaHtml, string key)
        {
            etiquetasReemplazo.ForEach(etiqueta => {
                if (etiqueta != key)
                {
                    plantillaHtml = plantillaHtml.Replace(key, "");
                }
            });

            return plantillaHtml;
        }

        /// <summary>
        /// Obtiene una etiqueta y su contenido de un string
        /// </summary>
        /// <param name="plantillaHtml">origen de datos</param>
        /// <param name="etiquetaApertura">tag apertura</param>
        /// <param name="etiquetaCierre">tag cierre</param>
        /// <returns></returns>
        public static string GetContenidoEtiquetaHtml(string plantillaHtml, string etiquetaApertura, string etiquetaCierre)
        {
            string contenido = string.Empty;
            foreach (Match match in Regex.Matches(plantillaHtml, $"{etiquetaApertura}(.*?){etiquetaCierre}"))
            {
                contenido = match.Groups[0].Value;
            }

            return contenido;
        }
    }
}
