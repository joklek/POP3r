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
                    return "RETR";
                case Commands.DELE:
                    return "DELE";
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
    }
}