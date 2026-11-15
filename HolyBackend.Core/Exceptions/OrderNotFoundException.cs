using System;

namespace HolyBackend.Core.Exceptions
{
    public class OrderNotFoundException : DomainException
    {
        public OrderNotFoundException(Guid orderId)
            : base($"Заявку з ідентифікатором {orderId} не знайдено в Гримуарі.") { }
    }
}
