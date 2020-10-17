using System;

namespace Lib
{
    public class SysNotSuppException : Exception
    {
        private const string msg = "Sorry, your OS is not supported. Извините, ваша ОС не поддерживается.";

        public SysNotSuppException() : base(msg) { }
    }
}