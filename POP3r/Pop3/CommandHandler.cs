using System.Collections.Generic;
using System.Net;
using POP3r.Pop3.Interfaces;
using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3
{
    public class CommandHandler : ICommandHandler
    {
        private IPAddress IpAddress;
        private uint Port;

        public CommandHandler(string ipAddress, uint port)
        {
            IpAddress = IPAddress.Parse(ipAddress);
            Port = port;
        }

        public void LoginToServer(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public void LogoutFromServer()
        {
            throw new System.NotImplementedException();
        }

        public MaildropInfo GetMailboxInfo()
        {
            throw new System.NotImplementedException();
        }

        public List<MessageInfo> GetAllMessagesInfo()
        {
            throw new System.NotImplementedException();
        }

        public MessageInfo GetMessageInfo(uint index)
        {
            throw new System.NotImplementedException();
        }

        public Message GetMessageBody(uint index)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteMessage(uint index)
        {
            throw new System.NotImplementedException();
        }

        public void ResetSession()
        {
            throw new System.NotImplementedException();
        }

        public bool UserIsLoggedIn()
        {
            throw new System.NotImplementedException();
        }

        public string GetUniqueId(uint index)
        {
            throw new System.NotImplementedException();
        }

        public Message GetPartialMessageWithHeader(uint index, uint numberOfLines)
        {
            throw new System.NotImplementedException();
        }
    }
}