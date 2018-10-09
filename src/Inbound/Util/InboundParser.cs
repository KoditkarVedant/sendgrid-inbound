using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Inbound.Util
{
    public class InboundParser
    {
        private static string[] keys = { "from", "attachments", "headers", "text", "envelope", "to", "html", "sender_ip",
            "attachment-info", "subject", "dkim", "SPF", "charsets", "content-ids", "spam_report", "spam_score", "email" };

        public InboundParser(HttpRequest request)
        {
            Payload = request.Form;
        }

        public IFormCollection Payload { get; }

        /// <summary>
        /// Return a dictionary of key/values in the payload received from the webhook
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> KeyValues() =>
            keys.Aggregate(new Dictionary<string, string>(), (data, key) =>
            {
                if (Payload.ContainsKey(key))
                {
                    data.Add(key, Payload[key]);
                }
                return data;
            });
    }
}