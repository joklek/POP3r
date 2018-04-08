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
            var numberOfMessages = _commandHandler.GetMailboxInfo().NumberOfMessages;
            var listOfMessages = new List<Message>();
            for (var i = 10; i > 0; i--)
            {
                try
                {
                    listOfMessages.Add(_commandHandler.GetMessage(numberOfMessages - i));
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
    }
}