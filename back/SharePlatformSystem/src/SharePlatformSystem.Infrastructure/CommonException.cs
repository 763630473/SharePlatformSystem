using System;

namespace SharePlatformSystem.Infrastructure
{
    public class CommonException : Exception
    {
        private readonly int _code;

        public CommonException(string message, int code)
            : base(message)
        {
            this._code = code;
        }

        public int Code
        {
            get { return _code; }
        }

    }
}
