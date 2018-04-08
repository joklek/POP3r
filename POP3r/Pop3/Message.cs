using System;
using POP3r.Pop3.Interfaces;
using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3
{
    public class Message : IMessage
    {
        public string Header { get; private set; }
        public string Body { get; private set; }
        public bool IsComplete { get; private set; }
        public MessageInfo Info { get; private set; }
        public int SizeInOctets { get; private set; }

        public string Sender { get; private set; }
        public string Recipient { get; private set; }
        public string Subject { get; private set; }
        
        public string DateSent { get; private set; }

        public Message(Response response)
        {
            var indexOfSplit = response.Body.IndexOf("\r\n\r\n", StringComparison.Ordinal);
            const string octetsSplitText = "octets\r\n";

            Header = response.Body.Remove(indexOfSplit);

            if (Header.Contains(octetsSplitText))
            {
                SizeInOctets = int.Parse(Header.Remove(Header.IndexOf(" ", StringComparison.Ordinal)));
                Header = Header.Substring(Header.IndexOf(octetsSplitText, StringComparison.Ordinal) + octetsSplitText.Length);
            }

            ParseHeader(Header);

            Body = response.Body.Substring(indexOfSplit + "\r\n\r\n".Length);
        }

        private void ParseHeader(string emailHeader)
        {
            Sender = HeaderParser.GetSender(emailHeader);
            Recipient = HeaderParser.GetRecipient(emailHeader);
            Subject = HeaderParser.GetSubject(emailHeader);
            DateSent = HeaderParser.GetDate(emailHeader);
        }
    }
}