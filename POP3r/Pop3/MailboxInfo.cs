using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3
{
    public class MailboxInfo
    {
        public List<MessageInfo> ListOfMessageInfo { get; }
        public int MessageCount { get; }
        public int MailboxSize { get; }

        public MailboxInfo(Response response)
        {
            if (!Regex.IsMatch(response.Body, @"^\d*"))
                throw new Exception("Incorrect response string provided: " + response);

            var splitResponse = response.Body.Split(' ');
            MessageCount = int.Parse(splitResponse[0]);

            var text = response.Body.Substring(response.Body.IndexOf("\r\n", StringComparison.Ordinal) + "\r\n".Length);
            string[] separatingChars = { "\r\n" };
            var listOfStrings = text.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries).ToList();

            ListOfMessageInfo = listOfStrings.Select(
                x => new MessageInfo(int.Parse(x.Split(' ')[0]), int.Parse(x.Split(' ')[1]))).ToList();
        }
    }
}