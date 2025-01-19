using System.Net;

namespace FreeMarket.Domain.Classes
{
    public class ServiceResponse<T> where T : class?
    {
        public T? Data { get; }
        public HttpStatusCode Status { get; }
        public string? Error { get; }

        internal ServiceResponse(T? data=null, HttpStatusCode status= HttpStatusCode.OK, string? error=null)
        {
            Data = data;
            Error = error;
            Status = status;
        }

        public static ServiceResponse<T> Send(T? data,HttpStatusCode? status=null) 
        {
            return new ServiceResponse<T>(data, status?? HttpStatusCode.OK);
        }

        public static ServiceResponse<T> SendError(string? error=null, HttpStatusCode? status = null)
        {
            return new ServiceResponse<T>(null, status?? HttpStatusCode.InternalServerError,error ?? "Error desconocido.");
        }
    }
}
