using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using POP3r.Pop3.Interfaces;
using POP3r.Pop3.ServerResponses;

namespace POP3r.Pop3
{
    public class ServerCommunicator : ICommunicator
    {
        private readonly IPEndPoint _mailEndpoint;
        private readonly Socket _socket;

        public ServerCommunicator(IPAddress ip, int port)
        {
            _mailEndpoint = new IPEndPoint(ip, port);
            _socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        private void CheckIfConnected()
        {
            if (!_socket.Connected)
            {
                throw new Exception("Can't excecute commands when not connected to server");
            }
        }

        public Response ExcecuteCommand(string command)
        {
            CheckIfConnected();
            SendCommand(command);
            return new Response(GetResponse());
        }

        public Response ExcecuteCommandMultiline(string command)
        {
            CheckIfConnected();
            SendCommand(command);
            return new Response(GetMultilineResponse());
        }

        private string GetMultilineResponse()
        {
            var result = string.Empty;
            do
            {
                result += GetResponse();
            } while (!result.EndsWith("\r\n.\r\n"));
            return result;
        }

        public void OpenConnection()
        {
            if (_socket.Connected)
            {
                throw new Exception("Can't open connection. It is already open");
            }

            try
            {
                _socket.Connect(_mailEndpoint);
                var response = new Response(GetResponse());
                if (!response.IsOk)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while connecting to mail server", ex);
            }
        }

        private void SendCommand(string command)
        {
            Debug.Print(command);
            _socket.Send(Encoding.UTF8.GetBytes(command + "\r\n"));
        }

        private string GetResponse()
        {
            var bytes = new byte[512];
            var buffSize = _socket.Receive(bytes);
            var receivedString = Encoding.UTF8.GetString(bytes, 0, buffSize);
            Debug.Print(receivedString.Trim());
            return receivedString;
        }

        public void CloseConnection()
        {
            if (!_socket.Connected)
            {
                throw new Exception("Can't close connection. It is already closed");
            }

            try
            {
                _socket.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while disconnecting from mail server", ex);
            }
        }
    }
}