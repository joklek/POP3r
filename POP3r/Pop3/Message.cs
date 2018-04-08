using System;
using POP3r.Pop3.Interfaces;
using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3
{
    public class Message : IMessage
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public bool IsComplete { get; set; }
        public MessageInfo Info { get; }
        public int SizeInOctets { get; }

        public Message(Response response)
        {
            var indexOfSplit = response.Body.IndexOf("\r\n\r\n", StringComparison.Ordinal);
            var octetsSplitText = "octets\r\n";

            Header = response.Body.Remove(indexOfSplit);

            SizeInOctets = int.Parse(Header.Remove(Header.IndexOf(" ", StringComparison.Ordinal)));
            Header = Header.Substring(Header.IndexOf(octetsSplitText, StringComparison.Ordinal) + octetsSplitText.Length);

            Body = response.Body.Substring(indexOfSplit + "\r\n\r\n".Length);
        }

        public Message(MessageInfo info)
        {
            Info = info;
        }
    }
}