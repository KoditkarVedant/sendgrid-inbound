using Inbound.Models;
using Inbound.Parsers;
using System.IO;
using Shouldly;
using Xunit;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Inbound.Tests.Parsers
{
    public class InboundWebhookParserTests
    {
        [Fact]
        public async void Default_payload()
        {
            Stream stream = new MemoryStream();
            await File.OpenRead("sample_data/default_data.txt").CopyToAsync(stream);
            stream.Position = 0;

            InboundWebhookParser parser = new InboundWebhookParser(stream);

            InboundEmail inboundEmail = parser.Parse();

            inboundEmail.ShouldNotBeNull();
            inboundEmail.Dkim.ShouldBe("{@sendgrid.com : pass}");
            inboundEmail.Html.Trim().ShouldBe("<html><body><strong>Hello SendGrid!</body></html>");
            inboundEmail.Text.Trim().ShouldBe("Hello SendGrid!");
            inboundEmail.SenderIp.ShouldBe("0.0.0.0");
            inboundEmail.SpamReport.ShouldBeNull();
            inboundEmail.Subject.ShouldBe("Testing non-raw");
            inboundEmail.SpamScore.ShouldBeNull();
            inboundEmail.Spf.ShouldBe("pass");
            inboundEmail.Charsets.Except(new KeyValuePair<string, Encoding>[] {
                new KeyValuePair<string, Encoding>("to", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("html", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("subject", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("from", Encoding.UTF8),
                new KeyValuePair<string, Encoding>("text", Encoding.UTF8)
            }).Count().ShouldBe(0);
            inboundEmail.Attachments.Count().ShouldBe(0);
            inboundEmail.Headers.Except(new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("MIME-Version","1.0"),
                new KeyValuePair<string, string>("Received","by 0.0.0.0 with HTTP; Wed, 10 Aug 2016 18:10:13 -0700 (PDT)"),
                new KeyValuePair<string, string>("From","Example User <test@example.com>"),
                new KeyValuePair<string, string>("Date","Wed, 10 Aug 2016 18:10:13 -0700"),
                new KeyValuePair<string, string>("Subject","Inbound Parse Test Data"),
                new KeyValuePair<string, string>("To","inbound@inbound.example.com"),
                new KeyValuePair<string, string>("Content-Type","multipart/alternative; boundary=001a113df448cad2d00539c16e89")
            }).Count().ShouldBe(0);

            inboundEmail.To[0].Email.ShouldBe("inbound@inbound.example.com");
            inboundEmail.To[0].Name.ShouldBe(string.Empty);

            inboundEmail.From.Email.ShouldBe("test@example.com");
            inboundEmail.From.Name.ShouldBe("Example User");

            inboundEmail.Cc.Count().ShouldBe(0);
            inboundEmail.Envelope.From.ShouldBe("test@example.com");
            inboundEmail.Envelope.To.Contains("inbound@inbound.example.com");
        }
    }
}
