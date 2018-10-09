using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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

            // var payload = new RawPayload();
            // var content = new MultipartFormDataContent();
            // content.Add(new StringContent(payload.Headers), "headers");
            // content.Add(new StringContent(payload.dkim.ToString()), "dkim");
            // content.Add(new StringContent(payload.To), "to");
            // content.Add(new StringContent(payload.Html), "html");
            // content.Add(new StringContent(payload.From), "from");
            // content.Add(new StringContent(payload.Text), "text");
            // content.Add(new StringContent(payload.Sender_Ip), "sender_ip");
            // content.Add(new StringContent(JsonConvert.SerializeObject(payload.Envelope)), "envelope");
            // content.Add(new StringContent(JsonConvert.SerializeObject(payload.Attachments)), "attachments");
            // content.Add(new StringContent(payload.Subject), "subject");
            // content.Add(new StringContent(JsonConvert.SerializeObject(payload.charsets)), "charsets");
            // content.Add(new StringContent(payload.SPF), "SPF");

            //client.PostAsync("/inbound", content).Wait();

            // HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/inbound");
            // requestMessage.Headers.Clear();
            // requestMessage.Content = new StringContent(data);
            // requestMessage.Content.Headers.Clear();
            // requestMessage.Content.Headers.Add("Content-Type", "multipart/form-data; boundary=xYzZY");
            // requestMessage.Headers.Add("User-Agent", "SendGrid - Test");
            //client.SendAsync(requestMessage).Wait();

            var content = new StringContent(data);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "multipart/form-data; boundary=xYzZY");
            client.PostAsync("/inbound", content).Wait();

        }
    }
}
