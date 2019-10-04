using System;

namespace ZymkeySharp
{
    public class ZymkeyException : Exception {
        public ZymkeyException(int code) : base($"A Zymkey error occurred. Code: {code}.") {}
    }
}