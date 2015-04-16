using Microsoft.AspNet.TestHost;
using Multi.Web.Api.Test.FakeApi;
using System;
using System.Net.Http;

namespace Multi.Web.Api
{
    public class TestClientProvider : IClientProvider, IDisposable
    {
        TestServer _fakeCalendarServer;
        TestServer _fakeCalcServer;

        public TestClientProvider()
        {
            _fakeCalendarServer = TestServer.Create(app =>
            {
                app.UseFakeExternalApi();
            });

            _fakeCalcServer = TestServer.Create(app =>
            {
                app.UseFakeExternalApi();
            });

        }

        public HttpClient GetClientFor(string providerName)
        {
            if (providerName == "calendar")
            {
                return _fakeCalendarServer.CreateClient();
            }
            else if (providerName == "calc")
            {
                return _fakeCalcServer.CreateClient();
            }
            else
            {
                throw new Exception("Unsupported external api");
            }
        }

        public void Dispose()
        {
            _fakeCalendarServer.Dispose();
            _fakeCalcServer.Dispose();
        }
    }
}