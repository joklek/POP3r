using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3.Interfaces
{
    public interface IMessage
    {
        string Header { get; }
        string Body { get; }
        bool IsComplete { get; }
        MessageInfo Info { get; }
    }
}