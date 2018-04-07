namespace POP3r.Pop3.ServerResponses
{
    public class MessageInfo
    {
        public MessageInfo(int messageId, int sizeInOctets)
        {
            MessageId = messageId;
            SizeInOctets = sizeInOctets;
        }

        public int MessageId { get; }
        public int SizeInOctets { get; }
    }
}