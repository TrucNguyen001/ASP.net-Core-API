using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MISA.AMISDemo.Core.ErrorsServe;
using MISA.AMISDemo.Core.ValidateException;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MISA.AMISDemo.Core.MISAServiceResult;
using System.Net;
using MISA.AMISDemo.Core.Resource;

namespace MISA.AMISDemo.Core.ValidateException
{
    public class HandleExceptionMiddleware
    {
        #region Field
        private RequestDelegate _next;
        #endregion

        #region Contructor
        public HandleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Methods
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (MISAValidateException ex)
            {
                var serviceResult = new MISAServiceResultError();
                serviceResult.Errors.Add(ex.Message);
                serviceResult.StatusCode = System.Net.HttpStatusCode.BadRequest;
                serviceResult.Data = ex.Data;
                var res = JsonConvert.SerializeObject(serviceResult);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(res);
            }
            catch (MISAValidateExceptionAuth ex)
            {
                var serviceResult = new MISAServiceResultAuth();
                string[] parts = ex.Message.Split(',');

                string userMsg = "";
                string devMsg = "";

                foreach (var part in parts)
                {
                    if (part.Contains("UserMsg"))
                    {
                        userMsg = part.Substring(part.IndexOf(':') + 1).Trim();
                    }
                    else if (part.Contains("DevMsg"))
                    {
                        devMsg = part.Substring(part.IndexOf(':') + 1).Trim();
                    }
                }
                serviceResult.UserMsg = userMsg;
                serviceResult.DevMsg = devMsg;
                serviceResult.StatusCode = HttpStatusCode.Unauthorized;
                var res = JsonConvert.SerializeObject(serviceResult);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(res);
            }
            catch (Exception ex)
            {
                var serviceResult = new MISAServiceResultError();
                serviceResult.Errors.Add(ex.Message);
                serviceResult.StatusCode = System.Net.HttpStatusCode.BadRequest;
                serviceResult.Data = ex.Data;
                var res = JsonConvert.SerializeObject(serviceResult);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(res);

            }
        }
        #endregion
    }
}
