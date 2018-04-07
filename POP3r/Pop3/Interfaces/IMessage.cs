using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3.Interfaces
{
    public interface IMessage
    {
        string Header { get; set; }
        string Body { get; set; }
        bool IsComplete { get; set; }
        MessageInfo Info { get; }
    }
}