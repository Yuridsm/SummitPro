﻿namespace TrincaBarbecue.Core.DomainException
{
    public class NameInvalidException : Exception
    {
        public NameInvalidException()
        {
        }

        public NameInvalidException(string message)
            : base(message)
        {
        }

        public NameInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
