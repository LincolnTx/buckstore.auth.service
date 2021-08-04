using System;
using MediatR;

namespace buckstore.auth.service.application.IntegrationEvents.Events
{
    public class IntegrationEvent: INotification
    {
        public IntegrationEvent(DateTime createDate)
        {
            Id = Guid.NewGuid();
            CreationDate = createDate;
        }

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
