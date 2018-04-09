using System;
using POP3r.Properties;

namespace POP3r.Pop3
{
    public static class CommandsExtension
    {
        public static string GetCommandText(this Commands self)
        {
            switch (self)
            {
                case Commands.USER:
                    return "USER {0}";
                case Commands.PASS:
                    return "PASS {0}";
                case Commands.STAT:
                    return "STAT";
                case Commands.RETR:
                    return "RETR {0}";
                case Commands.DELE:
                    return "DELE {0}";
                case Commands.LIST:
                    return "LIST {0}";
                case Commands.NOOP:
                    return "NOOP";
                case Commands.TOP:
                    return "TOP {0} {1}";
                case Commands.UIDL:
                    return "UIDL {0}";
                case Commands.RSET:
                    return "RSET";
                case Commands.QUIT:
                    return "QUIT";
                default:
                    throw new ArgumentOutOfRangeException(nameof(self), self, Resources.CommandsExtension_GetFormat_invalid_enum_type);
            }
        }

        public static bool CommandValidInState(this Commands self, SessionStates state)
        {
            switch (self)
            {
                case Commands.QUIT:
                    return true;
                case Commands.USER:
                case Commands.PASS:
                    return state == SessionStates.AUTHORIZATION;
            }

            return state == SessionStates.TRANSACTION;
        }
    }
}