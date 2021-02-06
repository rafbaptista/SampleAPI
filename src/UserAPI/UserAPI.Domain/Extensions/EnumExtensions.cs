using System;
using System.Reflection;
using UserAPI.Domain.Attributes;

namespace UserAPI.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetMessageError(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(MessageErrorAttribute), false);
                if (attrs != null && attrs.Length > 0) return ((MessageErrorAttribute)attrs[0]).MessageError;
            }
            return en.ToString();
        }
    }
}
