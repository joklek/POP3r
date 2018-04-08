using System;
using System.Text.RegularExpressions;

namespace POP3r.Pop3.ServerResponses
{
    public class MessageInfo
    {
        public int MessageId { get; }
        public int SizeInOctets { get; }

        public MessageInfo(string responseBody)
        {
            if (!Regex.IsMatch(responseBody, @"^\d* \d*"))
                throw new Exception("Incorrect response string provided: " + responseBody);

            var splitResponse = responseBody.Split(' ');
            var messageId = int.Parse(splitResponse[0]);
            var sizeInOctets = int.Parse(splitResponse[1]);

            if (messageId < 0 || sizeInOctets < 0)
                throw new Exception("Maildrop info must be greater than zero. MessageId: " + messageId + ", Size of octets: " + sizeInOctets);
            MessageId = messageId;
            SizeInOctets = sizeInOctets;
        }

        public MessageInfo(int messageId, int sizeInOctets)
        {
            if (messageId < 0 || sizeInOctets < 0)
                throw new Exception("Maildrop info must be greater than zero. MessageId: " + messageId + ", Size of octets: " + sizeInOctets);
            MessageId = messageId;
            SizeInOctets = sizeInOctets;
        }
    }
}