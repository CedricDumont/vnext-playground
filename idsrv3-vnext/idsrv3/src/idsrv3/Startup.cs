using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Thinktecture.IdentityServer.Core.Configuration;
using idsrv3.Config;
using Microsoft.AspNet.Http.Security;

namespace idsrv3
{

    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Map("/core", core =>
            {
                var factory = InMemoryFactory.Create(
                                        users: Users.Get(),
                                        clients: Clients.Get(),
                                        scopes: Scopes.Get());

                var idsrvOptions = new IdentityServerOptions
                {
                    IssuerUri = "https://idsrv3.com",
                    SiteName = "test vnext Identity server",
                    Factory = factory,
                    SigningCertificate = Certificate.Get(),
                    RequireSsl = false,

                    CorsPolicy = CorsPolicy.AllowAll,

                    AuthenticationOptions = new AuthenticationOptions
                    {
                    }
                };

                core.UseIdentityServer(idsrvOptions);
            });

            app.Map("/api", api =>
            {

                api.UseOAuthBearerAuthentication(options => {
                    options.Authority = Constants.AuthorizationUrl;
                    options.MetadataAddress = Constants.AuthorizationUrl + "/.well-known/openid-configuration";
                    options.TokenValidationParameters.ValidAudience = "https://idsrv3.com/resources"; 
                });

                api.UseMvc();

            });

        }
    }
}
