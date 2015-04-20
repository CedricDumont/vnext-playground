using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Multi.Web.Api
{
    public class MultiMiddleware
    {
        private readonly RequestDelegate next;

        public MultiMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IClientProvider provider)
        {
            HttpClient calendarClient = null;
            HttpClient CalcClient = null;

            try
            {

                //
                //get the respective client
                //
                calendarClient = provider.GetClientFor("calendar");
                CalcClient = provider.GetClientFor("calc");

                //
                //call the calendar api
                //
                var calendarResponse = "";
                if (context.Request.Path.Value == "/today")
                {
                    calendarResponse = await calendarClient.GetStringAsync("http://www.calendarApi.io/today");
                }
                else if (context.Request.Path.Value == "/yesterday")
                {
                    calendarResponse = await calendarClient.GetStringAsync("http://www.calendarApi.io/yesterday");
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    //does not process further
                    return;
                }
                //
                //call another api
                //
                var calcResponse = await CalcClient.GetStringAsync("http://www.calcApi.io/count");

                //
                // write the final response
                //
                await context.Response.WriteAsync(calendarResponse + " count is " + calcResponse);

               // await next(context);
            }
            finally
            {
                if (calendarClient != null)
                {
                    calendarClient.Dispose();
                }
                if (CalcClient != null)
                {
                    CalcClient.Dispose();
                }
            }

        }
    }

    public static class MultiMiddlewareExtensions
    {
        public static IApplicationBuilder UseMulti(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MultiMiddleware>();
        }
    }
}