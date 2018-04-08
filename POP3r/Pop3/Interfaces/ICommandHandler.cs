using System.Collections.Generic;
using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3.Interfaces
{
    public interface ICommandHandler
    {
        void LoginToServer(string username, string password);  // USER ir PASS
        void LogoutFromServer(); // QUIT
        MaildropInfo GetMailboxInfo(); //STAT
        List<MessageInfo> GetAllMessagesInfo(); // LIST
        MessageInfo GetMessageInfo(int index);  // LIST
        Message GetMessageBody(int index);  // RETR
        void DeleteMessage(int index); // DELE
        void ResetSession(); // RSET
        bool UserIsLoggedIn(); // NOOP
        string GetUniqueId(int index); // UIDL
        Message GetPartialMessageWithHeader(int index, int numberOfLines); // TOP
    }
}