using System;

namespace UserAPI.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class MessageErrorAttribute : Attribute
    {
        public MessageErrorAttribute(string messageError)
        {
            MessageError = messageError;
        }

        public string MessageError { get; private set; }
    }
}
