﻿namespace TrincaBarbecue.SharedKernel.DomainException
{
    public class InvalidUsernameException : Exception
    {
        public InvalidUsernameException()
        {
        }

        public InvalidUsernameException(string message)
            : base(message)
        {
        }

        public InvalidUsernameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
