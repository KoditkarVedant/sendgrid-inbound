using System;
using System.IO;
using System.Net.Http;
using Xunit;

namespace Inbound.Tests
{
    public class InboundTests
    {
        [Fact]
        public void TestWithDefaultPayload()
        {
            var data = File.ReadAllTextAsync("sample_data/default_data.txt").Result;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "SendGrid - Test");

            var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "multipart/form-data; boundary=xYzZY");
            client.PostAsync("/inbound", content).Wait();
        }
    }
}
