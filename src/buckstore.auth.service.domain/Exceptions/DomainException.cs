using System;

namespace buckstore.auth.service.domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
            
        }
    }
}
