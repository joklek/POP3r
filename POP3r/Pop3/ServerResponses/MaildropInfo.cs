using System;
using System.Text.RegularExpressions;

namespace POP3r.Pop3.ServerResponses
{
    public struct MaildropInfo
    {
        public int NumberOfMessages { get; }
        public int SizeInOctets { get; }

        public MaildropInfo(string responseBody)
        {
            if (!Regex.IsMatch(responseBody, @"^\d* \d*\s"))
                throw new Exception("Incorrect response string provided: " + responseBody);

            var splitResponse = responseBody.Split(' ');
            var numberOfMessages = int.Parse(splitResponse[0]);
            var sizeInOctets = int.Parse(splitResponse[1]);

            if (numberOfMessages < 0 || sizeInOctets < 0)
                throw new Exception("Maildrop info must be greater than zero. Number of Messages: " + numberOfMessages + ", Size of octets: " + sizeInOctets);
            NumberOfMessages = numberOfMessages;
            SizeInOctets = sizeInOctets;
        }

        public MaildropInfo(int numberOfMessages, int sizeInOctets)
        {
            if (numberOfMessages < 0 || sizeInOctets < 0)
                throw new Exception("Maildrop info must be greater than zero. Number of Messages: " + numberOfMessages + ", Size of octets: " + sizeInOctets);
            NumberOfMessages = numberOfMessages;
            SizeInOctets = sizeInOctets;
        }
    }
}