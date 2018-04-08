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

            ExcecuteCommand(Commands.USER, loginUsernameCommand);
            ExcecuteCommand(Commands.PASS, loginPasswordCommand);
        }

        public void LogoutFromServer()
        {
            ExcecuteCommand(Commands.QUIT, Commands.QUIT.GetCommandText());
            _communicator.CloseConnection();
        }

        public MaildropInfo GetMailboxInfo()
        {
            var response = ExcecuteCommand(Commands.STAT, Commands.STAT.GetCommandText());
            return new MaildropInfo(response.Body);
        }

        public List<MessageInfo> GetAllMessagesInfo()
        {
            string.Format(Commands.LIST.GetCommandText(), "");
            throw new System.NotImplementedException();
        }

        public MessageInfo GetMessageInfo(int index)
        {
            return new MessageInfo(ExcecuteCommand(Commands.LIST, string.Format(Commands.LIST.GetCommandText(), index)).Body);
        }

        public Message GetMessageBody(int index)
        {
            return new Message(ExcecuteCommand(Commands.RETR, string.Format(Commands.RETR.GetCommandText(), index)));
        }

        public void DeleteMessage(int index)
        {
            ExcecuteCommand(Commands.DELE, string.Format(Commands.DELE.GetCommandText(), index));
        }

        public void ResetSession()
        {
            ExcecuteCommand(Commands.RSET, Commands.RSET.GetCommandText());
        }

        public bool UserIsLoggedIn()
        {
            return ExcecuteCommand(Commands.NOOP, Commands.NOOP.GetCommandText()).IsOk;
        }

        public string GetUniqueId(int index)
        {
            return ExcecuteCommand(Commands.UIDL, string.Format(Commands.UIDL.GetCommandText(), index)).Body;
        }

        public Message GetPartialMessageWithHeader(int index, int numberOfLines)
        {
            string.Format(Commands.TOP.GetCommandText(), index, numberOfLines);
            throw new System.NotImplementedException();
        }

        private Response ExcecuteCommand(Commands commandType, string commandBody)
        {
            Response commandResponse;

            if (commandType != Commands.TOP && commandType != Commands.RETR)
            {
                commandResponse = _communicator.ExcecuteCommand(commandBody);
            }
            else
            {
                commandResponse = _communicator.ExcecuteCommandMultiline(commandBody);
            }
            
            if (!commandResponse.IsOk)
            {
                throw new Exception("Server returned error with message: " + commandResponse.Body);
            }

            return commandResponse;
        }
    }
}