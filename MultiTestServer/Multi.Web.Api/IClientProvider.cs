using System.Net.Http;

namespace Multi.Web.Api
{
    public interface IClientProvider
    {
        HttpClient GetClientFor(string providerName);
    }
}