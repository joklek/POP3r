using System;

namespace POP3r
{
    public static class HeaderParser
    {
        public static string GetRecipient(string header)
        {
            const string identifier = "To";
            return ExtractPart(header, identifier);
        }

        public static string GetSender(string header)
        {
            const string identifier = "From";
            return ExtractPart(header, identifier);
        }

        public static string GetDate(string header)
        {
            const string identifier = "Date";
            return GetLineAfterIdentifier(header, identifier);
        }

        public static string GetSubject(string header)
        {
            const string identifier = "Subject";
            return GetLineAfterIdentifier(header, identifier);
        }

        private static string GetLineAfterIdentifier(string header, string identifier)
        {
            var fullIdentifier = identifier + ": ";
            if (!header.Contains(fullIdentifier))
            {
                return "";
            }

            var indexOfPartStart = header.IndexOf(fullIdentifier, StringComparison.Ordinal) + fullIdentifier.Length;
            var temporaryString = header.Substring(indexOfPartStart);
            var partToBeReturned = temporaryString.Remove(temporaryString.IndexOf("\r\n", StringComparison.Ordinal));
            return partToBeReturned;
        }

        private static string ExtractPart(string header, string identifier)
        {
            var fullIdentifier = identifier + ": ";
            if (!header.Contains(fullIdentifier))
            {
                return "";
            }

            var indexOfPartStart = header.IndexOf(fullIdentifier, StringComparison.Ordinal) + fullIdentifier.Length;
            var temporaryString = header.Substring(indexOfPartStart);

            indexOfPartStart = temporaryString.IndexOf("<", StringComparison.Ordinal) + "<".Length;
            temporaryString = temporaryString.Substring(indexOfPartStart);

            var indexOfPartEnd = temporaryString.IndexOf(">", StringComparison.Ordinal);
            var partToBeReturned = temporaryString.Remove(indexOfPartEnd);

            return partToBeReturned;
        }
    }
}