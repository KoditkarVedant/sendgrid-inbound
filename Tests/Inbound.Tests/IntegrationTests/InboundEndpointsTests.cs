using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Inbound.Tests.IntegrationTests
{
    public class InboundEndpointsTests
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;

        public InboundEndpointsTests()
        {
            _applicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        }

        [Fact]
        public async Task Get_IndexPageReturnsSuccessAndCorrectContentType()
        {
            const string url = "/";

            var client = _applicationFactory.CreateClient();
            var response = await client.GetAsync(url);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType!.MediaType.ShouldBe("text/html");
        }

        [Fact]
        public async Task Get_InboundEndpointReturnsNotFound()
        {
            const string url = "/inbound";
            using var client = _applicationFactory.CreateClient();
            var response = await client.GetAsync(url);
            response.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task Post_InboundEndpointWithDefaultPayload()
        {
            const string url = "/inbound";
            var data = File.ReadAllTextAsync("sample_data/default_data.txt").Result;

            var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "multipart/form-data; boundary=xYzZY");

            using var client = _applicationFactory.CreateClient();
            var response = await client.PostAsync(url, content);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_InboundEndpointWithRawPayloadWithAttachments()
        {
            const string url = "/inbound";
            var data = File.ReadAllTextAsync("sample_data/raw_data_with_attachments.txt").Result;

            var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "multipart/form-data; boundary=xYzZY");

            using var client = _applicationFactory.CreateClient();
            var response = await client.PostAsync(url, content);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}