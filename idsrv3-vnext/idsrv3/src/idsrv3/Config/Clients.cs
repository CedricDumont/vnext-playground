using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityServer.Core.Models;

namespace idsrv3.Config
{
    static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {

                 new Client
                {
                     //Resource Owner Flow Client (our web UI)
                    ClientName = "WebUI",
                    Enabled = true,

                    ClientId = "IdentityWebUI",
                    ClientSecrets = new List<ClientSecret>
                    {
                        new ClientSecret("secret".Sha256())
                    },

                    Flow = Flows.ResourceOwner,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600

                }
            };
        }
    }
}
