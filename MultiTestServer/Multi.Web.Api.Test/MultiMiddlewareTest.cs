using System;
using System.Threading.Tasks;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection;
using Xunit;
using Microsoft.AspNet.Builder;
using System.Net.Http;

namespace Multi.Web.Api
{
    [Collection("testserver")]
    public class TestServerHelper : IDisposable
    {
        public TestServerHelper()
        {
            ClientProvider = new TestClientProvider();

            ApiServer = TestServer.Create((app) =>
            {
                app.UseServices(services =>
                {
                    services.AddTransient<IClientProvider>(s => ClientProvider);
                });
                app.UseMulti();
            });
        }
        public TestClientProvider ClientProvider { get; private set; }

        public TestServer ApiServer { get; private set; }

        public void Dispose()
        {
            ApiServer.Dispose();
            ClientProvider.Dispose();
        }
    }

    [Collection("testserver")]
    public class MultiMiddlewareTest
    {
       
        TestServerHelper _testServerHelper;

        public MultiMiddlewareTest()
        {
            _testServerHelper = new TestServerHelper();
           
        }

        [Fact]
        public async Task ShouldReturnToday()
        {
            using (HttpClient client = _testServerHelper.ApiServer.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/today");

                String content = await response.Content.ReadAsStringAsync();
                Assert.Equal(content, "2015-04-15 count is 1");

                return ;
            }
        }

        [Fact]
        public async Task ShouldReturnYesterday()
        {
            using (HttpClient client = _testServerHelper.ApiServer.CreateClient())
            {
                var response = await client.GetAsync("http://localhost/yesterday");

                String content = await response.Content.ReadAsStringAsync();
                Assert.Equal(content, "2015-04-14 count is 1");

                return ;
            }
        }

        public void Dispose()
        {
            _testServerHelper.Dispose();
        }
    }
}
