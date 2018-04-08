using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3.Interfaces
{
    public interface ICommunicator
    {
        Response ExcecuteCommand(string command);
        Response ExcecuteCommandMultiline(string command);
        void OpenConnection();
        void CloseConnection();
    }
}