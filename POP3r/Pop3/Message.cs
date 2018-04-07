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

        public Message(MessageInfo info)
        {
            Info = info;
        }
    }
}