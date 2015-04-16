using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;

namespace Multi.Web.Api.Test.FakeApi
{
    public class FakeExternalApi
    {
        private readonly RequestDelegate next;

        public FakeExternalApi(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Mocking the calcapi
            if (context.Request.Host.Value.Equals("www.calcapi.io"))
            {
                if (context.Request.Path.Value == "/count")
                {
                    await context.Response.WriteAsync("1");
                }              
            }
            //Mocking the calendarapi
            else if (context.Request.Host.Value.Equals("www.calendarapi.io"))
            {
                if (context.Request.Path.Value == "/today")
                {
                    await context.Response.WriteAsync("2015-04-15");
                }
                else if (context.Request.Path.Value == "/yesterday")
                {
                    await context.Response.WriteAsync("2015-04-14");
                }
                else if (context.Request.Path.Value == "/tomorow")
                {
                    await context.Response.WriteAsync("2015-04-16");
                }
            }
            else
            {
                throw new Exception("undefined host : " + context.Request.Host.Value);
            }

            await next(context);
        }
    }

    public static class FakeExternalApiExtensions
    {
        public static IApplicationBuilder UseFakeExternalApi(this IApplicationBuilder app)
        {
            return app.UseMiddleware<FakeExternalApi>();
        }
    }

}