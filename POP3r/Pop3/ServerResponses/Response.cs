using System;
using System.Collections.Generic;

namespace POP3r.Pop3.ServerResponses
{
    public class Response
    {
        public bool IsOk { get; }
        public string Body { get; }
        public string Raw { get; }

        private const string StatusCodeOk = "+OK";
        private const string StatusCodeError = "-ERR";
        private static readonly List<string> StatusList = new List<string> { StatusCodeOk, StatusCodeError };

        public Response(string response)
        {
            Raw = response;
            var status = GetStatus(response);
            IsOk = status == StatusCodeOk ;
            Body = RemoveStatus(response, status);
        }

        private static string GetStatus(string response)
        {
            foreach (var responseCode in StatusList)
            {
                if (response.StartsWith(responseCode + "\r\n") || response.StartsWith(responseCode + " "))
                {
                    return responseCode;
                }
            }
            throw new Exception("Server returned an unexpected message: " + response);
        }

        private static string RemoveStatus(string response, string status)
        {
            var body = response.Remove(0, status.Length);
            body = body.Trim();
            return body;
        }
    }
}