using System.Collections.Generic;

namespace HolyBackend.Core.Exceptions
{
    public class OrderValidationException : DomainException
    {
        public OrderValidationException(IEnumerable<string> errors)
            : base("Порушено канони Ордену: " + string.Join(" ", errors))
        {
        }
    }
}