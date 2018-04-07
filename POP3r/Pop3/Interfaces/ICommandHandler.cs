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
        MessageInfo GetMessageInfo(uint index);  // LIST
        Message GetMessageBody(uint index);  // RETR
        void DeleteMessage(uint index); // DELE
        void ResetSession(); // RSET
        bool UserIsLoggedIn(); // NOOP
        string GetUniqueId(uint index); // UIDL
        Message GetPartialMessageWithHeader(uint index, uint numberOfLines); // TOP
    }
}