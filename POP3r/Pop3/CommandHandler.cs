using System;
using System.Collections.Generic;
using System.Net;
using POP3r.Pop3.Interfaces;
using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3
{
    public class CommandHandler : ICommandHandler
    {
        private ICommunicator _communicator;

        public CommandHandler(string ip, int port)
        {
            _communicator = new ServerCommunicator(IPAddress.Parse(ip), port);
        }

        public void LoginToServer(string username, string password)
        {
            var loginUsernameCommand = string.Format(Commands.USER.GetCommandText(), username);
            var loginPasswordCommand = string.Format(Commands.PASS.GetCommandText(), password);

            _communicator.OpenConnection();

            SendCommand(loginUsernameCommand);
            SendCommand(loginPasswordCommand);
        }

        public void LogoutFromServer()
        {
            SendCommand(Commands.QUIT.GetCommandText());
            _communicator.CloseConnection();
        }

        public MaildropInfo GetMailboxInfo()
        {
            var response = SendCommand(Commands.STAT.GetCommandText());
            return new MaildropInfo(response.Body);
        }

        public List<MessageInfo> GetAllMessagesInfo()
        {
            string.Format(Commands.LIST.GetCommandText(), "");
            throw new System.NotImplementedException();
        }

        public MessageInfo GetMessageInfo(int index)
        {
            return new MessageInfo(SendCommand(string.Format(Commands.LIST.GetCommandText(), index)).Body);
        }

        public Message GetMessageBody(int index)
        {
            string.Format(Commands.RETR.GetCommandText(), index);
            throw new System.NotImplementedException();
        }

        public void DeleteMessage(int index)
        {
            SendCommand(string.Format(Commands.DELE.GetCommandText(), index));
        }

        public void ResetSession()
        {
            SendCommand(Commands.RSET.GetCommandText());
        }

        public bool UserIsLoggedIn()
        {
            return SendCommand(Commands.NOOP.GetCommandText()).IsOk;
        }

        public string GetUniqueId(int index)
        {
            return SendCommand(string.Format(Commands.UIDL.GetCommandText(), index)).Body;
        }

        public Message GetPartialMessageWithHeader(int index, int numberOfLines)
        {
            string.Format(Commands.TOP.GetCommandText(), index, numberOfLines);
            throw new System.NotImplementedException();
        }

        private Response SendCommand(string command)
        {
            var commandResponse = _communicator.ExcecuteCommand(command);
            if (!commandResponse.IsOk)
            {
                throw new Exception("Server returned error with message: " + commandResponse.Body);
            }

            return commandResponse;
        }
    }
}