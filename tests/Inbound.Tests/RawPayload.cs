using System.Collections.Generic;

namespace Inbound.Tests
{
    public class RawPayload
    {
        public string From = "Example User <test@example.com>";
        public object Attachments = 0;
        public string Headers = "MIME-Version: 1.0\nReceived: by 0.0.0.0 with HTTP; Wed, 10 Aug 2016 18:10:13 -0700 (PDT)\nFrom: Example User <test@example.com>\nDate: Wed, 10 Aug 2016 18:10:13 -0700\nSubject: Inbound Parse Test Data\nTo: inbound@inbound.example.com\nContent-Type: multipart/alternative; boundary=001a113df448cad2d00539c16e89\n";
        public string Text = "Hello SendGrid!\n";
        public Envelope Envelope = new Envelope();
        public string To = "inbound@inbound.example.com";
        public string Html = "<html><body><strong>Hello SendGrid!</body></html>\n";
        public string Sender_Ip = "0.0.0.0";
        public string Subject = "Testing non-raw";
        public object dkim = "{@sendgrid.com : pass}";
        public string SPF = "pass";
        public CharacterSets charsets = new CharacterSets();
    }

    public class Envelope
    {
        public IEnumerable<string> To = new List<string> { "inbound@inbound.example.com" };
        public string From = "test@example.com";
    }

    public class CharacterSets
    {
        public string to = "UTF-8";
        public string html = "UTF-8";
        public string subject = "UTF-8";
        public string from = "UTF-8";
        public string text = "UTF-8";
    }
}