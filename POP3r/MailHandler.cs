using System;
using System.Collections.Generic;
using POP3r.Pop3;
using POP3r.Pop3.Interfaces;

namespace POP3r
{
    public class MailHandler
    {
        private CommandHandler _commandHandler;
        public SessionStates State { get; private set; }
        private MailboxInfo _mailboxInfo;

        public MailHandler(string ipAddress, string port)
        {
            _commandHandler = new CommandHandler(ipAddress, int.Parse(port));
            State = SessionStates.AUTHORIZATION;
        }

        public void Login(string username, string password)
        {
            try
            {
                _commandHandler.LoginToServer(username, password);
                State = SessionStates.TRANSACTION;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Message> FetchMailList()
        {
            _mailboxInfo = _commandHandler.GetAllMessagesInfo();
            var numberOfMessages = _mailboxInfo.MailboxSize;

            var listOfMessages = new List<Message>();
            for (var i = 0; i < 10; i++)
            {
                try
                {
                    var message = _commandHandler.GetMessage(numberOfMessages - i);
                    message.IndexInMailList = _mailboxInfo.ListOfMessageInfo[i].MessageId; 
                    listOfMessages.Add(message);
                }
                catch
                {
                }
            }
            return listOfMessages;
        }

        public void Logout()
        {
            try
            {
                _commandHandler.LogoutFromServer();
                State = SessionStates.AUTHORIZATION;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteMessage(int index)
        {
            _commandHandler.DeleteMessage(index);
        }

        public int GetAmountOfMail()
        {
            return _commandHandler.GetMailboxInfo().NumberOfMessages;
        }
    }
}